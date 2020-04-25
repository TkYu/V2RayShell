using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using V2RayShell.Model;
using V2RayShell.Properties;
using V2RayShell.Services;

namespace V2RayShell.View
{
    public partial class ConfigForm : Form
    {
        private V2RayShellController controller;

        // this is a copy of configuration that we are working on
        private Configuration _modifiedConfiguration;
        private int _lastSelectedIndex = -1;

        public ConfigForm(V2RayShellController controller)
        {
            InitializeComponent();
            PerformLayout();
            UpdateTexts();
            Icon = Resources.v2ray;
            this.controller = controller;
            controller.ConfigChanged += (s, e) =>
            {
                LoadCurrentConfiguration();
            };
            LoadCurrentConfiguration();
        }

        private void UpdateTexts()
        {
            Font = Global.Font;
            AddButton.Text = I18N.GetString("&Add");
            DeleteButton.Text = I18N.GetString("&Delete");
            DuplicateButton.Text = I18N.GetString("Dupli&cate");
            ClipboardButton.Text = I18N.GetString("Cli&pboard");
            IPLabel.Text = I18N.GetString("Server Address");
            ServerPortLabel.Text = I18N.GetString("Server Port");
            IDLabel.Text = I18N.GetString("ID");
            TPLabel.Text = I18N.GetString("Trans Protocol");
            CTLabel.Text = I18N.GetString("Camouflage Type");
            AlterIdLabel.Text = I18N.GetString("AlterId");
            CDLabel.Text = I18N.GetString("Camouflage Domain");
            PathLabel.Text = I18N.GetString("Camouflage Path");
            ProxyPortLabel.Text = I18N.GetString("Proxy Port");
            RemarksLabel.Text = I18N.GetString("Remarks");
            SecurityLabel.Text = I18N.GetString("Security");
            ServerGroupBox.Text = I18N.GetString("Server");
            CorePortLabel.Text = I18N.GetString("Core Port");
            OKButton.Text = I18N.GetString("OK");
            MyCancelButton.Text = I18N.GetString("Cancel");
            MoveUpButton.Text = I18N.GetString("Move &Up");
            MoveDownButton.Text = I18N.GetString("Move D&own");
            this.Text = I18N.GetString("Edit Servers");
        }

        private void LoadCurrentConfiguration()
        {
            _modifiedConfiguration = controller.GetConfigurationCopy();
            LoadConfiguration(_modifiedConfiguration);
            _lastSelectedIndex = _modifiedConfiguration.index;
            if (_lastSelectedIndex < 0 || _lastSelectedIndex >= ServersListBox.Items.Count)
            {
                _lastSelectedIndex = 0;
            }
            ServersListBox.SelectedIndex = _lastSelectedIndex;
            UpdateMoveUpAndDownButton();
            LoadSelectedServer();
        }

        private void UpdateMoveUpAndDownButton()
        {
            MoveUpButton.Enabled = ServersListBox.SelectedIndex != 0;
            MoveDownButton.Enabled = ServersListBox.SelectedIndex != ServersListBox.Items.Count - 1;
        }

        private void LoadSelectedServer()
        {
            if (ServersListBox.SelectedIndex >= 0 && ServersListBox.SelectedIndex < _modifiedConfiguration.configs.Count)
            {
                var server = _modifiedConfiguration.configs[ServersListBox.SelectedIndex];

                LocalPortNum.Value = _modifiedConfiguration.localPort;
                CorePortNum.Value = _modifiedConfiguration.corePort;

                IPTextBox.Text = server.add;
                ServerPortText.Text = server.port.ToString();
                IDTextBox.Text = server.id;
                TPSelect.SelectedItem = server.net ?? "tcp";
                CTSelect.SelectedItem = server.type ?? "none";
                AlterIdText.Text = server.aid.ToString();
                CDTextBox.Text = server.host;
                PathTextBox.Text = server.path;
                RemarksTextBox.Text = server.ps;
                SecuritySelect.SelectedItem = server.tls ?? "";
            }
        }

        private void MoveConfigItem(int step)
        {
            int index = ServersListBox.SelectedIndex;
            var server = _modifiedConfiguration.configs[index];
            object item = ServersListBox.Items[index];

            _modifiedConfiguration.configs.Remove(server);
            _modifiedConfiguration.configs.Insert(index + step, server);
            _modifiedConfiguration.index += step;

            ServersListBox.BeginUpdate();
            ServersListBox.Enabled = false;
            _lastSelectedIndex = index + step;
            ServersListBox.Items.Remove(item);
            ServersListBox.Items.Insert(index + step, item);
            ServersListBox.Enabled = true;
            ServersListBox.SelectedIndex = index + step;
            ServersListBox.EndUpdate();

            UpdateMoveUpAndDownButton();
        }

