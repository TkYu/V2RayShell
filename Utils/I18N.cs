using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using V2RayShell.Properties;

namespace V2RayShell
{
    public class I18N
    {
        protected static Dictionary<string, string> Strings;


        static I18N()
        {
            Strings = new Dictionary<string, string>();

            if (Global.CurrentCulture.StartsWith("zh"))
            {
                string[] lines = Resources.cn.Split('\r','\n');
                foreach (string line in lines)
                {
                    if (line.StartsWith("#"))
                    {
                        continue;
                    }

                    string[] kv = line.Split('=');
                    if (kv.Length == 2)
                    {
                        Strings[kv[0]] = kv[1];
                    }
                }
            }
        }

        public static string GetString(string key)
        {
            return Strings.ContainsKey(key) ? Strings[key] : key;
        }

        public static string GetString(string format,params object[] args)
        {
            return Strings.ContainsKey(format) ? string.Format(Strings[format], args) : string.Format(format, args);
        }
    }
}
