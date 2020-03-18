using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using V2RayShell.Model;

namespace V2RayShell.Services
{
    public class GFWListUpdater
    {
        private const string GFWLIST_URL = "https://raw.githubusercontent.com/gfwlist/gfwlist/master/gfwlist.txt";


        public event EventHandler<ResultEventArgs> UpdateCompleted;

        public event ErrorEventHandler Error;

        public class ResultEventArgs : EventArgs
        {
            public bool Success;

            public ResultEventArgs(bool success)
            {
                this.Success = success;
            }
        }

        private void http_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                bool pacFileChanged = MergeAndWritePACFile(e.Result);
                UpdateCompleted?.Invoke(this, new ResultEventArgs(pacFileChanged));
            }
            catch (Exception ex)
            {
                Error?.Invoke(this, new ErrorEventArgs(ex));
            }
        }

        public void UpdatePACFromGFWList(Configuration config)
        {
            WebClient http = new WebClient {Proxy = new WebProxy(IPAddress.Loopback.ToString(), config.localPort)};
            http.DownloadStringCompleted += http_DownloadStringCompleted;
            http.DownloadStringAsync(new Uri(GFWLIST_URL));
        }

        public static List<string> ParseResult(string response)
        {
            if (string.IsNullOrEmpty(response)) throw new ArgumentNullException(nameof(response));
            string content = Encoding.Default.GetString(Convert.FromBase64String(response));
            if (!CheckSum(content)) throw new Exception(I18N.GetString("CheckSum Fail"));
            string[] lines = content.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> valid_lines = new List<string>();
            foreach (var line in lines)
            {
                if (line.StartsWith("!") || line.StartsWith("["))
                    continue;
                valid_lines.Add(line);
            }
            return valid_lines;
        }

        public static bool MergeAndWritePACFile(string gfwListResult)
        {
            PACServer.TouchPACFile();
            var original = PACServer.PacContent;
            string rules = null, userrules = null;
            if (!string.IsNullOrEmpty(gfwListResult))
            {
                var parse = ParseResult(gfwListResult);
                rules = SimpleJson.SimpleJson.SerializeObject(parse);
            }
            if (File.Exists(PACServer.USER_RULE_FILE))
            {
                string ur = File.ReadAllText(PACServer.USER_RULE_FILE, Encoding.UTF8);
                var spl = ur.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Where(c=>!c.StartsWith("!") && !c.StartsWith("["));
                userrules = SimpleJson.SimpleJson.SerializeObject(spl);
            }
            var result = PACServer.ReplaceContent(original, rules, userrules);
            if (original.Equals(result))
            {
                return false;
            }
            PACServer.PacContent = result;
            return true;
        }


        private static readonly Regex check = new Regex(@"^\s*!\s*checksum[\s\-:]+([\w\+\/=]+).*\n",RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private static bool CheckSum(string content)
        {
            var mch = check.Match(content);
            if (mch.Success)
            {
                var current = mch.Groups[1].Value.Trim();
                return current == CalcSum(content);
            }
            return false;
        }
        private static string CalcSum(string content)
        {
            var clean = check.Replace(Regex.Replace(content.Replace("\r", ""), "\n+", "\n"), "");
            using (var md5 = System.Security.Cryptography.MD5.Create())
                return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(clean))).TrimEnd('=');
        }

    }
}
