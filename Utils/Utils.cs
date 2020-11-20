using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace V2RayShell
{
    public static class Utils
    {
        public static string GetTempPath(string filename = null)
        {
            string tmpPath;
            if (File.Exists(Path.Combine(Global.AppPath,"v2ray_portable_mode.txt")))
            {
                tmpPath = Path.Combine(Global.AppPath,"temp");
                try
                {
                    if (!Directory.Exists(tmpPath))
                        Directory.CreateDirectory(tmpPath);
                } catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                tmpPath = Path.GetTempPath();
            }

            if (string.IsNullOrEmpty(filename)) return tmpPath;
            return Path.Combine(tmpPath,filename);
        }

        public static void ReleaseMemory(bool removePages)
        {
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
            if (removePages)
            {
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr) 0xFFFFFFFF, (UIntPtr) 0xFFFFFFFF);
            }
        }

        #region Method
        public static string DeBase64(this string str, Encoding encoding = null)
        {
            return (encoding??Encoding.UTF8).GetString(Convert.FromBase64String(str.Rfc4648Base64UrlDecode()));
        }

        public static string Rfc4648Base64UrlDecode(this string str)
        {
            str = str.Replace("\r","").Replace("\n", "").Replace('-', '+').Replace('_', '/');
            switch (str.Length % 4)
            { // Pad with trailing '='s
                case 0:
                    // No pad chars in this case
                    break;
                case 2:
                    // Two pad chars
                    str += "==";
                    break;
                case 3:
                    // One pad char
                    str += "=";
                    break;
                default:
                    throw new Exception("Invalid Base64 string.");
            }
            return str;
        }

        public static IEnumerable<TControl> GetChildControls<TControl>(this Control control) where TControl : Control
        {
            if (control.Controls.Count == 0) return Enumerable.Empty<TControl>();
            var children = control.Controls.OfType<TControl>().ToList();
            return children.SelectMany(GetChildControls<TControl>).Concat(children);
        }

        public static string GetChromeUrl()
        {
            var get = GetChromeUrl(GetForegroundWindow());
            if (string.IsNullOrEmpty(get)) get = Process.GetProcessesByName("chrome").Where(p => p.MainWindowHandle != IntPtr.Zero).Select(proc => GetChromeUrl(proc.MainWindowHandle)).FirstOrDefault();
            if (string.IsNullOrEmpty(get)) get = Process.GetProcessesByName("msedge").Where(p => p.MainWindowHandle != IntPtr.Zero).Select(proc => GetChromeUrl(proc.MainWindowHandle)).FirstOrDefault();
            return get;
        }

        public static string GetChromeUrl(IntPtr hwnd)
        {
            var element = AutomationElement.FromHandle(hwnd);
            //hwnd.Dump();
            //element.Current.ClassName.Dump();
            var edit = element?.FindFirst(TreeScope.Descendants | TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));
            string vp = ((ValuePattern)edit?.GetCurrentPattern(ValuePattern.Pattern))?.Current.Value;
            if (!string.IsNullOrEmpty(vp))
            {
                // prepend http:// to the url, because Chrome hides it if it's not SSL
                if (!vp.StartsWith("http"))
                {
                    vp = "https://" + vp;
                }
                return vp;
            }
            return null;
        }

        public static int GetRandomPort(int start = 22468,int end = 32468)
        {//only check ipv4
            Logging.Info($"Checking Port {start} to {end}");
            var ports = Enumerable.Range(start, end - start + 1).ToArray();
            var ipGlobalProperties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners().Where(gp => gp.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(c => c.Port).ToArray();
            return ports.First(c => !tcpConnInfoArray.Contains(c));
        }

        public static bool IsPortAvailable(int port,bool isAny = false)
        {
            System.Net.Sockets.TcpListener listener = null;
            try
            {
                listener = new System.Net.Sockets.TcpListener(isAny ? System.Net.IPAddress.Any : System.Net.IPAddress.Loopback, port);
                listener.Start();
                return true;
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                return false;
            }
            finally
            {
                listener?.Stop();
            }
        }

        public static bool IsAvailable(this System.Net.IPEndPoint endPoint)
        {
            System.Net.Sockets.TcpListener listener = null;
            try
            {
                listener = new System.Net.Sockets.TcpListener(endPoint);
                listener.Start();
                return true;
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                return false;
            }
            finally
            {
                listener?.Stop();
            }
        }

        public static string GetCommandLine(this Process process)
        {
            using (var searcher = new System.Management.ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
            using (var objects = searcher.Get())
            {
                return objects.Cast<System.Management.ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
            }
        }

        public static void KillProcess(this Process p)
        {
            try
            {
                p.CloseMainWindow();
                p.WaitForExit(100);
                if (!p.HasExited)
                {
                    p.Kill();
                    p.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
            }
        }

        public static string StartProcess(string fileName,string args)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = args,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            var process = Process.Start(processStartInfo);
            var output = process?.StandardOutput.ReadToEnd();
            process?.WaitForExit();
            return output;
        }

        public static void SetNotifyIconText(this NotifyIcon ni, string text)
        {
            if (text.Length >= 128)
                // ReSharper disable once NotResolvedInText
                throw new ArgumentOutOfRangeException("Text limited to 127 characters");
            Type t = typeof(NotifyIcon);
            BindingFlags hidden = BindingFlags.NonPublic | BindingFlags.Instance;
            t.GetField("text", hidden)?.SetValue(ni, text);
            var val = t.GetField("added", hidden)?.GetValue(ni);
            if (val != null && (bool)val)
                t.GetMethod("UpdateIcon", hidden)?.Invoke(ni, new object[] { true });
        }

        private class JsonSerializerStrategy : SimpleJson.PocoJsonSerializerStrategy
        {
            // convert string to int
            public override object DeserializeObject(object value, Type type)
            {
                if (type == typeof(int) && value is string)
                {
                    return int.Parse(value.ToString());
                }
                return base.DeserializeObject(value, type);
            }
        }
        public static string SerializeToJson(this object obj)
        {
            return SimpleJson.SimpleJson.SerializeObject(obj, new JsonSerializerStrategy());
        }

        public static Task<string> SerializeToJsonAsync(this object obj)
        {
            return Task.Run(() => SimpleJson.SimpleJson.SerializeObject(obj, new JsonSerializerStrategy()));
        }
        public static T DeSerializeJsonObject<T>(string jsonString)
        {
            return SimpleJson.SimpleJson.DeserializeObject<T>(jsonString, new JsonSerializerStrategy());
        }

        public static Task<T> DeSerializeJsonObjectAsync<T>(string jsonString)
        {
            return Task.Run(() => SimpleJson.SimpleJson.DeserializeObject<T>(jsonString, new JsonSerializerStrategy()));
        }


        #endregion

        #region DllImport
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();


        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process, UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);

        #endregion
    }
}
