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
    public class UpdateChecker
    {
        public const string SHELL_URL = "https://github.com/TkYu/V2RayShell/releases/latest";
        public const string V2RAY_URL = "https://github.com/v2ray/v2ray-core/releases/latest";

        public UpdateChecker()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        private async Task<string> GetCoreVersion(string proxy = null)
        {
            if (proxy == null)
            {
                try
                {
                    var regx = new Regex(@"<a href=""/v2ray/dist/releases/tag/(.*?)"">(.*?)</a>",RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    var request = (HttpWebRequest)WebRequest.Create("https://github.com.cnpmjs.org/v2ray/dist/releases/latest");
                    request.Timeout = 5000;
                    request.AllowAutoRedirect = true;
                    var response = await request.GetResponseAsync();
                    using (var sr = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
                    {
                        var responseBody = await sr.ReadToEndAsync();
                        if (regx.IsMatch(responseBody))
                        {
                            var mc = regx.Matches(responseBody);
                            return mc[0].Groups[1].Value.TrimStart('v');
                        }
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Logging.LogUsefulException(e);
                    return null;
                }
            }
            else
            {
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(V2RAY_URL);
                    request.Proxy = new WebProxy(new Uri(proxy));
                    request.Timeout = 5000;
                    request.AllowAutoRedirect = false;
                    var response = await request.GetResponseAsync();
                    return response.Headers["Location"].Split('/').Last().TrimStart('v');
                }
                catch (Exception e)
                {
                    Logging.LogUsefulException(e);
                    return null;
                }
            }
        }

        private async Task<string> GetVersion(string proxy = null)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(proxy == null ? "https://github.com.cnpmjs.org/TkYu/V2RayShell/releases/latest" : SHELL_URL);
                if (proxy != null) request.Proxy = new WebProxy(new Uri(proxy));
                request.Timeout = 5000;
                request.AllowAutoRedirect = false;
                var response = await request.GetResponseAsync();
                return response.Headers["Location"].Split('/').Last().TrimStart('v');
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                return null;
            }
        }

        public async Task<Tuple<bool, bool, string, string>> CheckUpdate(Configuration config)
        {
            var v2rayupdate = false;
            var v2rayversion = await GetCoreVersion();
            if (string.IsNullOrEmpty(v2rayversion)) v2rayversion = await GetCoreVersion($"http://127.0.0.1:{config.localPort}");
            if (!string.IsNullOrEmpty(v2rayversion) && CompareVersion(v2rayversion, V2Ray.Version.ToString()) > 0) v2rayupdate = true;
            var versionupdate = false;
            var version = await GetVersion();
            if (string.IsNullOrEmpty(version)) version = await GetVersion($"http://127.0.0.1:{config.localPort}");
            if (!string.IsNullOrEmpty(version) && CompareVersion(version, Global.Version) > 0) versionupdate = true;
            return new Tuple<bool, bool, string, string>(versionupdate, v2rayupdate, version, v2rayversion);
        }

        public static int CompareVersion(string l, string r)
        {
            var ls = l.Split('.');
            var rs = r.Split('.');
            for (int i = 0; i < Math.Max(ls.Length, rs.Length); i++)
            {
                int lp = (i < ls.Length) ? int.Parse(ls[i]) : 0;
                int rp = (i < rs.Length) ? int.Parse(rs[i]) : 0;
                if (lp != rp)
                {
                    return lp - rp;
                }
            }

            return 0;
        }
    }
}