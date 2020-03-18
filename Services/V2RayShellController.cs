using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using V2RayShell.Model;

namespace V2RayShell.Services
{
    public class V2RayShellController
    {
        public CancellationTokenSource cts = new CancellationTokenSource();
        private Task _ramThread;

        private Configuration _config;

        private GFWListUpdater gfwListUpdater;

        private PACServer pacServer;
        private V2RayRunner v2rayRunner;
        private PrivoxyRunner privoxyRunner;


        #region Events
        public class PathEventArgs : EventArgs
        {
            public string Path;
        }


        public event EventHandler ConfigChanged;
        public event ErrorEventHandler Errored;

        public event EventHandler EnableStatusChanged;
        public event EventHandler EnableGlobalChanged;
        public event EventHandler ShareOverLANStatusChanged;
        public event EventHandler VerboseLoggingStatusChanged;

        public event EventHandler<PathEventArgs> PACFileReadyToOpen;
        public event EventHandler<PathEventArgs> UserRuleFileReadyToOpen;
        public event EventHandler<GFWListUpdater.ResultEventArgs> UpdatePACFromGFWListCompleted;
        public event ErrorEventHandler UpdatePACFromGFWListError;

        protected void ReportError(Exception e)
        {
            Errored?.Invoke(this, new ErrorEventArgs(e));
        }

        #endregion

        public V2RayShellController()
        {
            _config = Configuration.Load();
            StartReleasingMemory();
        }

        #region Method

        #region Configs
        public void CopyPacUrl()
        {
            if (pacServer != null)
                System.Windows.Forms.Clipboard.SetDataObject(pacServer.PacUrl);
        }

        public ServerObject GetCurrentServer()
        {
            return _config.GetCurrentServer();
        }

        // always return copy
        public Configuration GetConfigurationCopy()
        {
            return Configuration.Load();
        }

        // always return current instance
        public Configuration GetCurrentConfiguration()
        {
            return _config;
        }

        private async Task SaveConfig(Configuration newConfig)
        {
            Configuration.Save(newConfig);
            // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
            await ReloadAsync();
        }

        public void SaveServers(List<ServerObject> servers, int localPort, int socksport)
        {
            _config.configs = servers;
            _config.localPort = localPort;
            _config.corePort = socksport;
            Configuration.Save(_config);
        }

        public void SaveSubscribes(List<SubscribeConfig> subscribes)
        {
            _config.subscribes = subscribes;
            Configuration.Save(_config);
        }

        public async Task<bool> AddServerBySSURL(string vmessURL)
        {
            try
            {
                var servers = ServerObject.GetServers(vmessURL);
                if (servers == null || servers.Count == 0) return false;
                foreach (var server in servers)
                {
                    _config.configs.Add(server);
                }
                _config.index = _config.configs.Count - 1;
                await SaveConfig(_config);
                return true;
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                return false;
            }
        }

        public async Task SaveHotkeyConfig(HotkeyConfig newConfig)
        {
            _config.hotkey = newConfig;
            await SaveConfig(_config);
            ConfigChanged?.Invoke(this, new EventArgs());
        }
        #endregion

        #region Toggle
        public async Task ToggleCheckingUpdate(bool enabled)
        {
            _config.autoCheckUpdate = enabled;
            await Task.Delay(100);
            Configuration.Save(_config);
            ConfigChanged?.Invoke(this, new EventArgs());
        }

        public async Task ToggleEnable(bool enabled)
        {
            _config.enabled = enabled;
            await SaveConfig(_config);
            EnableStatusChanged?.Invoke(this, new EventArgs());
        }

        public async Task ToggleGlobal(bool global)
        {
            _config.global = global;
            await SaveConfig(_config);
            EnableGlobalChanged?.Invoke(this, new EventArgs());
        }

        public async Task ToggleShareOverLAN(bool enabled)
        {
            _config.shareOverLan = enabled;
            await SaveConfig(_config);
            ShareOverLANStatusChanged?.Invoke(this, new EventArgs());
        }

        public async Task ToggleVerboseLogging(bool enabled)
        {
            _config.isVerboseLogging = enabled;
            await SaveConfig(_config);
            VerboseLoggingStatusChanged?.Invoke(this, new EventArgs());
        }

        private void pacServer_PACFileChanged(object sender, EventArgs e)
        {
            UpdateSystemProxy();
        }

