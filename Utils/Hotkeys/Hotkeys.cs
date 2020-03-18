using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using V2RayShell.Model;
using V2RayShell.Services;

namespace V2RayShell.Hotkeys
{
    public static class HotKeys
    {
        private static HotKeyManager _hotKeyManager;

        public delegate void HotKeyCallBackHandler();
        // map key and corresponding handler function
        private static Dictionary<HotKey, HotKeyCallBackHandler> _keymap = new Dictionary<HotKey, HotKeyCallBackHandler>();

        public static void Init(V2RayShellController controller, MenuViewController menu)
        {
            _hotKeyManager = new HotKeyManager();
            _hotKeyManager.KeyPressed += HotKeyManagerPressed;

            HotkeyCallbacks.InitInstance(controller, menu);

            var keys = controller.GetConfigurationCopy()?.hotkey ?? new HotkeyConfig();
            LoadConfiguration(keys);
        }

        private static void LoadConfiguration(HotkeyConfig config)
        {
            foreach (var pi in config.GetType().GetProperties())
            {
                var pv = pi.GetValue(config,null);
                if (pv is string str)
                {
                    var hotkey = Str2HotKey(str);
                    if (hotkey != null)
                    {
                        var callbackName = pi.Name + "Callback";
                        if (HotkeyCallbacks.GetCallback(callbackName) is HotKeyCallBackHandler callback)
                        {
                            if (IsCallbackExists(callback, out var prevHotKey))
                                UnRegist(prevHotKey);
                            var regResult = Regist(hotkey, callback);
                            Logging.Info($"Reg {str} to {pi.Name}{(regResult ? "Successful" : "Fail")}");
                        }
                    }
                }
            }
        }

        public static void Destroy()
        {
            _hotKeyManager.KeyPressed -= HotKeyManagerPressed;
            _hotKeyManager.Dispose();
        }

        private static void HotKeyManagerPressed(object sender, KeyPressedEventArgs e)
        {
            var hotkey = e.HotKey;
            if (_keymap.TryGetValue(hotkey, out var callback))
                callback();
        }

        public static bool IsHotkeyExists( HotKey hotKey )
        {
            if (hotKey == null) throw new ArgumentNullException(nameof(hotKey));
            return _keymap.Any( v => v.Key.Equals( hotKey ) );
        }

        public static bool IsCallbackExists( HotKeyCallBackHandler cb, out HotKey hotkey)
        {
            if (cb == null) throw new ArgumentNullException(nameof(cb));
            try
            {
                var key = _keymap.First(x => x.Value == cb).Key;
                hotkey = key;
                return true;
            }
            catch (InvalidOperationException)
            {
                hotkey = null;
                return false;
            }
        }
        public static string HotKey2Str( HotKey key )
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            return HotKey2Str( key.Key, key.Modifiers );
        }

        public static string HotKey2Str( Key key, ModifierKeys modifier )
        {
            if (!Enum.IsDefined(typeof(Key), key))
                throw new InvalidEnumArgumentException(nameof(key), (int) key, typeof(Key));
            try
            {
                ModifierKeysConverter mkc = new ModifierKeysConverter();
                var keyStr = Enum.GetName(typeof(Key), key);
                var modifierStr = mkc.ConvertToInvariantString(modifier);

                return $"{modifierStr}+{keyStr}";
            }
            catch (NotSupportedException)
            {
                return null;
            }
        }

        public static HotKey Str2HotKey(string s)
        {
            try
            {
                if (string.IsNullOrEmpty(s)) return null;
                int offset = s.LastIndexOf("+", StringComparison.OrdinalIgnoreCase);
                if (offset <= 0) return null;
                string modifierStr = s.Substring(0, offset).Trim();
                string keyStr = s.Substring(offset + 1).Trim();

                KeyConverter kc = new KeyConverter();
                ModifierKeysConverter mkc = new ModifierKeysConverter();

                // ReSharper disable once PossibleNullReferenceException
                Key key = (Key) kc.ConvertFrom(keyStr.ToUpper());
                // ReSharper disable once PossibleNullReferenceException
                ModifierKeys modifier = (ModifierKeys) mkc.ConvertFrom(modifierStr.ToUpper());

                return new HotKey(key, modifier);
            }
            catch (NotSupportedException)
            {
                return null;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public static bool Regist( HotKey key, HotKeyCallBackHandler callBack )
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (callBack == null)
                throw new ArgumentNullException(nameof(callBack));
            try
            {
                _hotKeyManager.Register(key);
                _keymap[key] = callBack;
                return true;
            }
            catch (ArgumentException)
            {
                return true;
            }
            catch (Win32Exception)
            {
                return false;
            }
        }

        public static bool Regist(Key key, ModifierKeys modifiers, HotKeyCallBackHandler callBack)
        {
            if (!Enum.IsDefined(typeof(Key), key))
                throw new InvalidEnumArgumentException(nameof(key), (int) key, typeof(Key));
            try
            {
                var hotkey = _hotKeyManager.Register(key, modifiers);
                _keymap[hotkey] = callBack;
                return true;
            }
            catch (ArgumentException)
            {
                return true;
            }
            catch (Win32Exception)
            {
                return false;
            }
        }

        public static void UnRegist(HotKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            _hotKeyManager.Unregister(key);
            if(_keymap.ContainsKey(key))
                _keymap.Remove(key);
        }
    }
}