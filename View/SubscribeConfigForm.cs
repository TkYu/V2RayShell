using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using V2RayShell.Model;
using V2RayShell.Properties;
using V2RayShell.Services;

namespace V2RayShell.View
{
public partial class SubscribeConfigForm : Form
    {
        private V2RayShellController _controller;

        // this is a copy of configuration that we are working on
        private Configuration _modifiedConfiguration;
        private int _lastSelectedIndex = -1;
        public SubscribeConfigForm()
        {
            InitializeComponent();
        }

        public SubscribeConfigForm(V2RayShellController controller)
        {
            InitializeComponent();
            UpdateTexts();
            Icon = Resources.v2ray;
            _controller = controller;
            _controller.ConfigChanged += controller_ConfigChanged;
            LoadCurrentConfiguration();
        }

        private void UpdateTexts()
        {
            Font = Global.Font;
            AddButton.Text = I18N.GetString("&Add");
            DeleteButton.Text = I18N.GetString("&Delete");
            SubscribeGroupBox.Text = I18N.GetString("Subscribe");
            NameLabel.Text = I18N.GetString("Subscribe Name");
            UrlLabel.Text = I18N.GetString("Url");
            UseProxyCheckBox.Text = I18N.GetString("UseProxy");
            OKButton.Text = I18N.GetString("OK");
            MyCancelButton.Text = I18N.GetString("Cancel");
            this.Text = I18N.GetString("Edit Subscriptions");
        }

        private void controller_ConfigChanged(object sender, EventArgs e)
        {
            LoadCurrentConfiguration();
        }

        private void LoadCurrentConfiguration()
        {
            _modifiedConfiguration = _controller.GetConfigurationCopy();
            LoadConfiguration(_modifiedConfiguration);
            _lastSelectedIndex = _modifiedConfiguration.subscribes.Count-1;
            if (_lastSelectedIndex < 0 || _lastSelectedIndex >= SubscribeListBox.Items.Count)
            {
                _lastSelectedIndex = -1;
            }
            SubscribeListBox.SelectedIndex = _lastSelectedIndex;
            LoadSelectedItem();
        }
        private void LoadConfiguration(Configuration configuration)
        {
            SubscribeListBox.Items.Clear();
            foreach (SubscribeConfig server in _modifiedConfiguration.subscribes)
            {
                SubscribeListBox.Items.Add(server.name);
            }
        }

        Dictionary<int,string> oldNames = new Dictionary<int, string>();
        private bool SaveOld()
        {
            try
            {
                if (_lastSelectedIndex == -1 || _lastSelectedIndex >= _modifiedConfiguration.subscribes.Count)
                {
                    return true;
                }
                var item = new SubscribeConfig{name = NameTextBox.Text,url = UrlTextBox.Text,useProxy = UseProxyCheckBox.Checked};
                if (!oldNames.ContainsKey(_lastSelectedIndex))
                    oldNames.Add(_lastSelectedIndex, _modifiedConfiguration.subscribes[_lastSelectedIndex].name);
                else
                    oldNames[_lastSelectedIndex] = _modifiedConfiguration.subscribes[_lastSelectedIndex].name;
                _modifiedConfiguration.subscribes[_lastSelectedIndex] = item;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void LoadSelectedItem()
        {
            if (SubscribeListBox.SelectedIndex >= 0 && SubscribeListBox.SelectedIndex < _modifiedConfiguration.subscribes.Count)
            {
                var item = _modifiedConfiguration.subscribes[SubscribeListBox.SelectedIndex];
                NameTextBox.Text = item.name;
                UrlTextBox.Text = item.url;
                UseProxyCheckBox.Checked = item.useProxy;
            }
        }

        private async void OKButton_Click(object sender, EventArgs e)
        {
            if (!SaveOld())
            {
                return;
            }

            OKButton.Enabled = false;
            OKButton.Text = I18N.GetString("Busy...");

            try
            {
                _controller.SaveSubscribes(_modifiedConfiguration.subscribes);
                if (_lastSelectedIndex > -1)
                {
                    var item = _modifiedConfiguration.subscribes[_lastSelectedIndex];
                    var wc = new WebClient();
                    if (item.useProxy) wc.Proxy = new WebProxy(IPAddress.Loopback.ToString(), _modifiedConfiguration.localPort);
                    var downloadString = await wc.DownloadStringTaskAsync(item.url);
                    wc.Dispose();
                    var debase64 = Encoding.UTF8.GetString(Convert.FromBase64String(downloadString));
                    var split = debase64.Split('\r', '\n');
                    var lst = new List<ServerObject>();
                    foreach (var s in split)
                    {
                        if (ServerObject.TryParse(s, out ServerObject svc))
                        {
                            svc.@group = item.name;
                            lst.Add(svc);
                        }
                    }
                    if (lst.Any())
                    {
                        _modifiedConfiguration.configs.RemoveAll(c => c.@group == oldNames[_lastSelectedIndex]);
                        _modifiedConfiguration.configs.AddRange(lst);
                    }
                    _controller.SaveServers(_modifiedConfiguration.configs, _modifiedConfiguration.localPort,_modifiedConfiguration.corePort);
                }
            }
            catch (Exception exception)
            {
                Logging.LogUsefulException(exception);
            }
            Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var item = new SubscribeConfig{name = "New"};
            _modifiedConfiguration.subscribes.Add(item);
            LoadConfiguration(_modifiedConfiguration);
            SubscribeListBox.SelectedIndex = _modifiedConfiguration.subscribes.Count - 1;
            _lastSelectedIndex = SubscribeListBox.SelectedIndex;
        }

        private void SubscribeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!SubscribeListBox.CanSelect)
            {
                return;
            }
            if (_lastSelectedIndex == SubscribeListBox.SelectedIndex)
            {
                return;
            }
            if (!SaveOld())
            {
                SubscribeListBox.SelectedIndex = _lastSelectedIndex;
                return;
            }
            if (_lastSelectedIndex >= 0)
            {
                SubscribeListBox.Items[_lastSelectedIndex] = _modifiedConfiguration.subscribes[_lastSelectedIndex].name;
            }
            LoadSelectedItem();
            _lastSelectedIndex = SubscribeListBox.SelectedIndex;
        }

        private void MyCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            _lastSelectedIndex = SubscribeListBox.SelectedIndex;
            if (_lastSelectedIndex >= 0 && _lastSelectedIndex < _modifiedConfiguration.subscribes.Count)
            {
                _modifiedConfiguration.subscribes.RemoveAt(_lastSelectedIndex);
            }
            if (_lastSelectedIndex >= _modifiedConfiguration.subscribes.Count)
            {
                _lastSelectedIndex = _modifiedConfiguration.subscribes.Count - 1;
            }
            SubscribeListBox.SelectedIndex = _lastSelectedIndex;
            LoadConfiguration(_modifiedConfiguration);
            SubscribeListBox.SelectedIndex = _lastSelectedIndex;
            LoadSelectedItem();
        }
    }
}
