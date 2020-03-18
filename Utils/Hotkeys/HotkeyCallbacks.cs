using System;
using System.Reflection;
using V2RayShell.Services;

namespace V2RayShell.Hotkeys
{
    public class HotkeyCallbacks
    {

        public static void InitInstance(V2RayShellController controller,MenuViewController menu)
        {
            if (Instance != null)
            {
                return;
            }

            Instance = new HotkeyCallbacks(controller, menu);
        }

        /// <summary>
        /// Create hotkey callback handler delegate based on callback name
        /// </summary>
        /// <param name="methodname"></param>
        /// <returns></returns>
        public static Delegate GetCallback(string methodname)
        {
            if (string.IsNullOrEmpty(methodname)) throw new ArgumentException(nameof(methodname));
            MethodInfo dynMethod = typeof(HotkeyCallbacks).GetMethod(methodname,
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);
            return dynMethod == null ? null : Delegate.CreateDelegate(typeof(HotKeys.HotKeyCallBackHandler), Instance, dynMethod);
        }

        #region Singleton 
        private static HotkeyCallbacks Instance { get; set; }

        private readonly V2RayShellController _controller;
        private readonly MenuViewController _menuController;
        private HotkeyCallbacks(V2RayShellController controller,MenuViewController menu)
        {
            _controller = controller;
            _menuController = menu;
        }

        #endregion

        #region Callbacks
        private void AddCurrentChromeURLtoPACCallback()
        {
            var curl = Utils.GetChromeUrl();
            if (!string.IsNullOrEmpty(curl))
            {
                var rule = $"|{curl}";
                Logging.Info("DetectURL:" + curl);
                _menuController.ShowBalloonTip(_controller.InsertUserRule(rule) ? I18N.GetString("user rule has been added") : I18N.GetString("user rule has been removed"), rule);
                return;
            }
            Logging.Info("Can not find any url");
        }
        private void AddCurrentChromeDomaintoPACCallback()
        {
            var curl = Utils.GetChromeUrl();
            if (!string.IsNullOrEmpty(curl))
            {
                var spl = curl.Split('/');
                var rule = $"||{spl[2].ToLower().Replace("www.","")}";
                Logging.Info($"DetectURL:{curl},Domain:{rule}");
                _menuController.ShowBalloonTip(_controller.InsertUserRule(rule) ? I18N.GetString("user rule has been added") : I18N.GetString("user rule has been removed"), rule);
                return;
            }
            Logging.Info("Can not find any url");
        }
        private void ScanQRCallback()
        {
            _menuController?.ScanQR?.PerformClick();
        }

        private void SwitchSystemProxyCallback()
        {
            // bool enabled = _controller.GetConfigurationCopy().enabled;
            // _controller.ToggleEnable(!enabled).GetAwaiter().GetResult();
            _menuController?.enableItem?.PerformClick();
        }

        private void SwitchSystemProxyModeCallback()
        {
            SwitchProxyModeCallback();
        }

        private void SwitchProxyModeCallback()
        {
            //var config = _controller.GetConfigurationCopy();
            if (_menuController.enableItem == null) return;
            if (!_menuController.enableItem.Checked) return;
            var currStatus = _menuController.globalModeItem?.Checked??false;
            //_controller.ToggleGlobal(!currStatus).GetAwaiter().GetResult();
            if(currStatus)
                _menuController.PACModeItem?.PerformClick();
            else
                _menuController.globalModeItem?.PerformClick();
        }

        private void SwitchAllowLanCallback()
        {
            // var status = _controller.GetConfigurationCopy().shareOverLan;
            // _controller.ToggleShareOverLAN(!status).GetAwaiter().GetResult();
            _menuController?.ShareOverLANItem?.PerformClick();
        }

        private void ShowLogsCallback()
        {
            _menuController?.ShowLog?.PerformClick();
        }

        private void ServerMoveUpCallback()
        {
            int currIndex;
            int serverCount;
            GetCurrServerInfo(out currIndex, out serverCount);
            if (currIndex - 1 < 0)
            {
                // revert to last server
                currIndex = serverCount - 1;
            }
            else
            {
                currIndex -= 1;
            }
            _controller.SelectServerIndex(currIndex);
        }

        private void ServerMoveDownCallback()
        {
            int currIndex;
            int serverCount;
            GetCurrServerInfo(out currIndex, out serverCount);
            if (currIndex + 1 == serverCount)
            {
                // revert to first server
                currIndex = 0;
            }
            else
            {
                currIndex += 1;
            }
            _controller.SelectServerIndex(currIndex);
        }

        private void GetCurrServerInfo(out int currIndex, out int serverCount)
        {
            var currConfig = _controller.GetCurrentConfiguration();
            currIndex = currConfig.index;
            serverCount = currConfig.configs.Count;
        }

        #endregion
    }
}
