using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace V2RayShell
{
    public static class Global
    {
        public static readonly string AppPath;
        public static readonly string Version;
        public static readonly string ConfigPath;
        public static readonly string ProcessPath;
        public static readonly string ProcessName;
        public static readonly int PathHash;
        static Global()
        {
            Version = Application.ProductVersion;
            ProcessPath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;
            PathHash = ProcessPath.GetHashCode();
            AppPath = Path.GetDirectoryName(ProcessPath);
            ProcessName = Path.GetFileNameWithoutExtension(ProcessPath);
            ConfigPath = Utils.GetTempPath($"V2RayShell_{Application.StartupPath.GetHashCode()}.json");
        }
    }
}
