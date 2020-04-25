namespace V2RayShell.View
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.OKButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.ServerGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CTSelect = new System.Windows.Forms.ComboBox();
            this.AlterIdLabel = new System.Windows.Forms.Label();
            this.RemarksTextBox = new System.Windows.Forms.TextBox();
            this.IPLabel = new System.Windows.Forms.Label();
            this.ServerPortLabel = new System.Windows.Forms.Label();
            this.IDLabel = new System.Windows.Forms.Label();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.TPLabel = new System.Windows.Forms.Label();
            this.TPSelect = new System.Windows.Forms.ComboBox();
            this.SecurityLabel = new System.Windows.Forms.Label();
            this.CTLabel = new System.Windows.Forms.Label();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.PathLabel = new System.Windows.Forms.Label();
            this.RemarksLabel = new System.Windows.Forms.Label();
            this.IDTextBox = new System.Windows.Forms.MaskedTextBox();
            this.CDLabel = new System.Windows.Forms.Label();
            this.CDTextBox = new System.Windows.Forms.TextBox();
            this.SecuritySelect = new System.Windows.Forms.ComboBox();
            this.ServersListBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.MoveDownButton = new System.Windows.Forms.Button();
            this.MoveUpButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.ProxyPortLabel = new System.Windows.Forms.Label();
            this.LocalPortNum = new System.Windows.Forms.NumericUpDown();
            this.CorePortLabel = new System.Windows.Forms.Label();
            this.CorePortNum = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.DuplicateButton = new System.Windows.Forms.Button();
            this.ClipboardButton = new System.Windows.Forms.Button();
            this.ServerPortText = new System.Windows.Forms.TextBox();
            this.AlterIdText = new System.Windows.Forms.TextBox();
            this.ServerGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocalPortNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CorePortNum)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Location = new System.Drawing.Point(404, 374);
            this.panel2.Margin = new System.Windows.Forms.Padding(6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0, 0);
            this.panel2.TabIndex = 1;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.OKButton.Location = new System.Drawing.Point(6, 6);
            this.OKButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 0);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(150, 46);
            this.OKButton.TabIndex = 15;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.MyCancelButton.Location = new System.Drawing.Point(168, 6);
            this.MyCancelButton.Margin = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(150, 46);
            this.MyCancelButton.TabIndex = 16;
            this.MyCancelButton.Text = "Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            this.MyCancelButton.Click += new System.EventHandler(this.MyCancelButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.DeleteButton.Location = new System.Drawing.Point(172, 12);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(6, 12, 0, 6);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(160, 46);
            this.DeleteButton.TabIndex = 11;
            this.DeleteButton.Text = "&Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.AddButton.Location = new System.Drawing.Point(0, 12);
            this.AddButton.Margin = new System.Windows.Forms.Padding(0, 12, 6, 6);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(160, 46);
            this.AddButton.TabIndex = 10;
            this.AddButton.Text = "&Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ServerGroupBox
            // 
            this.ServerGroupBox.AutoSize = true;
            this.ServerGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ServerGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.ServerGroupBox.Location = new System.Drawing.Point(356, 0);
            this.ServerGroupBox.Margin = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.ServerGroupBox.Name = "ServerGroupBox";
            this.ServerGroupBox.Padding = new System.Windows.Forms.Padding(6);
            this.ServerGroupBox.Size = new System.Drawing.Size(671, 518);
            this.ServerGroupBox.TabIndex = 0;
            this.ServerGroupBox.TabStop = false;
            this.ServerGroupBox.Text = "Server";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.CTSelect, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.AlterIdLabel, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.RemarksTextBox, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.IPLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ServerPortLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.IDLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.IPTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.TPLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.TPSelect, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.SecurityLabel, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.CTLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.PathTextBox, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.PathLabel, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.RemarksLabel, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.IDTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.CDLabel, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.CDTextBox, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.SecuritySelect, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.ServerPortText, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.AlterIdText, 1, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 36);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(654, 452);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // CTSelect
            // 
            this.CTSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CTSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CTSelect.FormattingEnabled = true;
            this.CTSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CTSelect.ItemHeight = 25;
            this.CTSelect.Items.AddRange(new object[] {
            "none",
            "srtp",
            "utp",
            "wechat-video",
            "dtls",
            "wireguard"});
            this.CTSelect.Location = new System.Drawing.Point(224, 190);
            this.CTSelect.Margin = new System.Windows.Forms.Padding(6);
            this.CTSelect.Name = "CTSelect";
            this.CTSelect.Size = new System.Drawing.Size(418, 33);
            this.CTSelect.TabIndex = 4;
            // 
            // AlterIdLabel
            // 
            this.AlterIdLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.AlterIdLabel.AutoSize = true;
            this.AlterIdLabel.Location = new System.Drawing.Point(139, 238);
            this.AlterIdLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.AlterIdLabel.Name = "AlterIdLabel";
            this.AlterIdLabel.Size = new System.Drawing.Size(73, 25);
            this.AlterIdLabel.TabIndex = 6;
            this.AlterIdLabel.Text = "AlterId";
            // 
            // RemarksTextBox
            // 
            this.RemarksTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RemarksTextBox.Location = new System.Drawing.Point(224, 364);
            this.RemarksTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.RemarksTextBox.MaxLength = 32;
            this.RemarksTextBox.Name = "RemarksTextBox";
            this.RemarksTextBox.Size = new System.Drawing.Size(418, 31);
            this.RemarksTextBox.TabIndex = 8;
            this.RemarksTextBox.WordWrap = false;
            // 
            // IPLabel
            // 
            this.IPLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.IPLabel.AutoSize = true;
            this.IPLabel.Location = new System.Drawing.Point(52, 15);
            this.IPLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(160, 25);
            this.IPLabel.TabIndex = 0;
            this.IPLabel.Text = "Server Address";
            // 
            // ServerPortLabel
            // 
            this.ServerPortLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ServerPortLabel.AutoSize = true;
            this.ServerPortLabel.Location = new System.Drawing.Point(92, 58);
            this.ServerPortLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.ServerPortLabel.Name = "ServerPortLabel";
            this.ServerPortLabel.Size = new System.Drawing.Size(120, 25);
            this.ServerPortLabel.TabIndex = 1;
            this.ServerPortLabel.Text = "Server Port";
            // 
            // IDLabel
            // 
            this.IDLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.IDLabel.AutoSize = true;
            this.IDLabel.Location = new System.Drawing.Point(180, 103);
            this.IDLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(32, 25);
            this.IDLabel.TabIndex = 2;
            this.IDLabel.Text = "ID";
            this.IDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IPTextBox
            // 
            this.IPTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.IPTextBox.Location = new System.Drawing.Point(224, 12);
            this.IPTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.IPTextBox.MaxLength = 512;
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(418, 31);
            this.IPTextBox.TabIndex = 0;
            this.IPTextBox.WordWrap = false;
            // 
            // TPLabel
            // 
            this.TPLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.TPLabel.AutoSize = true;
            this.TPLabel.Location = new System.Drawing.Point(60, 149);
            this.TPLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.TPLabel.Name = "TPLabel";
            this.TPLabel.Size = new System.Drawing.Size(152, 25);
            this.TPLabel.TabIndex = 4;
            this.TPLabel.Text = "Trans Protocol";
            // 
            // TPSelect
            // 
            this.TPSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TPSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TPSelect.FormattingEnabled = true;
            this.TPSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TPSelect.ItemHeight = 25;
            this.TPSelect.Items.AddRange(new object[] {
            "tcp",
            "kcp",
            "ws",
            "h2",
            "quic"});
            this.TPSelect.Location = new System.Drawing.Point(224, 145);
            this.TPSelect.Margin = new System.Windows.Forms.Padding(6);
            this.TPSelect.Name = "TPSelect";
            this.TPSelect.Size = new System.Drawing.Size(418, 33);
            this.TPSelect.TabIndex = 3;
            // 
            // SecurityLabel
            // 
            this.SecurityLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SecurityLabel.AutoSize = true;
            this.SecurityLabel.Location = new System.Drawing.Point(122, 411);
            this.SecurityLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.SecurityLabel.Name = "SecurityLabel";
            this.SecurityLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SecurityLabel.Size = new System.Drawing.Size(90, 25);
            this.SecurityLabel.TabIndex = 9;
            this.SecurityLabel.Text = "Security";
            // 
            // CTLabel
            // 
            this.CTLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.CTLabel.AutoSize = true;
            this.CTLabel.Location = new System.Drawing.Point(31, 194);
            this.CTLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.CTLabel.Name = "CTLabel";
            this.CTLabel.Size = new System.Drawing.Size(181, 25);
            this.CTLabel.TabIndex = 5;
            this.CTLabel.Text = "Camouflage Type";
            // 
            // PathTextBox
            // 
            this.PathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PathTextBox.Location = new System.Drawing.Point(224, 321);
            this.PathTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.PathTextBox.MaxLength = 512;
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(418, 31);
            this.PathTextBox.TabIndex = 7;
            this.PathTextBox.WordWrap = false;
            // 
            // PathLabel
            // 
            this.PathLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PathLabel.AutoSize = true;
            this.PathLabel.Location = new System.Drawing.Point(156, 324);
            this.PathLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.PathLabel.Name = "PathLabel";
            this.PathLabel.Size = new System.Drawing.Size(56, 25);
            this.PathLabel.TabIndex = 7;
            this.PathLabel.Text = "Path";
            // 
            // RemarksLabel
            // 
            this.RemarksLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.RemarksLabel.AutoSize = true;
            this.RemarksLabel.Location = new System.Drawing.Point(115, 367);
            this.RemarksLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.RemarksLabel.Name = "RemarksLabel";
            this.RemarksLabel.Size = new System.Drawing.Size(97, 25);
            this.RemarksLabel.TabIndex = 8;
            this.RemarksLabel.Text = "Remarks";
            // 
            // IDTextBox
            // 
            this.IDTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.IDTextBox.Font = new System.Drawing.Font("Segoe UI Semilight", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDTextBox.Location = new System.Drawing.Point(224, 98);
            this.IDTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.IDTextBox.Mask = "CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC";
            this.IDTextBox.Name = "IDTextBox";
            this.IDTextBox.Size = new System.Drawing.Size(418, 35);
            this.IDTextBox.TabIndex = 2;
            // 
            // CDLabel
            // 
            this.CDLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.CDLabel.AutoSize = true;
            this.CDLabel.Location = new System.Drawing.Point(9, 281);
            this.CDLabel.Name = "CDLabel";
            this.CDLabel.Size = new System.Drawing.Size(206, 25);
            this.CDLabel.TabIndex = 15;
            this.CDLabel.Text = "Camouflage Domain";
            // 
            // CDTextBox
            // 
            this.CDTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CDTextBox.Location = new System.Drawing.Point(224, 278);
            this.CDTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.CDTextBox.Name = "CDTextBox";
            this.CDTextBox.Size = new System.Drawing.Size(418, 31);
            this.CDTextBox.TabIndex = 6;
            // 
            // SecuritySelect
            // 
            this.SecuritySelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SecuritySelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SecuritySelect.FormattingEnabled = true;
            this.SecuritySelect.Items.AddRange(new object[] {
            "",
            "tls"});
            this.SecuritySelect.Location = new System.Drawing.Point(224, 407);
            this.SecuritySelect.Margin = new System.Windows.Forms.Padding(6);
            this.SecuritySelect.Name = "SecuritySelect";
            this.SecuritySelect.Size = new System.Drawing.Size(418, 33);
            this.SecuritySelect.TabIndex = 9;
            // 
            // ServersListBox
            // 
            this.ServersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServersListBox.FormattingEnabled = true;
            this.ServersListBox.IntegralHeight = false;
            this.ServersListBox.ItemHeight = 25;
            this.ServersListBox.Location = new System.Drawing.Point(0, 0);
            this.ServersListBox.Margin = new System.Windows.Forms.Padding(0);
            this.ServersListBox.Name = "ServersListBox";
            this.ServersListBox.Size = new System.Drawing.Size(332, 555);
            this.ServersListBox.TabIndex = 9;
            this.ServersListBox.SelectedIndexChanged += new System.EventHandler(this.ServersListBox_SelectedIndexChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.ServersListBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ServerGroupBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(24, 24);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1027, 747);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.MoveDownButton, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.MoveUpButton, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 683);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(332, 64);
            this.tableLayoutPanel6.TabIndex = 10;
            // 
            // MoveDownButton
            // 
            this.MoveDownButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.MoveDownButton.Location = new System.Drawing.Point(172, 12);
            this.MoveDownButton.Margin = new System.Windows.Forms.Padding(6, 12, 0, 6);
            this.MoveDownButton.Name = "MoveDownButton";
            this.MoveDownButton.Size = new System.Drawing.Size(160, 46);
            this.MoveDownButton.TabIndex = 14;
            this.MoveDownButton.Text = "Move D&own";
            this.MoveDownButton.UseVisualStyleBackColor = true;
            this.MoveDownButton.Click += new System.EventHandler(this.MoveDownButton_Click);
            // 
            // MoveUpButton
            // 
            this.MoveUpButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.MoveUpButton.Location = new System.Drawing.Point(0, 12);
            this.MoveUpButton.Margin = new System.Windows.Forms.Padding(0, 12, 6, 6);
            this.MoveUpButton.Name = "MoveUpButton";
            this.MoveUpButton.Size = new System.Drawing.Size(160, 46);
            this.MoveUpButton.TabIndex = 13;
            this.MoveUpButton.Text = "Move &Up";
            this.MoveUpButton.UseVisualStyleBackColor = true;
            this.MoveUpButton.Click += new System.EventHandler(this.MoveUpButton_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.ProxyPortLabel, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.LocalPortNum, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.CorePortLabel, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.CorePortNum, 1, 1);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(765, 555);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.Padding = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(262, 128);
            this.tableLayoutPanel5.TabIndex = 9;
            // 
            // ProxyPortLabel
            // 
            this.ProxyPortLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ProxyPortLabel.AutoSize = true;
            this.ProxyPortLabel.Location = new System.Drawing.Point(12, 29);
            this.ProxyPortLabel.Margin = new System.Windows.Forms.Padding(6, 20, 6, 20);
            this.ProxyPortLabel.Name = "ProxyPortLabel";
            this.ProxyPortLabel.Size = new System.Drawing.Size(112, 25);
            this.ProxyPortLabel.TabIndex = 3;
            this.ProxyPortLabel.Text = "Proxy Port";
            // 
            // LocalPortNum
            // 
            this.LocalPortNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LocalPortNum.Location = new System.Drawing.Point(133, 26);
            this.LocalPortNum.Margin = new System.Windows.Forms.Padding(3, 20, 3, 20);
            this.LocalPortNum.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.LocalPortNum.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.LocalPortNum.Name = "LocalPortNum";
            this.LocalPortNum.Size = new System.Drawing.Size(120, 31);
            this.LocalPortNum.TabIndex = 10;
            this.LocalPortNum.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // CorePortLabel
            // 
            this.CorePortLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.CorePortLabel.AutoSize = true;
            this.CorePortLabel.Location = new System.Drawing.Point(24, 87);
            this.CorePortLabel.Name = "CorePortLabel";
            this.CorePortLabel.Size = new System.Drawing.Size(103, 25);
            this.CorePortLabel.TabIndex = 10;
            this.CorePortLabel.Text = "Core Port";
            // 
            // CorePortNum
            // 
            this.CorePortNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CorePortNum.Location = new System.Drawing.Point(133, 84);
            this.CorePortNum.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CorePortNum.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.CorePortNum.Name = "CorePortNum";
            this.CorePortNum.Size = new System.Drawing.Size(120, 31);
            this.CorePortNum.TabIndex = 11;
            this.CorePortNum.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.Controls.Add(this.MyCancelButton, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.OKButton, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(709, 689);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(6, 6, 0, 6);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(318, 52);
            this.tableLayoutPanel3.TabIndex = 8;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.DuplicateButton, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.DeleteButton, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.AddButton, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.ClipboardButton, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 555);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(332, 128);
            this.tableLayoutPanel4.TabIndex = 8;
            // 
            // DuplicateButton
            // 
            this.DuplicateButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.DuplicateButton.Location = new System.Drawing.Point(0, 76);
            this.DuplicateButton.Margin = new System.Windows.Forms.Padding(0, 12, 6, 6);
            this.DuplicateButton.Name = "DuplicateButton";
            this.DuplicateButton.Size = new System.Drawing.Size(160, 46);
            this.DuplicateButton.TabIndex = 12;
            this.DuplicateButton.Text = "Dupli&cate";
            this.DuplicateButton.UseVisualStyleBackColor = true;
            this.DuplicateButton.Click += new System.EventHandler(this.DuplicateButton_Click);
            // 
            // ClipboardButton
            // 
            this.ClipboardButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.ClipboardButton.Location = new System.Drawing.Point(172, 76);
            this.ClipboardButton.Margin = new System.Windows.Forms.Padding(6, 12, 0, 6);
            this.ClipboardButton.Name = "ClipboardButton";
            this.ClipboardButton.Size = new System.Drawing.Size(160, 46);
            this.ClipboardButton.TabIndex = 13;
            this.ClipboardButton.Text = "Clipboard";
            this.ClipboardButton.UseVisualStyleBackColor = true;
            this.ClipboardButton.Click += new System.EventHandler(this.ClipboardButton_Click);
            // 
            // ServerPortText
            // 
            this.ServerPortText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerPortText.Location = new System.Drawing.Point(224, 55);
            this.ServerPortText.Margin = new System.Windows.Forms.Padding(6);
            this.ServerPortText.Name = "ServerPortText";
            this.ServerPortText.Size = new System.Drawing.Size(418, 31);
            this.ServerPortText.TabIndex = 1;
            this.ServerPortText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyAllowDigit_KeyPress);
            // 
            // AlterIdText
            // 
            this.AlterIdText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.AlterIdText.Location = new System.Drawing.Point(224, 235);
            this.AlterIdText.Margin = new System.Windows.Forms.Padding(6);
            this.AlterIdText.Name = "AlterIdText";
            this.AlterIdText.Size = new System.Drawing.Size(418, 31);
            this.AlterIdText.TabIndex = 5;
            this.AlterIdText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyAllowDigit_KeyPress);
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(1128, 854);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.Padding = new System.Windows.Forms.Padding(24, 24, 24, 18);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Servers";
            this.ServerGroupBox.ResumeLayout(false);
            this.ServerGroupBox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocalPortNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CorePortNum)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button MyCancelButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.GroupBox ServerGroupBox;
        private System.Windows.Forms.ListBox ServersListBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label ProxyPortLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button MoveDownButton;
        private System.Windows.Forms.Button MoveUpButton;
        private System.Windows.Forms.Button DuplicateButton;
        private System.Windows.Forms.Button ClipboardButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label AlterIdLabel;
        private System.Windows.Forms.TextBox RemarksTextBox;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Label ServerPortLabel;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.Label TPLabel;
        private System.Windows.Forms.ComboBox TPSelect;
        private System.Windows.Forms.Label SecurityLabel;
        private System.Windows.Forms.Label CTLabel;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.Label PathLabel;
        private System.Windows.Forms.Label RemarksLabel;
        private System.Windows.Forms.ComboBox CTSelect;
        private System.Windows.Forms.NumericUpDown LocalPortNum;
        private System.Windows.Forms.MaskedTextBox IDTextBox;
        private System.Windows.Forms.Label CDLabel;
        private System.Windows.Forms.TextBox CDTextBox;
        private System.Windows.Forms.ComboBox SecuritySelect;
        private System.Windows.Forms.Label CorePortLabel;
        private System.Windows.Forms.NumericUpDown CorePortNum;
        private System.Windows.Forms.TextBox ServerPortText;
        private System.Windows.Forms.TextBox AlterIdText;
    }
}