        private void pacServer_UserRuleFileChanged(object sender, EventArgs e)
        {
            GFWListUpdater.MergeAndWritePACFile(null);
        }
        #endregion

        public bool InsertUserRule(string rule)
        {
            var ur = File.Exists(PACServer.USER_RULE_FILE) ? File.ReadAllText(PACServer.USER_RULE_FILE, System.Text.Encoding.UTF8) : Properties.Resources.user_rule;
            if (ur.Contains(rule))
            {
                File.WriteAllText(PACServer.USER_RULE_FILE, ur.Replace($"\r\n{rule}", ""));
                return false;
            }
            ur += $"\r\n{rule}";
            File.WriteAllText(PACServer.USER_RULE_FILE, ur);
            return true;
        }

        public void UpdatePACFromGFWList()
        {
            gfwListUpdater?.UpdatePACFromGFWList(_config);
        }

        private void pacServer_PACUpdateCompleted(object sender, GFWListUpdater.ResultEventArgs e)
        {
            UpdatePACFromGFWListCompleted?.Invoke(this, e);
        }

        private void pacServer_PACUpdateError(object sender, ErrorEventArgs e)
        {
            UpdatePACFromGFWListError?.Invoke(this, e);
        }

        public void TouchPACFile()
        {
            string pacFilename = PACServer.TouchPACFile();
            PACFileReadyToOpen?.Invoke(this, new PathEventArgs() { Path = pacFilename });
        }

        public void TouchUserRuleFile()
        {
            string userRuleFilename = PACServer.TouchUserRuleFile();
            UserRuleFileReadyToOpen?.Invoke(this, new PathEventArgs() { Path = userRuleFilename });
        }


        private bool _systemProxyIsDirty;

        private void UpdateSystemProxy()
        {
            if (_config.enabled)
            {
                SystemProxy.Update(_config, pacServer.RunningPort, false);
                _systemProxyIsDirty = true;
            }
            else
            {
                // only switch it off if we have switched it on
                if (_systemProxyIsDirty)
                {
                    SystemProxy.Update(_config, pacServer.RunningPort, false);
                    _systemProxyIsDirty = false;
                }
            }
        }

        public void RestartCore()
        {
            v2rayRunner?.Stop();
            v2rayRunner?.Start(_config);
        }

        protected async Task ReloadAsync()
        {
            _config = await Configuration.LoadAsync();
            if (_config.isDefault) return;
            if (privoxyRunner == null) privoxyRunner = new PrivoxyRunner();
            if (v2rayRunner == null) v2rayRunner = new V2RayRunner();

            if (pacServer == null)
            {
                pacServer = new PACServer();
                pacServer.PACFileChanged += pacServer_PACFileChanged;
                pacServer.UserRuleFileChanged += pacServer_UserRuleFileChanged;
            }
            if (gfwListUpdater == null)
            {
                gfwListUpdater = new GFWListUpdater();
                gfwListUpdater.UpdateCompleted += pacServer_PACUpdateCompleted;
                gfwListUpdater.Error += pacServer_PACUpdateError;
            }
            pacServer.UpdatePACURL(_config);
            pacServer.Stop();
            privoxyRunner.Stop();
            v2rayRunner.Stop();
            try
            {
                pacServer.Start(_config);
                privoxyRunner.Start(_config);
                v2rayRunner.Start(_config);
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                ReportError(e);
            }
            ConfigChanged?.Invoke(this, new EventArgs());
            UpdateSystemProxy();
            Utils.ReleaseMemory(true);
        }

        public Task StartAsync()
        {
            return ReloadAsync();
        }

        private bool stopped = false;
        public async Task StopAsync()
        {
            if (stopped)
            {
                return;
            }
            stopped = true;
            await Task.Delay(10);
            v2rayRunner?.Stop();
            privoxyRunner?.Stop();
            pacServer?.Stop();
            if (_config.enabled)
            {
                SystemProxy.Update(_config, 0, true);
            }

        }
        public Task SelectServerIndex(int index)
        {
            _config.index = index;
            return SaveConfig(_config);
        }

        private void StartReleasingMemory()
        {
            _ramThread = Task.Run(() =>
            {
                while (!cts.IsCancellationRequested)
                {
                    Utils.ReleaseMemory(false);
                    Thread.Sleep(30 * 1000);
                }
            },cts.Token);
        }

        #endregion
    }
}
