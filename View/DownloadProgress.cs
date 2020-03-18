using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using V2RayShell.Properties;
using V2RayShell.Services;

namespace V2RayShell.View
{
    public partial class DownloadProgress : Form
    {
        public DownloadProgress()
        {
            InitializeComponent();
            Icon = Resources.v2ray;
        }

        private const string V2RAY_URL = "https://github.com/v2ray/v2ray-core/releases/latest";

        #region InvokeMethod

        private delegate void GoodByeDelegate(DialogResult result);
        private void GoodBye(DialogResult result)
        {
            if (InvokeRequired)
            {
                Invoke(new GoodByeDelegate(GoodBye), result);
            }
            else
            {
                DialogResult = result;
                Close();
            }
        }

        private delegate void ChangeTextDelegate(string str);
        private void ChangeText(string str)
        {
            if (InvokeRequired)
                Invoke(new ChangeTextDelegate(ChangeText), str);
            else
                textBox1.Text = str;
        }

        private delegate void ChangeProgressDelegate(int value);
        private void ChangeProgress(int value)
        {
            if (InvokeRequired)
            {
                Invoke(new ChangeProgressDelegate(ChangeProgress), value);
            }
            else
            {
                if (value > 100 || value<0)
                {
                    progressBar1.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    if (progressBar1.Style != ProgressBarStyle.Blocks)
                        progressBar1.Style = ProgressBarStyle.Blocks;
                    progressBar1.Value = value;
                }
            }
        }

        #endregion

        private async Task<string> GetVersion(string proxy = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(V2RAY_URL);
                if(!string.IsNullOrEmpty(proxy))
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

        private void DownloadProgress_Load(object sender, EventArgs e)
        {
            Text = I18N.GetString("Sit back and relax");
            ActiveControl = progressBar1;
            //TODO
            _ = DoUpdate();
        }

        private async Task DoUpdate()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;//3072
            ChangeProgress(999);
            ChangeText(I18N.GetString("Get latest version..."));
            var newVersion = await GetVersion();
            string proxy = null;
            if (string.IsNullOrEmpty(newVersion))
            {
                proxy = Microsoft.VisualBasic.Interaction.InputBox(I18N.GetString("We need a proxy to download v2ray core"), "Yo", "http://127.0.0.1:1080");
                if (Uri.IsWellFormedUriString(proxy,UriKind.Absolute)) newVersion = await GetVersion(proxy);
            }

            if (string.IsNullOrEmpty(newVersion))
            {
                System.Diagnostics.Process.Start(V2RAY_URL);
                GoodBye(DialogResult.Abort);
                return;
            }

            ChangeText(I18N.GetString("Upgrade {0} to {1} ...",V2Ray.Version?.ToString()??"0.0.0",newVersion));
            WebClient webClient = new WebClient();
            if(!string.IsNullOrEmpty(proxy) && Uri.IsWellFormedUriString(proxy,UriKind.Absolute))
                webClient.Proxy = new WebProxy(new Uri(proxy));
            webClient.DownloadProgressChanged += (s, e) =>
            {
                ChangeProgress(e.ProgressPercentage);
                //ChangeText(newVersion + I18N.GetString("Downloading...") + $" {e.ProgressPercentage}%");
            };
            var fileName = Utils.GetTempPath(Guid.NewGuid().ToString("N"));
            var downloadURL = $"https://github.com/v2ray/v2ray-core/releases/download/v{newVersion}/v2ray-windows-{(Environment.Is64BitOperatingSystem ? "64" : "32")}.zip";
            ChangeText(I18N.GetString("Downloading file from {0}, You can download it manually and extract to same folder.", downloadURL));
            await webClient.DownloadFileTaskAsync(downloadURL, fileName);
            ChangeProgress(100);
            ChangeText(newVersion + I18N.GetString("Extracting..."));
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(fileName))
                {
                    var additional = Path.Combine(Global.AppPath, "Additional");
                    if (!Directory.Exists(additional))
                        Directory.CreateDirectory(additional);
                    V2RayRunner.KillAll();
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.Length == 0)
                            continue;
                        if (entry.FullName.Contains('/'))
                            continue;//directory, useless
                        if (entry.FullName == "v2ray.exe" || entry.FullName == "v2ctl.exe")
                        {
                            entry.ExtractToFile(Path.Combine(Global.AppPath, entry.FullName), true);
                        }
                        else
                        {
                            entry.ExtractToFile(Path.Combine(additional, entry.FullName), true);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                GoodBye(DialogResult.Abort);
                return;
            }
            finally
            {
                File.Delete(fileName);
            }
            GoodBye(DialogResult.OK);
        }
    }



}
