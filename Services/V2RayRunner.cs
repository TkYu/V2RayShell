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
    class V2RayRunner
    {
        private static readonly string _uniqueConfigFile;
        private static readonly Job _v2rayJob;
        private Process _process;
        static V2RayRunner()
        {
            try
            {
                _uniqueConfigFile = $"V2RayShell_{Global.PathHash}.json";
                _v2rayJob = new Job();
            }
            catch (IOException e)
            {
                Logging.LogUsefulException(e);
            }
        }

        public static void KillAll()
        {
            var existingPrivoxy = Process.GetProcessesByName(V2Ray.V2RAY_CORE_WITHOUTEXT);
            foreach (var p in existingPrivoxy.Where(IsChildProcess)) p.KillProcess();
        }

        public void Start(Configuration configuration)
        {
            if (_process != null) return;
            KillAll();
            var config = Utils.GetTempPath(_uniqueConfigFile);
            File.WriteAllText(config,V2Ray.GenerateVNextConf(configuration.GetCurrentServer(),configuration.corePort,configuration.shareOverLan ? "0.0.0.0" : "127.0.0.1"));
            _process = new Process
            {
                StartInfo =
                {
                    FileName = V2Ray.V2RAY_CORE,
                    Arguments = $"-config {config}",
                    WorkingDirectory = Global.AppPath,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = true,
                    CreateNoWindow = true
                }
            };
            _process.Start();
            _v2rayJob.AddProcess(_process.Handle);
        }

        public void Stop()
        {
            if (_process == null) return;
            _process.KillProcess();
            _process.Dispose();
            _process = null;
        }


        private static bool IsChildProcess(Process process)
        {
            try
            {
                //v2ray.exe  -config _uniqueConfigFile
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
