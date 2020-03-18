using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;

namespace V2RayShell.Hotkeys
{
    /// <summary>
    /// Setups system-wide hot keys and provides possibility to react on their events.
    /// </summary>
    public class HotKeyManager : IDisposable
    {
        private readonly HwndSource _windowHandleSource;

        private readonly Dictionary<HotKey, int> _registered;

        /// <summary>
        /// Occurs when registered system-wide hot key is pressed.
        /// </summary>
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotKeyManager"/> class.
        /// </summary>
        public HotKeyManager()
        {
            _windowHandleSource = new HwndSource(new HwndSourceParameters());
            _windowHandleSource.AddHook(messagesHandler);

            _registered = new Dictionary<HotKey, int>();
        }

        /// <summary>
        /// Registers the system-wide hot key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="modifiers">The key modifiers.</param>
        /// <returns>The registered <see cref="HotKey"/>.</returns>
        public HotKey Register(Key key, ModifierKeys modifiers)
        {
            var hotKey = new HotKey(key, modifiers);
            Register(hotKey);
            return hotKey;
        }

        /// <summary>
        /// Registers the system-wide hot key.
        /// </summary>
        /// <param name="hotKey">The hot key.</param>
        public void Register(HotKey hotKey)
        {
            // Check if specified hot key is already registered.
            if (_registered.ContainsKey(hotKey))
                throw new ArgumentException("The specified hot key is already registered.");

            // Register new hot key.
            var id = getFreeKeyId();
            if (!WinApi.RegisterHotKey(_windowHandleSource.Handle, id, hotKey.Key, hotKey.Modifiers))
                throw new Win32Exception(Marshal.GetLastWin32Error(), "Can't register the hot key.");

            _registered.Add(hotKey, id);
        }

        /// <summary>
        /// Unregisters previously registered hot key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="modifiers">The key modifiers.</param>
        public void Unregister(Key key, ModifierKeys modifiers)
        {
            var hotKey = new HotKey(key, modifiers);
            Unregister(hotKey);
        }

        /// <summary>
        /// Unregisters previously registered hot key.
        /// </summary>
        /// <param name="hotKey">The registered hot key.</param>
        public void Unregister(HotKey hotKey)
        {
            if (_registered.TryGetValue(hotKey, out var id))
            {
                WinApi.UnregisterHotKey(_windowHandleSource.Handle, id);
                _registered.Remove(hotKey);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Unregister hot keys.
            foreach (var hotKey in _registered)
            {
                WinApi.UnregisterHotKey(_windowHandleSource.Handle, hotKey.Value);
            }

            _windowHandleSource.RemoveHook(messagesHandler);
            _windowHandleSource.Dispose();
            //System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(() => _windowHandleSource.Dispose());
        }

        private int getFreeKeyId()
        {
            return _registered.Any() ? _registered.Values.Max() + 1 : 0;
        }

        private IntPtr messagesHandler(IntPtr handle, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (message == WinApi.WmHotKey)
            {
                // Extract key and modifiers from the message.
                var key = KeyInterop.KeyFromVirtualKey(((int) lParam >> 16) & 0xFFFF);
                var modifiers = (ModifierKeys) ((int) lParam & 0xFFFF);

                var hotKey = new HotKey(key, modifiers);
                onKeyPressed(new KeyPressedEventArgs(hotKey));

                handled = true;
                return new IntPtr(1);
            }

            return IntPtr.Zero;
        }

        private void onKeyPressed(KeyPressedEventArgs e)
        {
            var handler = KeyPressed;
            handler?.Invoke(this, e);
        }
    }

    #region HotKey

    /// <summary>
    /// Represents system-wide hot key.
    /// </summary>
    public class HotKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HotKey"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="modifiers">The key modifiers.</param>
        public HotKey(Key key, ModifierKeys modifiers)
        {
            Key = key;
            Modifiers = modifiers;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HotKey"/> class.
        /// </summary>
        public HotKey()
        {
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public Key Key { get; set; }

        /// <summary>
        /// Gets or sets the key modifiers.
        /// </summary>
        /// <value>
        /// The key modifiers.
        /// </value>
        public ModifierKeys Modifiers { get; set; }

        #region Equality members

        /// <summary>
        /// Determines whether the specified <see cref="HotKey"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="HotKey"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="HotKey"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(HotKey other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Equals(other.Key, Key) && Equals(other.Modifiers, Modifiers);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == typeof(HotKey) && Equals((HotKey)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Key.GetHashCode() * 397) ^ Modifiers.GetHashCode();
            }
        }

        #endregion
    }

    #endregion


    #region WinApi

    internal class WinApi
    {
        /// <summary>
        /// Registers a system-wide hot key.
        /// </summary>
        /// <param name="handle">The handle of the window that will process hot key messages.</param>
        /// <param name="id">The hot key ID.</param>
        /// <param name="key">The key.</param>
        /// <param name="modifiers">The key modifiers.</param>
        /// <returns><c>true</c> if hot key was registered; otherwise, <c>false</c>.</returns>
        public static bool RegisterHotKey(IntPtr handle, int id, Key key, ModifierKeys modifiers)
        {
            var virtualCode = KeyInterop.VirtualKeyFromKey(key);
            return RegisterHotKey(handle, id, (uint) modifiers, (uint) virtualCode);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr handle, int id, uint modifiers, uint virtualCode);

        /// <summary>
        /// Unregisters previously registered system-wide hot key.
        /// </summary>
        /// <param name="handle">The window handle.</param>
        /// <param name="id">The hot key ID.</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr handle, int id);

        /// <summary>
        /// The message code posted when the user presses a hot key.
        /// </summary>
        public static int WmHotKey = 0x0312;
    }

    #endregion


    #region KeyPressedEventArgs

    /// <summary>
    /// Arguments for key pressed event which contain information about pressed hot key.
    /// </summary>
    public class KeyPressedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyPressedEventArgs"/> class.
        /// </summary>
        /// <param name="hotKey">The hot key.</param>
        public KeyPressedEventArgs(HotKey hotKey)
        {
            HotKey = hotKey;
        }

        /// <summary>
        /// Gets the hot key.
        /// </summary>
        public HotKey HotKey { get; private set; }
    }

    #endregion
}