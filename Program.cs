using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using V2RayShell.Services;
using V2RayShell.View;

namespace V2RayShell
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Utils.ReleaseMemory(true);
            using (Mutex mutex = new Mutex(false, "Global\\V2RayShell_" + Global.PathHash))
            {
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += Application_ThreadException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Application.ApplicationExit += Application_ApplicationExit;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var processesName = Process.GetCurrentProcess().MainModule?.ModuleName ?? "V2RayShell.exe";
                var processesNameWithoutExtension = Path.GetFileNameWithoutExtension(processesName);
                if (!mutex.WaitOne(0, false))
                {
                    Process[] oldProcesses = Process.GetProcessesByName(processesNameWithoutExtension);
                    if (oldProcesses.Length > 0)
                    {
                        Process oldProcess = oldProcesses[0];
                    }
                    MessageBox.Show(I18N.GetString("Find V2RayShell icon in your notify tray.") + "\n" +
                                    I18N.GetString("If you want to start multiple V2RayShell, make a copy in another directory."),
                        I18N.GetString("V2RayShell is already running."));
                    return;
                }

                Directory.SetCurrentDirectory(Global.AppPath);

                Logging.OpenLogFile();
                //check
                if (V2Ray.CoreExsis)
                {
                    if (V2Ray.Version != null)
                    {
                        Logging.Info("V2RayCore version : " + V2Ray.Version);
                    }
                    else
                    {
                        throw new Exception(I18N.GetString("v2ray.exe -version parse fail!"));
                    }
                }
                else
                {
                    //need do sth
                    var download = new DownloadProgress();
                    var result = download.ShowDialog();
                    if (result == DialogResult.Abort || result == DialogResult.Cancel)
                    {
                        MessageBox.Show(I18N.GetString("download fail!"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Logging.Info("V2RayCore version : " + V2Ray.Version);
                }

                var controller = new V2RayShellController();
                var viewController = new MenuViewController(controller);
                Hotkeys.HotKeys.Init(controller, viewController);
                controller.StartAsync();
                Application.Run();
            }
        }

        #region HandleExceptions

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string errMsg = e.ExceptionObject.ToString();
            Logging.Error(errMsg);
            MessageBox.Show(
                $"{I18N.GetString("Unexpected error, V2RayShell will exit. Please report to")} https://github.com/TkYu/V2RayShell/issues {Environment.NewLine}{errMsg}",
                "V2RayShell non-UI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string errorMsg = $"Exception Detail: {Environment.NewLine}{e.Exception}";
            Logging.Error(errorMsg);
            MessageBox.Show(
                $"{I18N.GetString("Unexpected error, V2RayShell will exit. Please report to")} https://github.com/TkYu/V2RayShell/issues {Environment.NewLine}{errorMsg}",
                "V2RayShell UI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }


        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Application.ApplicationExit -= Application_ApplicationExit;
            Application.ThreadException -= Application_ThreadException;
            try
            {
                Hotkeys.HotKeys.Destroy();
            }
            catch
            {
                //TODO
            }
        }

        #endregion
    }
}