        private bool SaveOldSelectedServer()
        {
            try
            {
                if (_lastSelectedIndex == -1 || _lastSelectedIndex >= _modifiedConfiguration.configs.Count)
                {
                    return true;
                }
                var server = new ServerObject{v="10"};

                if (Uri.CheckHostName(server.add = IPTextBox.Text.Trim()) == UriHostNameType.Unknown)
                {
                    MessageBox.Show(I18N.GetString("Invalid server address"));
                    IPTextBox.Focus();
                    return false;
                }

                var old = _modifiedConfiguration.configs[_lastSelectedIndex];

                server.port = int.Parse(ServerPortText.Text);
                server.aid = int.Parse(AlterIdText.Text);

                server.ps = RemarksTextBox.Text;
                server.id = IDTextBox.Text;
                server.net = TPSelect.SelectedItem.ToString();
                server.type = CTSelect.SelectedItem.ToString();
                server.host = CDTextBox.Text;
                server.path = PathTextBox.Text;
                server.tls = SecuritySelect?.SelectedItem?.ToString()??"";

                var localPort = (int) LocalPortNum.Value;
                var corePort = (int) CorePortNum.Value;
                Configuration.CheckServer(server.add);
                Configuration.CheckLocalPort(localPort);

                if (old != null) server.group = old.group;

                _modifiedConfiguration.configs[_lastSelectedIndex] = server;
                _modifiedConfiguration.localPort = localPort;
                _modifiedConfiguration.corePort = corePort;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void LoadConfiguration(Configuration configuration)
        {
            ServersListBox.Items.Clear();
            foreach (var server in _modifiedConfiguration.configs)
            {
                ServersListBox.Items.Add(server.ps);
            }
        }

        private void MyCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClipboardButton_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            var txt = Clipboard.GetText(TextDataFormat.Text);
            if (ServerObject.TryParse(txt, out var saba))
            {
                _modifiedConfiguration.configs.Add(saba);
                LoadConfiguration(_modifiedConfiguration);
                ServersListBox.SelectedIndex = _modifiedConfiguration.configs.Count - 1;
                _lastSelectedIndex = ServersListBox.SelectedIndex;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            _lastSelectedIndex = ServersListBox.SelectedIndex;
            if (_lastSelectedIndex >= 0 && _lastSelectedIndex < _modifiedConfiguration.configs.Count)
            {
                _modifiedConfiguration.configs.RemoveAt(_lastSelectedIndex);
            }
            if (_lastSelectedIndex >= _modifiedConfiguration.configs.Count)
            {
                _lastSelectedIndex = _modifiedConfiguration.configs.Count - 1;
            }
            ServersListBox.SelectedIndex = _lastSelectedIndex;
            LoadConfiguration(_modifiedConfiguration);
            ServersListBox.SelectedIndex = _lastSelectedIndex;
            LoadSelectedServer();
        }

        private void ServersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ServersListBox.CanSelect)
            {
                return;
            }
            if (_lastSelectedIndex == ServersListBox.SelectedIndex)
            {
                // we are moving back to oldSelectedIndex or doing a force move
                return;
            }
            if (!SaveOldSelectedServer())
            {
                // why this won't cause stack overflow?
                ServersListBox.SelectedIndex = _lastSelectedIndex;
                return;
            }
            if (_lastSelectedIndex >= 0)
            {
                ServersListBox.Items[_lastSelectedIndex] = _modifiedConfiguration.configs[_lastSelectedIndex].ps;
            }
            UpdateMoveUpAndDownButton();
            LoadSelectedServer();
            _lastSelectedIndex = ServersListBox.SelectedIndex;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            var server = Configuration.GetDefaultServer();
            _modifiedConfiguration.configs.Add(server);
            LoadConfiguration(_modifiedConfiguration);
            ServersListBox.SelectedIndex = _modifiedConfiguration.configs.Count - 1;
            _lastSelectedIndex = ServersListBox.SelectedIndex;
        }

        private void DuplicateButton_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            var currServer = _modifiedConfiguration.configs[_lastSelectedIndex];
            var currIndex = _modifiedConfiguration.configs.IndexOf(currServer);
            _modifiedConfiguration.configs.Insert(currIndex + 1, currServer);
            LoadConfiguration(_modifiedConfiguration);
            ServersListBox.SelectedIndex = currIndex + 1;
            _lastSelectedIndex = ServersListBox.SelectedIndex;
        }

        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            if (ServersListBox.SelectedIndex > 0)
            {
                MoveConfigItem(-1);  // -1 means move backward
            }
        }

        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            if (ServersListBox.SelectedIndex < ServersListBox.Items.Count - 1)
            {
                MoveConfigItem(+1);  // +1 means move forward
            }
        }

        private async void OKButton_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            if (_modifiedConfiguration.configs.Count == 0)
            {
                MessageBox.Show(I18N.GetString("Please add at least one server"));
                return;
            }

            OKButton.Enabled = false;
            OKButton.Text = I18N.GetString("Busy...");
            controller.SaveServers(_modifiedConfiguration.configs, _modifiedConfiguration.localPort,_modifiedConfiguration.corePort);
            await controller.SelectServerIndex(ServersListBox.SelectedIndex);
            Close();
        }

        private void OnlyAllowDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
