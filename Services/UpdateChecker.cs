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
        //public const string SHELL_URL = "https://github.com/TkYu/V2RayShell/releases/latest";
        public const string SHELL_URL = "https://github.com.cnpmjs.org/TkYu/V2RayShell/releases";
        public const string SHELL_API = "https://api.github.com/repos/TkYu/V2RayShell/releases";
        public const string V2RAY_URL = "https://github.com/v2ray/v2ray-core/releases/latest";

        public UpdateChecker()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;//3072
        }

        private async Task<string> GetCoreVersion(string proxy = null)
        {
            if (proxy == null)
            {
                try
                {
                    var regx = new Regex(@"<a href=""/v2ray/dist/releases/tag/(.*?)"">(.*?)</a>",RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    var request = (HttpWebRequest)WebRequest.Create("https://github.com.cnpmjs.org/v2ray/dist/releases");
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
            if (proxy == null)
            {
                try
                {
                    var regx = new Regex(@"<a href=""/TkYu/V2RayShell/releases/tag/(.*?)"">(.*?)</a>",RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    var request = (HttpWebRequest)WebRequest.Create(SHELL_URL);
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.132 Safari/537.36";
                    request.Timeout = 6000;
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
                    var request = (HttpWebRequest)WebRequest.Create(SHELL_API);
                    request.Proxy = new WebProxy(new Uri(proxy));
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.132 Safari/537.36";
                    request.Timeout = 6000;
                    request.AllowAutoRedirect = true;
                    var response = await request.GetResponseAsync();
                    using (var sr = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
                    {
                        var responseBody = await sr.ReadToEndAsync();
                        var parse = await Utils.DeSerializeJsonObjectAsync<GitHubRelease[]>(responseBody);
                        var version = parse.FirstOrDefault()?.tag_name;
                        if (!string.IsNullOrEmpty(version)) return version.TrimStart('v');
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Logging.LogUsefulException(e);
                    return null;
                }
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

    public class GitHubRelease
    {
        public string url { get; set; }
        public string assets_url { get; set; }
        public string upload_url { get; set; }
        public string html_url { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string tag_name { get; set; }
        public string target_commitish { get; set; }
        public string name { get; set; }
        public bool draft { get; set; }
        public Author author { get; set; }
        public bool prerelease { get; set; }
        public DateTime created_at { get; set; }
        public DateTime published_at { get; set; }
        public Asset[] assets { get; set; }
        public string tarball_url { get; set; }
        public string zipball_url { get; set; }
        public string body { get; set; }
    }

    public class Author
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }

    public class Asset
    {
        public string url { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string name { get; set; }
        public object label { get; set; }
        public Uploader uploader { get; set; }
        public string content_type { get; set; }
        public string state { get; set; }
        public int size { get; set; }
        public int download_count { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string browser_download_url { get; set; }
    }

    public class Uploader
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }

}