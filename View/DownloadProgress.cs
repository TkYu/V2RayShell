using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using V2RayShell.Properties;
using V2RayShell.Services;

namespace V2RayShell.View
{
    public sealed partial class DownloadProgress : Form
    {
        private readonly int act = 0;
        private readonly string filePath;
        private readonly string fileName;
        private readonly System.Threading.CancellationTokenSource cts;
        private static string UA = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.92 Safari/537.36";
        public DownloadProgress(int action)
        {
            InitializeComponent();
            Icon = Resources.v2ray;
            Font = Global.Font;
            act = action;
            fileName = Guid.NewGuid().ToString("N") + ".zip";
            filePath = Utils.GetTempPath(fileName);
            cts = new System.Threading.CancellationTokenSource();
        }


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

        private delegate void ChangeTitleDelegate(string str);
        private void ChangeTitle(string str)
        {
            if (InvokeRequired)
                Invoke(new ChangeTitleDelegate(ChangeTitle), str);
            else
                Text = str;
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


        private async void DownloadProgress_Load(object sender, EventArgs e)
        {
            Text = I18N.GetString("Sit back and relax");
            ActiveControl = progressBar1;
            //ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;//3072

            #region G2G
            //switch (act)
            //{
            //    case 1 when await SelfUpdate():
            //        GoodBye(DialogResult.OK);
            //        return;
            //    case 2 when await DoUpdate():
            //        GoodBye(DialogResult.OK);
            //        return;
            //    case 3 when await DoUpdate() && await SelfUpdate():
            //        GoodBye(DialogResult.OK);
            //        return;
            //}
            var ok = (act == 1 || act == 3) && await SelfUpdate();
            if ((act == 2 || act == 3) && await DoUpdate()) ok = true;
            GoodBye(ok ? DialogResult.OK : DialogResult.Abort);

            #endregion
        }

        private async Task<bool> SelfUpdate()
        {
            ChangeProgress(999);
            ChangeText(I18N.GetString("Get latest version..."));
            var newVersion = await UpdateChecker.GetVersion();
            string proxy = null;
            if (string.IsNullOrEmpty(newVersion))
            {
                proxy = Microsoft.VisualBasic.Interaction.InputBox(I18N.GetString("We need a proxy to download v2ray core"), "Yo", "http://127.0.0.1:1080");
                if (Uri.IsWellFormedUriString(proxy,UriKind.Absolute)) newVersion = await UpdateChecker.GetVersion(proxy);
            }
            if (string.IsNullOrEmpty(newVersion))
            {
                Process.Start(UpdateChecker.SHELL_URL);
                return false;
            }
            ChangeText(I18N.GetString("Upgrade {0} to {1} ...",Global.Version,newVersion));
            var webClient = new WebClient();
            if(!string.IsNullOrEmpty(proxy) && Uri.IsWellFormedUriString(proxy,UriKind.Absolute))
                webClient.Proxy = new WebProxy(new Uri(proxy));
            cts.Token.Register(() => webClient.CancelAsync());
            webClient.DownloadProgressChanged += (s, e) =>
            {
                ChangeProgress(e.ProgressPercentage);
            };
            var downloadURL = $"https://github.wuyanzheshui.workers.dev/TkYu/V2RayShell/releases/download/v{newVersion}/V2RayShell{newVersion}.zip";
            ChangeTitle(I18N.GetString("Sit back and relax") + " " + I18N.GetString("Upgrade {0} to {1} ...", Global.Version, newVersion));
            ChangeText(I18N.GetString("Downloading file from {0}, You can download it manually and extract to same folder.", downloadURL));
            try
            {
                webClient.Headers.Add(HttpRequestHeader.UserAgent, UA);
                cts.CancelAfter(10000);
                await webClient.DownloadFileTaskAsync(downloadURL, filePath);
            }
            catch
            {
                if (File.Exists(filePath)) File.Delete(filePath);
            }
            if (!File.Exists(filePath))
            {
                downloadURL = $"https://github.com/TkYu/V2RayShell/releases/download/v{newVersion}/V2RayShell{newVersion}.zip";
                ChangeText(I18N.GetString("Downloading file from {0}, You can download it manually and extract to same folder.", downloadURL));
                try
                {
                    await webClient.DownloadFileTaskAsync(downloadURL, filePath);
                }
                catch
                {
                    if (File.Exists(filePath)) File.Delete(filePath);
                }
            }
            if (!File.Exists(filePath))
            {
                if(MessageBox.Show(I18N.GetString("Download fail! Copy link?"), "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    Clipboard.SetText(downloadURL);
                return false;
            }
            ChangeProgress(100);
            ChangeText(newVersion + I18N.GetString("Extracting..."));
            var tempfile = Utils.GetTempPath(Guid.NewGuid().ToString("N"));
            try
            {
                using (var archive = ZipFile.OpenRead(filePath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.FullName.ToLower() == "v2rayshell.exe")
                        {
                            entry.ExtractToFile(tempfile, true);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                if (File.Exists(tempfile)) File.Delete(tempfile);
                return false;
            }
            finally
            {
                File.Delete(filePath);
            }
            if (File.Exists(tempfile))
            {
                Program.ViewControllerInstance.HideTray();
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    Arguments = $"/C @echo off & Title Updating... & echo {I18N.GetString("Sit back and relax")} & taskkill /f /im {System.Diagnostics.Process.GetCurrentProcess().ProcessName}.exe & choice /C Y /N /D Y /T 1 & Del \"{Global.ProcessPath}\" & Move /y \"{tempfile}\" \"{Global.ProcessPath}\" & Start \"\" \"{Global.ProcessPath}\"",
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe"
                };
                System.Diagnostics.Process.Start(psi);
                Environment.Exit(0);
            }
            return true;
        }

        private async Task<bool> DoUpdate()
        {

            ChangeProgress(999);
            ChangeText(I18N.GetString("Get latest version..."));
            var newVersion = await UpdateChecker.GetCoreVersion();
            string proxy = null;
            if (string.IsNullOrEmpty(newVersion))
            {
                proxy = Microsoft.VisualBasic.Interaction.InputBox(I18N.GetString("We need a proxy to download v2ray core"), "Yo", "http://127.0.0.1:1080");
                if (Uri.IsWellFormedUriString(proxy,UriKind.Absolute)) newVersion = await UpdateChecker.GetCoreVersion(proxy);
            }

            if (string.IsNullOrEmpty(newVersion))
            {
                Process.Start(UpdateChecker.V2RAY_URL);
                return false;
            }

            ChangeText(I18N.GetString("Upgrade {0} to {1} ...",V2Ray.Version?.ToString()??"0.0.0",newVersion));
            WebClient webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.UserAgent, UA);
            if (!string.IsNullOrEmpty(proxy) && Uri.IsWellFormedUriString(proxy,UriKind.Absolute))
                webClient.Proxy = new WebProxy(new Uri(proxy));
            cts.Token.Register(() => webClient.CancelAsync());
            webClient.DownloadProgressChanged += (s, e) =>
            {
                ChangeProgress(e.ProgressPercentage);
                //ChangeText(newVersion + I18N.GetString("Downloading...") + $" {e.ProgressPercentage}%");
            };
            var plat = Environment.Is64BitOperatingSystem ? "64" : "32";
            var downloadURL = $"https://github.wuyanzheshui.workers.dev/v2fly/v2ray-core/releases/download/v{newVersion}/v2ray-windows-{plat}.zip";
            ChangeTitle(I18N.GetString("Sit back and relax") + " " + I18N.GetString("Upgrade {0} to {1} ...", V2Ray.Version?.ToString() ?? "0.0.0", newVersion));
            ChangeText(I18N.GetString("Downloading file from {0}, You can download it manually and extract to same folder.", downloadURL));
            try
            {
                //var cl = await GetUrlLen(downloadURL);
                //if (cl > 0)
                //{
                //    if (IsIDMInstalled(out var idm))
                //        await DownloadIDM(idm, downloadURL, fileName);
                //    if (GetFileLen(filePath) != cl)
                //    {
                //        File.Delete(filePath);
                //        cts.CancelAfter(30000);
                //        await webClient.DownloadFileTaskAsync(downloadURL, filePath);
                //    }
                //}
                //else
                //{
                cts.CancelAfter(30000);
                await webClient.DownloadFileTaskAsync(downloadURL, filePath);
                //}
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                if (File.Exists(filePath)) File.Delete(filePath);
            }
            if (!File.Exists(filePath))
            {
                downloadURL = $"https://github.com/v2fly/v2ray-core/releases/download/v{newVersion}/v2ray-windows-{plat}.zip";
                ChangeText(I18N.GetString("Downloading file from {0}, You can download it manually and extract to same folder.", downloadURL));
                try
                {
                    var cl = await GetUrlLen(downloadURL);
                    if (cl>0)
                    {
                        if (IsIDMInstalled(out var idm))
                            await DownloadIDM(idm, downloadURL, fileName);

                        if (GetFileLen(filePath) != cl)
                        {
                            File.Delete(filePath);
                            await webClient.DownloadFileTaskAsync(downloadURL, filePath);
                        }
                    }
                    else
                    {
                        await webClient.DownloadFileTaskAsync(downloadURL, filePath);
                    }
                }
                catch (Exception e)
                {
                    Logging.LogUsefulException(e);
                    if (File.Exists(filePath)) File.Delete(filePath);
                }
            }
            if (!File.Exists(filePath))
            {
                if (MessageBox.Show(I18N.GetString("Download fail! Copy link?"), "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    Clipboard.SetText(downloadURL);
                return false;
            }
            ChangeProgress(100);
            ChangeText(newVersion + I18N.GetString("Extracting..."));
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(filePath))
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
                return false;
            }
            finally
            {
                File.Delete(filePath);
            }
            return true;
        }

        private void textBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                if (IsIDMInstalled(out var idm))
                    Process.Start(idm,"/d " + e.LinkText);
                else
                    Process.Start(e.LinkText);
            }
            catch
            {
                //TODO
            }
        }

        private static async Task DownloadIDM(string idm,string downloadURL,string fileName)
        {
            Process.Start(idm, $"/d {downloadURL} /p \"{Utils.GetTempPath().TrimEnd('\\')}\" /f \"{fileName}\" /s /n");
            var sfileName = fileName.Substring(0, 8);
            var g2g = false;
            for (var i = 0; i < 8; i++)
            {
                if (Process.GetProcessesByName("IDMan").All(pr => !pr.MainWindowTitle.Contains(sfileName)))
                {
                    await Task.Delay(500);
                }
                else
                {
                    g2g = true;
                    break;
                }
            }
            if (!g2g) throw new Exception("Call IDMan Fail!");
            g2g = false;
            for (var i = 0; i < 120; i++)
            {
                if (Process.GetProcessesByName("IDMan").Any(pr => pr.MainWindowTitle.Contains(sfileName)))
                {
                    await Task.Delay(500);
                }
                else
                {
                    g2g = true;
                    break;
                }
            }
            if (!g2g) throw new Exception("Wait IDMan Fail!");
            await Task.Delay(800);
            var wnds = Process.GetProcessesByName("IDMan").Where(pr => !string.IsNullOrEmpty(pr.MainWindowTitle));
            foreach (var wnd in wnds) wnd?.CloseMainWindow();
        }

        private static long GetFileLen(string file)
        {
            if (!File.Exists(file)) return 0;
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return fs.Length;
            }
        }

        private static async Task<long> GetUrlLen(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = UA;
            var response = (HttpWebResponse)await request.GetResponseAsync();
            return long.TryParse(response.Headers[HttpResponseHeader.ContentLength], out var len) ? len : 0;
        }

        [DllImport("shlwapi.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern bool PathFindOnPath([MarshalAs(UnmanagedType.LPTStr)] StringBuilder pszFile, IntPtr unused);

        private bool IsIDMInstalled(out string path)
        {
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"Internet Download Manager\IDMan.exe");
            if (File.Exists(path))
                return true;
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Internet Download Manager\IDMan.exe");
            if (File.Exists(path))
                return true;
            var sb = new StringBuilder("IDMan.exe", 260);
            if (PathFindOnPath(sb, IntPtr.Zero))
            {
                path = sb.ToString();
                return true;
            }
            return false;
        }

        private void DownloadProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
            if (File.Exists(filePath))
            {
                for (int i = 0; i < 6; i++)
                {
                    try
                    {
                        File.Delete(filePath);
                        return;
                    }
                    catch
                    {
                        System.Threading.Thread.Sleep(500);
                    }
                }
            }
        }
    }



}
