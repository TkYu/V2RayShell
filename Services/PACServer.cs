using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using V2RayShell.Model;
using V2RayShell.Properties;

namespace V2RayShell.Services
{
    class PACServer : SimpleTcpServer
    {
        public const string RESOURCE_NAME = "pac";
        public const string PAC_FILE = "pac.txt";
        public const string USER_RULE_FILE = "user-rule.txt";

        private Configuration _config;

        public static readonly System.Threading.ReaderWriterLockSlim pacLocker = new System.Threading.ReaderWriterLockSlim();
        public string PacUrl { get; private set; } = "";

        public static string LanIP => System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.FirstOrDefault(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString()??"127.0.0.1";
        public static string PacContent
        {
            get
            {
                pacLocker.EnterReadLock();
                if (!File.Exists(PAC_FILE)) TouchPACFile();
                try
                {
                    return File.ReadAllText(PAC_FILE, Encoding.UTF8);
                }
                finally
                {
                    pacLocker.ExitReadLock();
                }
            }

            set
            {
                pacLocker.EnterWriteLock();
                try
                {
                    File.WriteAllText(PAC_FILE, value, Encoding.UTF8);
                }
                finally
                {
                    pacLocker.ExitWriteLock();
                }
            }
        }

        public int RunningPort { get; private set; }

        public PACServer():base(100,Int16.MaxValue)
        {
            Process = ProcessMethod;
            RunningPort = Utils.GetRandomPort();

            TouchPACFile();
            TouchUserRuleFile();
            WatchPacFile();
            WatchUserRuleFile();
        }

        private byte[] ProcessMethod(SocketAsyncEventArgs arg)
        {
            //var msg = Encoding.UTF8.GetString(arg.Buffer, arg.Offset, arg.BytesTransferred);
            var pac = PacContent.Replace("__PROXY__", $"\"PROXY {(_config.shareOverLan ? LanIP : "127.0.0.1")}:{_config.localPort};\"");
            string responseHead =
                $@"HTTP/1.1 200 OK
Server: V2RayShell/{Global.Version}
Content-Type: application/x-ns-proxy-autoconfig
Content-Length: { Encoding.UTF8.GetBytes(pac).Length}
Connection: Close

";
            return Encoding.UTF8.GetBytes(responseHead + pac);
        }


        public void Start(Configuration config)
        {
            _config = config;
            Start(new System.Net.IPEndPoint(_config.shareOverLan ? System.Net.IPAddress.Any : System.Net.IPAddress.Loopback, RunningPort));
        }


        private static string GetHash(string content)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(content));
                return Uri.EscapeUriString(BitConverter.ToString(hash).Replace("-",""));
            }
        }

        public void UpdatePACURL(Configuration config)
        {
            _config = config;
            string contentHash = GetHash(PacContent);
            PacUrl = $"http://{(config.shareOverLan? LanIP : "127.0.0.1")}:{RunningPort}/{RESOURCE_NAME}?hash={contentHash}";
            Logging.Debug("Set PAC URL:" + PacUrl);
        }

        public static string ReplaceContent(string content, string rules, string userRules = null)
        {
            var result = content;
            if (!string.IsNullOrEmpty(rules))
                result = Regex.Replace(content, "var\\s+rules\\s+=\\s+(.*?);", $"var rules = {rules};", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (!string.IsNullOrEmpty(userRules))
                result = Regex.Replace(result, "var\\s+userrules\\s+=\\s+(.*?);", $"var userrules = {userRules};", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return result;
        }

        #region Watcher

        FileSystemWatcher PACFileWatcher;
        FileSystemWatcher UserRuleFileWatcher;

        public event EventHandler PACFileChanged;
        public event EventHandler UserRuleFileChanged;


        public static string TouchPACFile()
        {
            if (!File.Exists(PAC_FILE))
            {
                var defaultRules = FileManager.UncompressString(Resources.proxy_pac_txt);
                var temp = FileManager.UncompressString(Resources.abp_js).Replace("__RULES__",defaultRules);
                temp = temp.Replace("__USERRULES__", "[]");
                File.WriteAllText(PAC_FILE, temp, Encoding.UTF8);
            }
            return PAC_FILE;
        }

        public static string TouchUserRuleFile()
        {
            if (!File.Exists(USER_RULE_FILE)) File.WriteAllText(USER_RULE_FILE, Resources.user_rule);
            return USER_RULE_FILE;
        }


        private void WatchPacFile()
        {
            PACFileWatcher?.Dispose();
            PACFileWatcher = new FileSystemWatcher(Directory.GetCurrentDirectory());
            PACFileWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            PACFileWatcher.Filter = PAC_FILE;
            PACFileWatcher.Changed += PACFileWatcher_Changed;
            PACFileWatcher.Created += PACFileWatcher_Changed;
            PACFileWatcher.Deleted += PACFileWatcher_Changed;
            PACFileWatcher.Renamed += PACFileWatcher_Changed;
            PACFileWatcher.EnableRaisingEvents = true;
        }

        private void WatchUserRuleFile()
        {
            UserRuleFileWatcher?.Dispose();
            UserRuleFileWatcher = new FileSystemWatcher(Directory.GetCurrentDirectory());
            UserRuleFileWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            UserRuleFileWatcher.Filter = USER_RULE_FILE;
            UserRuleFileWatcher.Changed += UserRuleFileWatcher_Changed;
            UserRuleFileWatcher.Created += UserRuleFileWatcher_Changed;
            UserRuleFileWatcher.Deleted += UserRuleFileWatcher_Changed;
            UserRuleFileWatcher.Renamed += UserRuleFileWatcher_Changed;
            UserRuleFileWatcher.EnableRaisingEvents = true;
        }


        #region FileSystemWatcher.OnChanged()
        private void PACFileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (PACFileChanged != null)
            {
                Logging.Info($"Detected: PAC file '{e.Name}' was {e.ChangeType.ToString().ToLower()}.");
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    ((FileSystemWatcher)sender).EnableRaisingEvents = false;
                    System.Threading.Thread.Sleep(10);
                    PACFileChanged(this, new EventArgs());
                    ((FileSystemWatcher)sender).EnableRaisingEvents = true;
                });
            }
        }

        private void UserRuleFileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (UserRuleFileChanged != null)
            {
                Logging.Info($"Detected: User Rule file '{e.Name}' was {e.ChangeType.ToString().ToLower()}.");
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    ((FileSystemWatcher)sender).EnableRaisingEvents = false;
                    System.Threading.Thread.Sleep(10);
                    UserRuleFileChanged(this, new EventArgs());
                    ((FileSystemWatcher)sender).EnableRaisingEvents = true;
                });
            }
        }
        #endregion
        #endregion
    }
}
