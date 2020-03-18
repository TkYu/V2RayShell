using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2RayShell.Model
{
    [Serializable]
    public class HotkeyConfig
    {
        public string SwitchSystemProxy {get; set; }
        public string SwitchSystemProxyMode { get; set; }
        public string SwitchAllowLan { get; set; }
        public string ShowLogs { get; set; }
        public string ServerMoveUp { get; set; }
        public string ServerMoveDown { get; set; }

        public string AddCurrentChromeURLtoPAC { get; set; }
        public string AddCurrentChromeDomaintoPAC { get; set; }
        public string ScanQR { get; set; }

        public HotkeyConfig()
        {
            SwitchSystemProxy = "";
            SwitchSystemProxyMode = "";
            SwitchAllowLan = "";
            ShowLogs = "";
            ServerMoveUp = "";
            ServerMoveDown = "";

            AddCurrentChromeDomaintoPAC = "";
            AddCurrentChromeURLtoPAC = "";
            ScanQR = "";
        }
    }
}
