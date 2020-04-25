namespace V2RayShell.View
{
partial class HotkeySettingsForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRegisterAll = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SwitchSystemProxyLabel = new System.Windows.Forms.Label();
            this.SwitchSystemProxyModeLabel = new System.Windows.Forms.Label();
            this.SwitchAllowLanLabel = new System.Windows.Forms.Label();
            this.ShowLogsLabel = new System.Windows.Forms.Label();
            this.ServerMoveUpLabel = new System.Windows.Forms.Label();
            this.ServerMoveDownLabel = new System.Windows.Forms.Label();
            this.AddCurrentChromeDomaintoPACLabel = new System.Windows.Forms.Label();
            this.AddCurrentChromeURLtoPACLabel = new System.Windows.Forms.Label();
            this.ScanQRLabel = new System.Windows.Forms.Label();
            this.SwitchSystemProxyTextBox = new System.Windows.Forms.TextBox();
            this.SwitchSystemProxyModeTextBox = new System.Windows.Forms.TextBox();
            this.SwitchAllowLanTextBox = new System.Windows.Forms.TextBox();
            this.ShowLogsTextBox = new System.Windows.Forms.TextBox();
            this.ServerMoveUpTextBox = new System.Windows.Forms.TextBox();
            this.ServerMoveDownTextBox = new System.Windows.Forms.TextBox();
            this.AddCurrentChromeDomaintoPACTextBox = new System.Windows.Forms.TextBox();
            this.AddCurrentChromeURLtoPACTextBox = new System.Windows.Forms.TextBox();
            this.ScanQRTextBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.btnOK);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnRegisterAll);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 675);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 32, 0);
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(950, 82);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(666, 14);
            this.btnOK.Margin = new System.Windows.Forms.Padding(6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(246, 62);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(408, 14);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(246, 62);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // btnRegisterAll
            // 
            this.btnRegisterAll.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRegisterAll.Location = new System.Drawing.Point(150, 14);
            this.btnRegisterAll.Margin = new System.Windows.Forms.Padding(6);
            this.btnRegisterAll.Name = "btnRegisterAll";
            this.btnRegisterAll.Size = new System.Drawing.Size(246, 62);
            this.btnRegisterAll.TabIndex = 2;
            this.btnRegisterAll.Text = "Reg All";
            this.btnRegisterAll.UseVisualStyleBackColor = true;
            this.btnRegisterAll.Click += new System.EventHandler(this.RegisterAllButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.SwitchSystemProxyLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.SwitchSystemProxyModeLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.SwitchAllowLanLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ShowLogsLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.ServerMoveUpLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.ServerMoveDownLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.AddCurrentChromeDomaintoPACLabel, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.AddCurrentChromeURLtoPACLabel, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.ScanQRLabel, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.SwitchSystemProxyTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.SwitchSystemProxyModeTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.SwitchAllowLanTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ShowLogsTextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.ServerMoveUpTextBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.ServerMoveDownTextBox, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.AddCurrentChromeDomaintoPACTextBox, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.AddCurrentChromeURLtoPACTextBox, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.ScanQRTextBox, 1, 8);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.50485F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.50485F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.50485F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.50485F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.50485F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.50485F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.50485F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.50485F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.50485F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1236, 764);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // SwitchSystemProxyLabel
            // 
            this.SwitchSystemProxyLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SwitchSystemProxyLabel.AutoSize = true;
            this.SwitchSystemProxyLabel.Location = new System.Drawing.Point(147, 23);
            this.SwitchSystemProxyLabel.Margin = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.SwitchSystemProxyLabel.Name = "SwitchSystemProxyLabel";
            this.SwitchSystemProxyLabel.Size = new System.Drawing.Size(221, 28);
            this.SwitchSystemProxyLabel.TabIndex = 0;
            this.SwitchSystemProxyLabel.Text = "Enable System Proxy";
            this.SwitchSystemProxyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SwitchSystemProxyModeLabel
            // 
            this.SwitchSystemProxyModeLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SwitchSystemProxyModeLabel.AutoSize = true;
            this.SwitchSystemProxyModeLabel.Location = new System.Drawing.Point(83, 98);
            this.SwitchSystemProxyModeLabel.Margin = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.SwitchSystemProxyModeLabel.Name = "SwitchSystemProxyModeLabel";
            this.SwitchSystemProxyModeLabel.Size = new System.Drawing.Size(285, 28);
            this.SwitchSystemProxyModeLabel.TabIndex = 1;
            this.SwitchSystemProxyModeLabel.Text = "Switch System Proxy Mode";
            this.SwitchSystemProxyModeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SwitchAllowLanLabel
            // 
            this.SwitchAllowLanLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SwitchAllowLanLabel.AutoSize = true;
            this.SwitchAllowLanLabel.Location = new System.Drawing.Point(121, 173);
            this.SwitchAllowLanLabel.Margin = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.SwitchAllowLanLabel.Name = "SwitchAllowLanLabel";
            this.SwitchAllowLanLabel.Size = new System.Drawing.Size(247, 28);
            this.SwitchAllowLanLabel.TabIndex = 3;
            this.SwitchAllowLanLabel.Text = "Allow Clients from LAN";
            this.SwitchAllowLanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ShowLogsLabel
            // 
            this.ShowLogsLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ShowLogsLabel.AutoSize = true;
            this.ShowLogsLabel.Location = new System.Drawing.Point(233, 248);
            this.ShowLogsLabel.Margin = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.ShowLogsLabel.Name = "ShowLogsLabel";
            this.ShowLogsLabel.Size = new System.Drawing.Size(135, 28);
            this.ShowLogsLabel.TabIndex = 4;
            this.ShowLogsLabel.Text = "Show Logs...";
            this.ShowLogsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ServerMoveUpLabel
            // 
            this.ServerMoveUpLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ServerMoveUpLabel.AutoSize = true;
            this.ServerMoveUpLabel.Location = new System.Drawing.Point(267, 323);
            this.ServerMoveUpLabel.Margin = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.ServerMoveUpLabel.Name = "ServerMoveUpLabel";
            this.ServerMoveUpLabel.Size = new System.Drawing.Size(101, 28);
            this.ServerMoveUpLabel.TabIndex = 4;
            this.ServerMoveUpLabel.Text = "Move up";
            this.ServerMoveUpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ServerMoveDownLabel
            // 
            this.ServerMoveDownLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ServerMoveDownLabel.AutoSize = true;
            this.ServerMoveDownLabel.Location = new System.Drawing.Point(234, 398);
            this.ServerMoveDownLabel.Margin = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.ServerMoveDownLabel.Name = "ServerMoveDownLabel";
            this.ServerMoveDownLabel.Size = new System.Drawing.Size(134, 28);
            this.ServerMoveDownLabel.TabIndex = 4;
            this.ServerMoveDownLabel.Text = "Move Down";
            this.ServerMoveDownLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddCurrentChromeDomaintoPACLabel
            // 
            this.AddCurrentChromeDomaintoPACLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.AddCurrentChromeDomaintoPACLabel.AutoSize = true;
            this.AddCurrentChromeDomaintoPACLabel.Location = new System.Drawing.Point(16, 473);
            this.AddCurrentChromeDomaintoPACLabel.Margin = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.AddCurrentChromeDomaintoPACLabel.Name = "AddCurrentChromeDomaintoPACLabel";
            this.AddCurrentChromeDomaintoPACLabel.Size = new System.Drawing.Size(352, 28);
            this.AddCurrentChromeDomaintoPACLabel.TabIndex = 4;
            this.AddCurrentChromeDomaintoPACLabel.Text = "AddCurrentChromeDomaintoPAC";
            this.AddCurrentChromeDomaintoPACLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddCurrentChromeURLtoPACLabel
            // 
            this.AddCurrentChromeURLtoPACLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.AddCurrentChromeURLtoPACLabel.AutoSize = true;
            this.AddCurrentChromeURLtoPACLabel.Location = new System.Drawing.Point(55, 548);
            this.AddCurrentChromeURLtoPACLabel.Margin = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.AddCurrentChromeURLtoPACLabel.Name = "AddCurrentChromeURLtoPACLabel";
            this.AddCurrentChromeURLtoPACLabel.Size = new System.Drawing.Size(313, 28);
            this.AddCurrentChromeURLtoPACLabel.TabIndex = 4;
            this.AddCurrentChromeURLtoPACLabel.Text = "AddCurrentChromeURLtoPAC";
            this.AddCurrentChromeURLtoPACLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScanQRLabel
            // 
            this.ScanQRLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ScanQRLabel.AutoSize = true;
            this.ScanQRLabel.Location = new System.Drawing.Point(277, 623);
            this.ScanQRLabel.Margin = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.ScanQRLabel.Name = "ScanQRLabel";
            this.ScanQRLabel.Size = new System.Drawing.Size(91, 28);
            this.ScanQRLabel.TabIndex = 4;
            this.ScanQRLabel.Text = "ScanQR";
            this.ScanQRLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SwitchSystemProxyTextBox
            // 
            this.SwitchSystemProxyTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SwitchSystemProxyTextBox.Location = new System.Drawing.Point(390, 20);
            this.SwitchSystemProxyTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 32, 6);
            this.SwitchSystemProxyTextBox.Name = "SwitchSystemProxyTextBox";
            this.SwitchSystemProxyTextBox.ReadOnly = true;
            this.SwitchSystemProxyTextBox.Size = new System.Drawing.Size(814, 35);
            this.SwitchSystemProxyTextBox.TabIndex = 7;
            this.SwitchSystemProxyTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.SwitchSystemProxyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotkeyDown);
            this.SwitchSystemProxyTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HotkeyUp);
            // 
            // SwitchSystemProxyModeTextBox
            // 
            this.SwitchSystemProxyModeTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SwitchSystemProxyModeTextBox.Location = new System.Drawing.Point(390, 95);
            this.SwitchSystemProxyModeTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 32, 6);
            this.SwitchSystemProxyModeTextBox.Name = "SwitchSystemProxyModeTextBox";
            this.SwitchSystemProxyModeTextBox.ReadOnly = true;
            this.SwitchSystemProxyModeTextBox.Size = new System.Drawing.Size(814, 35);
            this.SwitchSystemProxyModeTextBox.TabIndex = 8;
            this.SwitchSystemProxyModeTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.SwitchSystemProxyModeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotkeyDown);
            this.SwitchSystemProxyModeTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HotkeyUp);
            // 
            // SwitchAllowLanTextBox
            // 
            this.SwitchAllowLanTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SwitchAllowLanTextBox.Location = new System.Drawing.Point(390, 170);
            this.SwitchAllowLanTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 32, 6);
            this.SwitchAllowLanTextBox.Name = "SwitchAllowLanTextBox";
            this.SwitchAllowLanTextBox.ReadOnly = true;
            this.SwitchAllowLanTextBox.Size = new System.Drawing.Size(814, 35);
            this.SwitchAllowLanTextBox.TabIndex = 10;
            this.SwitchAllowLanTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.SwitchAllowLanTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotkeyDown);
            this.SwitchAllowLanTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HotkeyUp);
            // 
            // ShowLogsTextBox
            // 
            this.ShowLogsTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ShowLogsTextBox.Location = new System.Drawing.Point(390, 245);
            this.ShowLogsTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 32, 6);
            this.ShowLogsTextBox.Name = "ShowLogsTextBox";
            this.ShowLogsTextBox.ReadOnly = true;
            this.ShowLogsTextBox.Size = new System.Drawing.Size(814, 35);
            this.ShowLogsTextBox.TabIndex = 11;
            this.ShowLogsTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.ShowLogsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotkeyDown);
            this.ShowLogsTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HotkeyUp);
            // 
            // ServerMoveUpTextBox
            // 
            this.ServerMoveUpTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ServerMoveUpTextBox.Location = new System.Drawing.Point(390, 320);
            this.ServerMoveUpTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 32, 6);
            this.ServerMoveUpTextBox.Name = "ServerMoveUpTextBox";
            this.ServerMoveUpTextBox.ReadOnly = true;
            this.ServerMoveUpTextBox.Size = new System.Drawing.Size(814, 35);
            this.ServerMoveUpTextBox.TabIndex = 12;
            this.ServerMoveUpTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.ServerMoveUpTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotkeyDown);
            this.ServerMoveUpTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HotkeyUp);
            // 
            // ServerMoveDownTextBox
            // 
            this.ServerMoveDownTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ServerMoveDownTextBox.Location = new System.Drawing.Point(390, 395);
            this.ServerMoveDownTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 32, 6);
            this.ServerMoveDownTextBox.Name = "ServerMoveDownTextBox";
            this.ServerMoveDownTextBox.ReadOnly = true;
            this.ServerMoveDownTextBox.Size = new System.Drawing.Size(814, 35);
            this.ServerMoveDownTextBox.TabIndex = 13;
            this.ServerMoveDownTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.ServerMoveDownTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotkeyDown);
            this.ServerMoveDownTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HotkeyUp);
            // 
            // AddCurrentChromeDomaintoPACTextBox
            // 
            this.AddCurrentChromeDomaintoPACTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AddCurrentChromeDomaintoPACTextBox.Location = new System.Drawing.Point(390, 470);
            this.AddCurrentChromeDomaintoPACTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 32, 6);
            this.AddCurrentChromeDomaintoPACTextBox.Name = "AddCurrentChromeDomaintoPACTextBox";
            this.AddCurrentChromeDomaintoPACTextBox.ReadOnly = true;
            this.AddCurrentChromeDomaintoPACTextBox.Size = new System.Drawing.Size(814, 35);
            this.AddCurrentChromeDomaintoPACTextBox.TabIndex = 15;
            this.AddCurrentChromeDomaintoPACTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.AddCurrentChromeDomaintoPACTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotkeyDown);
            this.AddCurrentChromeDomaintoPACTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HotkeyUp);
            // 
            // AddCurrentChromeURLtoPACTextBox
            // 
            this.AddCurrentChromeURLtoPACTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AddCurrentChromeURLtoPACTextBox.Location = new System.Drawing.Point(390, 545);
            this.AddCurrentChromeURLtoPACTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 32, 6);
            this.AddCurrentChromeURLtoPACTextBox.Name = "AddCurrentChromeURLtoPACTextBox";
            this.AddCurrentChromeURLtoPACTextBox.ReadOnly = true;
            this.AddCurrentChromeURLtoPACTextBox.Size = new System.Drawing.Size(814, 35);
            this.AddCurrentChromeURLtoPACTextBox.TabIndex = 14;
            this.AddCurrentChromeURLtoPACTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.AddCurrentChromeURLtoPACTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotkeyDown);
            this.AddCurrentChromeURLtoPACTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HotkeyUp);
            // 
            // ScanQRTextBox
            // 
            this.ScanQRTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ScanQRTextBox.Location = new System.Drawing.Point(390, 620);
            this.ScanQRTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 32, 6);
            this.ScanQRTextBox.Name = "ScanQRTextBox";
            this.ScanQRTextBox.ReadOnly = true;
            this.ScanQRTextBox.Size = new System.Drawing.Size(814, 35);
            this.ScanQRTextBox.TabIndex = 16;
            this.ScanQRTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.ScanQRTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotkeyDown);
            this.ScanQRTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HotkeyUp);
            // 
            // HotkeySettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1236, 764);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HotkeySettingsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Hotkeys...";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label SwitchSystemProxyLabel;
        //private System.Windows.Forms.Label SwitchProxyModeLabel;
        private System.Windows.Forms.Label SwitchSystemProxyModeLabel;
        private System.Windows.Forms.Label SwitchAllowLanLabel;
        private System.Windows.Forms.Label ShowLogsLabel;
        private System.Windows.Forms.Label ServerMoveUpLabel;
        private System.Windows.Forms.Label ServerMoveDownLabel;
        private System.Windows.Forms.Label AddCurrentChromeURLtoPACLabel;
        private System.Windows.Forms.Label AddCurrentChromeDomaintoPACLabel;
        private System.Windows.Forms.Label ScanQRLabel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox ShowLogsTextBox;
        private System.Windows.Forms.TextBox SwitchAllowLanTextBox;
        //private System.Windows.Forms.TextBox SwitchProxyModeTextBox;
        private System.Windows.Forms.TextBox SwitchSystemProxyModeTextBox;
        private System.Windows.Forms.TextBox SwitchSystemProxyTextBox;
        private System.Windows.Forms.TextBox ServerMoveUpTextBox;
        private System.Windows.Forms.TextBox ServerMoveDownTextBox;
        private System.Windows.Forms.TextBox AddCurrentChromeURLtoPACTextBox;
        private System.Windows.Forms.TextBox AddCurrentChromeDomaintoPACTextBox;
        private System.Windows.Forms.TextBox ScanQRTextBox;
        private System.Windows.Forms.Button btnRegisterAll;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}