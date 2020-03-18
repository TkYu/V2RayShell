using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2RayShell.Model;
using V2RayShell.Properties;

namespace V2RayShell.Services
{
    class PrivoxyRunner
    {
        public const string PROCESS_NAME = "v2rayshell_privoxy";
        public const string PROCESS_NAME_WITHEXT = PROCESS_NAME + ".exe";
        private static readonly string _uniqueConfigFile;
        private static readonly Job _privoxyJob;
        private Process _process;

        static PrivoxyRunner()
        {
            try
            {
                _uniqueConfigFile = $"v2rayshell_privoxy_{Global.PathHash}.conf";
                _privoxyJob = new Job();
                FileManager.UncompressFile(Utils.GetTempPath(PROCESS_NAME_WITHEXT), Resources.privoxy_exe);
            }
            catch (IOException e)
            {
                Logging.LogUsefulException(e);
            }
        }

        public void Start(Configuration configuration)
        {
            if (_process == null)
            {
                var existingPrivoxy = Process.GetProcessesByName(PROCESS_NAME);
                foreach (var p in existingPrivoxy.Where(IsChildProcess)) p.KillProcess();
                var privoxyConfig = Resources.privoxy_conf;
                privoxyConfig = privoxyConfig.Replace("__SOCKS_PORT__", configuration.corePort.ToString());
                privoxyConfig = privoxyConfig.Replace("__PRIVOXY_BIND_PORT__", configuration.localPort.ToString());
                var isIPv6Enabled = false;
                privoxyConfig = isIPv6Enabled
                    ? privoxyConfig.Replace("__PRIVOXY_BIND_IP__", configuration.shareOverLan ? "[::]" : "[::1]")
                        .Replace("__SOCKS_HOST__", "[::1]")
                    : privoxyConfig.Replace("__PRIVOXY_BIND_IP__", configuration.shareOverLan ? "0.0.0.0" : "127.0.0.1")
                        .Replace("__SOCKS_HOST__", "127.0.0.1");

                File.WriteAllText(Utils.GetTempPath(_uniqueConfigFile),privoxyConfig);
                _process = new Process
                {
                    StartInfo =
                    {
                        FileName = PROCESS_NAME_WITHEXT,
                        Arguments = _uniqueConfigFile,
                        WorkingDirectory = Utils.GetTempPath(),
                        WindowStyle = ProcessWindowStyle.Hidden,
                        UseShellExecute = true,
                        CreateNoWindow = true
                    }
                };
                _process.Start();
                _privoxyJob.AddProcess(_process.Handle);
            }
        }

        public void Stop()
        {
            if (_process != null)
            {
                _process.KillProcess();
                _process.Dispose();
                _process = null;
            }
        }

        private static bool IsChildProcess(Process process)
        {
            try
            {
                var cmd = process.GetCommandLine();
                return cmd.Contains(_uniqueConfigFile);
            }
            catch (Exception ex)
            {
                Logging.LogUsefulException(ex);
                return false;
            }
        }
    }
}
