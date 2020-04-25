namespace V2RayShell.View
{
    partial class SubscribeConfigForm
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
            this.SubscribeGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.NameLabel = new System.Windows.Forms.Label();
            this.UrlLabel = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.UrlTextBox = new System.Windows.Forms.TextBox();
            this.UseProxyCheckBox = new System.Windows.Forms.CheckBox();
            this.SubscribeListBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.SubscribeGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Location = new System.Drawing.Point(414, 374);
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
            // SubscribeGroupBox
            // 
            this.SubscribeGroupBox.AutoSize = true;
            this.SubscribeGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SubscribeGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.SubscribeGroupBox.Location = new System.Drawing.Point(356, 0);
            this.SubscribeGroupBox.Margin = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.SubscribeGroupBox.Name = "SubscribeGroupBox";
            this.SubscribeGroupBox.Padding = new System.Windows.Forms.Padding(6);
            this.SubscribeGroupBox.Size = new System.Drawing.Size(533, 205);
            this.SubscribeGroupBox.TabIndex = 0;
            this.SubscribeGroupBox.TabStop = false;
            this.SubscribeGroupBox.Text = "Subscribe";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.NameLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.UrlLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.NameTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.UrlTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.UseProxyCheckBox, 1, 3);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(516, 139);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // NameLabel
            // 
            this.NameLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(12, 15);
            this.NameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(68, 25);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Name";
            // 
            // UrlLabel
            // 
            this.UrlLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.UrlLabel.AutoSize = true;
            this.UrlLabel.Location = new System.Drawing.Point(41, 58);
            this.UrlLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.UrlLabel.Name = "UrlLabel";
            this.UrlLabel.Size = new System.Drawing.Size(39, 25);
            this.UrlLabel.TabIndex = 1;
            this.UrlLabel.Text = "Url";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTextBox.Location = new System.Drawing.Point(92, 12);
            this.NameTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.NameTextBox.MaxLength = 20;
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(412, 31);
            this.NameTextBox.TabIndex = 0;
            this.NameTextBox.WordWrap = false;
            // 
            // UrlTextBox
            // 
            this.UrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.UrlTextBox.Location = new System.Drawing.Point(92, 55);
            this.UrlTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.UrlTextBox.MaxLength = 2000;
            this.UrlTextBox.Name = "UrlTextBox";
            this.UrlTextBox.Size = new System.Drawing.Size(412, 31);
            this.UrlTextBox.TabIndex = 1;
            this.UrlTextBox.WordWrap = false;
            // 
            // UseProxyCheckBox
            // 
            this.UseProxyCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.UseProxyCheckBox.AutoSize = true;
            this.UseProxyCheckBox.Location = new System.Drawing.Point(92, 98);
            this.UseProxyCheckBox.Margin = new System.Windows.Forms.Padding(6);
            this.UseProxyCheckBox.Name = "UseProxyCheckBox";
            this.UseProxyCheckBox.Size = new System.Drawing.Size(143, 29);
            this.UseProxyCheckBox.TabIndex = 3;
            this.UseProxyCheckBox.Text = "Use Proxy";
            this.UseProxyCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UseProxyCheckBox.UseVisualStyleBackColor = true;
            // 
            // SubscribeListBox
            // 
            this.SubscribeListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubscribeListBox.FormattingEnabled = true;
            this.SubscribeListBox.IntegralHeight = false;
            this.SubscribeListBox.ItemHeight = 25;
            this.SubscribeListBox.Location = new System.Drawing.Point(0, 0);
            this.SubscribeListBox.Margin = new System.Windows.Forms.Padding(0);
            this.SubscribeListBox.Name = "SubscribeListBox";
            this.SubscribeListBox.Size = new System.Drawing.Size(332, 292);
            this.SubscribeListBox.TabIndex = 9;
            this.SubscribeListBox.SelectedIndexChanged += new System.EventHandler(this.SubscribeListBox_SelectedIndexChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.SubscribeListBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.SubscribeGroupBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(24, 24);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(889, 420);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 356);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(332, 0);
            this.tableLayoutPanel6.TabIndex = 10;
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
            this.tableLayoutPanel3.Location = new System.Drawing.Point(571, 362);
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
            this.tableLayoutPanel4.Controls.Add(this.DeleteButton, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.AddButton, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 292);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(332, 64);
            this.tableLayoutPanel4.TabIndex = 8;
            // 
            // SubscribeConfigForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(1148, 796);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubscribeConfigForm";
            this.Padding = new System.Windows.Forms.Padding(24, 24, 24, 18);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Subscribe";
            this.SubscribeGroupBox.ResumeLayout(false);
            this.SubscribeGroupBox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
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
        private System.Windows.Forms.GroupBox SubscribeGroupBox;
        private System.Windows.Forms.ListBox SubscribeListBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label UrlLabel;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TextBox UrlTextBox;
        private System.Windows.Forms.CheckBox UseProxyCheckBox;
    }
}