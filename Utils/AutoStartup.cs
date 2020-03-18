using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace V2RayShell
{
    class AutoStartup
    {
        private const string KEY_NAME = "V2RayShell";
        public static bool Set(bool enabled)
        {
            try
            {
                RegistryKey runKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if (enabled)
                {
                    runKey?.SetValue(KEY_NAME, Global.ProcessPath);
                }
                else
                {
                    runKey?.DeleteValue(KEY_NAME);
                }
                runKey?.Close();
                return true;
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                return false;
            }
        }

        public static bool Check()
        {
            try
            {
                RegistryKey runKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                string[] runList = runKey?.GetValueNames();
                runKey?.Close();
                if (runList == null || runList.Length == 0) return false;
                foreach (string item in runList)
                {
                    if (item.Equals(KEY_NAME))
                        return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                return false;
            }
        }
    }
}
