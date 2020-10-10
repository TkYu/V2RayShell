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

        public const string V2RAY_URL = "https://github.com/v2ray/v2ray-core/releases/latest";
        public const string SHELL_URL = "https://github.com/TkYu/V2RayShell/releases/latest";
        public const string V2RAY_CNPMJS_URL = "https://github.com.cnpmjs.org/v2ray/v2ray-core/releases/latest";
        public const string V2RAY_FASTGIT_URL = "https://hub.fastgit.org/v2ray/v2ray-core/releases/latest";
        public const string SHELL_CNPMJS_URL = "https://github.com.cnpmjs.org/TkYu/V2RayShell/releases/latest";

        public UpdateChecker()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public static async Task<string> GetCoreVersion(string proxy = null)
        {
            if (!string.IsNullOrEmpty(proxy))
                return await GetCoreVersion(V2RAY_URL, proxy);
            var ret = await GetCoreVersion(V2RAY_CNPMJS_URL, null);
            if (string.IsNullOrEmpty(ret))
                return await GetCoreVersion(V2RAY_FASTGIT_URL, null);
            if (string.IsNullOrEmpty(ret))
                return await GetCoreVersion(V2RAY_URL, null);
            return ret;
        }

        public static async Task<string> GetCoreVersion(string entry, string proxy)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(entry);
                if (!string.IsNullOrEmpty(proxy)) request.Proxy = new WebProxy(new Uri(proxy));
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

        public static async Task<string> GetVersion(string proxy = null)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(proxy == null ? SHELL_CNPMJS_URL : SHELL_URL);
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