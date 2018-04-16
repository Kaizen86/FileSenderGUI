namespace FileSenderGUI
{
    partial class Window
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.Tabs = new System.Windows.Forms.TabControl();
            this.sendTab = new System.Windows.Forms.TabPage();
            this.send_progressBar = new System.Windows.Forms.ProgressBar();
            this.send_Status = new System.Windows.Forms.TextBox();
            this.send_sendButton = new System.Windows.Forms.Button();
            this.send_fileSelectButton = new System.Windows.Forms.Button();
            this.receiveTab = new System.Windows.Forms.TabPage();
            this.receive_Status = new System.Windows.Forms.TextBox();
            this.receive_startServerButton = new System.Windows.Forms.Button();
            this.receive_selectFolderButton = new System.Windows.Forms.Button();
            this.receive_progressBar = new System.Windows.Forms.ProgressBar();
            this.settingsTab = new System.Windows.Forms.TabPage();
            this.settings_listenPortInput_label = new System.Windows.Forms.Label();
            this.settings_serverPortInput_label = new System.Windows.Forms.Label();
            this.settings_serverAddressInput_label = new System.Windows.Forms.Label();
            this.settings_listenPortInput = new System.Windows.Forms.NumericUpDown();
            this.settings_serverPortInput = new System.Windows.Forms.NumericUpDown();
            this.settings_serverAddressInput = new System.Windows.Forms.TextBox();
            this.settings_saveSettingsButton = new System.Windows.Forms.Button();
            this.send_fileSelect = new System.Windows.Forms.OpenFileDialog();
            this.receive_selectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.Tabs.SuspendLayout();
            this.sendTab.SuspendLayout();
            this.receiveTab.SuspendLayout();
            this.settingsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settings_listenPortInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.settings_serverPortInput)).BeginInit();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.sendTab);
            this.Tabs.Controls.Add(this.receiveTab);
            this.Tabs.Controls.Add(this.settingsTab);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(388, 354);
            this.Tabs.TabIndex = 0;
            // 
            // sendTab
            // 
            this.sendTab.BackColor = System.Drawing.SystemColors.Control;
            this.sendTab.Controls.Add(this.send_progressBar);
            this.sendTab.Controls.Add(this.send_Status);
            this.sendTab.Controls.Add(this.send_sendButton);
            this.sendTab.Controls.Add(this.send_fileSelectButton);
            this.sendTab.Location = new System.Drawing.Point(4, 29);
            this.sendTab.Name = "sendTab";
            this.sendTab.Padding = new System.Windows.Forms.Padding(3);
            this.sendTab.Size = new System.Drawing.Size(380, 321);
            this.sendTab.TabIndex = 0;
            this.sendTab.Text = "Send";
            // 
            // send_progressBar
            // 
            this.send_progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.send_progressBar.Location = new System.Drawing.Point(8, 277);
            this.send_progressBar.Name = "send_progressBar";
            this.send_progressBar.Size = new System.Drawing.Size(364, 36);
            this.send_progressBar.TabIndex = 1;
            // 
            // send_Status
            // 
            this.send_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.send_Status.Location = new System.Drawing.Point(8, 87);
            this.send_Status.Multiline = true;
            this.send_Status.Name = "send_Status";
            this.send_Status.ReadOnly = true;
            this.send_Status.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.send_Status.Size = new System.Drawing.Size(364, 184);
            this.send_Status.TabIndex = 2;
            this.send_Status.Text = "Waiting for file...";
            // 
            // send_sendButton
            // 
            this.send_sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.send_sendButton.Location = new System.Drawing.Point(258, 6);
            this.send_sendButton.Name = "send_sendButton";
            this.send_sendButton.Size = new System.Drawing.Size(110, 75);
            this.send_sendButton.TabIndex = 1;
            this.send_sendButton.Text = "Send";
            this.send_sendButton.UseVisualStyleBackColor = true;
            this.send_sendButton.Click += new System.EventHandler(this.send_sendButton_Click);
            // 
            // send_fileSelectButton
            // 
            this.send_fileSelectButton.Location = new System.Drawing.Point(8, 6);
            this.send_fileSelectButton.Name = "send_fileSelectButton";
            this.send_fileSelectButton.Size = new System.Drawing.Size(110, 75);
            this.send_fileSelectButton.TabIndex = 0;
            this.send_fileSelectButton.Text = "Select File";
            this.send_fileSelectButton.UseVisualStyleBackColor = true;
            this.send_fileSelectButton.Click += new System.EventHandler(this.send_fileSelectButton_Click);
            // 
            // receiveTab
            // 
            this.receiveTab.BackColor = System.Drawing.SystemColors.Control;
            this.receiveTab.Controls.Add(this.receive_Status);
            this.receiveTab.Controls.Add(this.receive_startServerButton);
            this.receiveTab.Controls.Add(this.receive_selectFolderButton);
            this.receiveTab.Controls.Add(this.receive_progressBar);
            this.receiveTab.Location = new System.Drawing.Point(4, 29);
            this.receiveTab.Name = "receiveTab";
            this.receiveTab.Padding = new System.Windows.Forms.Padding(3);
            this.receiveTab.Size = new System.Drawing.Size(380, 321);
            this.receiveTab.TabIndex = 1;
            this.receiveTab.Text = "Receive";
            // 
            // receive_Status
            // 
            this.receive_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.receive_Status.Location = new System.Drawing.Point(8, 89);
            this.receive_Status.Multiline = true;
            this.receive_Status.Name = "receive_Status";
            this.receive_Status.ReadOnly = true;
            this.receive_Status.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.receive_Status.Size = new System.Drawing.Size(364, 184);
            this.receive_Status.TabIndex = 3;
            // 
            // receive_startServerButton
            // 
            this.receive_startServerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.receive_startServerButton.Enabled = false;
            this.receive_startServerButton.Location = new System.Drawing.Point(262, 8);
            this.receive_startServerButton.Name = "receive_startServerButton";
            this.receive_startServerButton.Size = new System.Drawing.Size(110, 75);
            this.receive_startServerButton.TabIndex = 2;
            this.receive_startServerButton.Text = "Start server";
            this.receive_startServerButton.UseVisualStyleBackColor = true;
            this.receive_startServerButton.Click += new System.EventHandler(this.receive_startServerButton_Click);
            // 
            // receive_selectFolderButton
            // 
            this.receive_selectFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receive_selectFolderButton.Location = new System.Drawing.Point(8, 8);
            this.receive_selectFolderButton.Name = "receive_selectFolderButton";
            this.receive_selectFolderButton.Size = new System.Drawing.Size(110, 75);
            this.receive_selectFolderButton.TabIndex = 1;
            this.receive_selectFolderButton.Text = "Select destination folder";
            this.receive_selectFolderButton.UseVisualStyleBackColor = true;
            this.receive_selectFolderButton.Click += new System.EventHandler(this.receive_selectFolderButton_Click);
            // 
            // receive_progressBar
            // 
            this.receive_progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.receive_progressBar.Location = new System.Drawing.Point(8, 279);
            this.receive_progressBar.Name = "receive_progressBar";
            this.receive_progressBar.Size = new System.Drawing.Size(364, 36);
            this.receive_progressBar.TabIndex = 0;
            // 
            // settingsTab
            // 
            this.settingsTab.BackColor = System.Drawing.SystemColors.Control;
            this.settingsTab.Controls.Add(this.settings_listenPortInput_label);
            this.settingsTab.Controls.Add(this.settings_serverPortInput_label);
            this.settingsTab.Controls.Add(this.settings_serverAddressInput_label);
            this.settingsTab.Controls.Add(this.settings_listenPortInput);
            this.settingsTab.Controls.Add(this.settings_serverPortInput);
            this.settingsTab.Controls.Add(this.settings_serverAddressInput);
            this.settingsTab.Controls.Add(this.settings_saveSettingsButton);
            this.settingsTab.Location = new System.Drawing.Point(4, 29);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTab.Size = new System.Drawing.Size(380, 321);
            this.settingsTab.TabIndex = 2;
            this.settingsTab.Text = "Settings";
            // 
            // settings_listenPortInput_label
            // 
            this.settings_listenPortInput_label.AutoSize = true;
            this.settings_listenPortInput_label.Location = new System.Drawing.Point(178, 108);
            this.settings_listenPortInput_label.Name = "settings_listenPortInput_label";
            this.settings_listenPortInput_label.Size = new System.Drawing.Size(99, 20);
            this.settings_listenPortInput_label.TabIndex = 6;
            this.settings_listenPortInput_label.Text = "Receive Port";
            // 
            // settings_serverPortInput_label
            // 
            this.settings_serverPortInput_label.AutoSize = true;
            this.settings_serverPortInput_label.Location = new System.Drawing.Point(178, 46);
            this.settings_serverPortInput_label.Name = "settings_serverPortInput_label";
            this.settings_serverPortInput_label.Size = new System.Drawing.Size(88, 20);
            this.settings_serverPortInput_label.TabIndex = 5;
            this.settings_serverPortInput_label.Text = "Server Port";
            // 
            // settings_serverAddressInput_label
            // 
            this.settings_serverAddressInput_label.AutoSize = true;
            this.settings_serverAddressInput_label.Location = new System.Drawing.Point(178, 15);
            this.settings_serverAddressInput_label.Name = "settings_serverAddressInput_label";
            this.settings_serverAddressInput_label.Size = new System.Drawing.Size(118, 20);
            this.settings_serverAddressInput_label.TabIndex = 4;
            this.settings_serverAddressInput_label.Text = "Server Address";
            // 
            // settings_listenPortInput
            // 
            this.settings_listenPortInput.Location = new System.Drawing.Point(8, 106);
            this.settings_listenPortInput.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.settings_listenPortInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.settings_listenPortInput.Name = "settings_listenPortInput";
            this.settings_listenPortInput.Size = new System.Drawing.Size(164, 26);
            this.settings_listenPortInput.TabIndex = 3;
            this.settings_listenPortInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // settings_serverPortInput
            // 
            this.settings_serverPortInput.Location = new System.Drawing.Point(8, 44);
            this.settings_serverPortInput.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.settings_serverPortInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.settings_serverPortInput.Name = "settings_serverPortInput";
            this.settings_serverPortInput.Size = new System.Drawing.Size(164, 26);
            this.settings_serverPortInput.TabIndex = 2;
            this.settings_serverPortInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // settings_serverAddressInput
            // 
            this.settings_serverAddressInput.Location = new System.Drawing.Point(8, 12);
            this.settings_serverAddressInput.Name = "settings_serverAddressInput";
            this.settings_serverAddressInput.Size = new System.Drawing.Size(164, 26);
            this.settings_serverAddressInput.TabIndex = 1;
            // 
            // settings_saveSettingsButton
            // 
            this.settings_saveSettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settings_saveSettingsButton.Location = new System.Drawing.Point(302, 6);
            this.settings_saveSettingsButton.Name = "settings_saveSettingsButton";
            this.settings_saveSettingsButton.Size = new System.Drawing.Size(75, 32);
            this.settings_saveSettingsButton.TabIndex = 0;
            this.settings_saveSettingsButton.Text = "Save";
            this.settings_saveSettingsButton.UseVisualStyleBackColor = true;
            this.settings_saveSettingsButton.Click += new System.EventHandler(this.settings_saveSettingsButton_Click);
            // 
            // send_fileSelect
            // 
            this.send_fileSelect.ReadOnlyChecked = true;
            // 
            // receive_selectFolder
            // 
            this.receive_selectFolder.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 354);
            this.Controls.Add(this.Tabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(410, 310);
            this.Name = "Window";
            this.Text = "File Sender";
            this.Tabs.ResumeLayout(false);
            this.sendTab.ResumeLayout(false);
            this.sendTab.PerformLayout();
            this.receiveTab.ResumeLayout(false);
            this.receiveTab.PerformLayout();
            this.settingsTab.ResumeLayout(false);
            this.settingsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settings_listenPortInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.settings_serverPortInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage sendTab;
        private System.Windows.Forms.TabPage receiveTab;
        private System.Windows.Forms.TabPage settingsTab;
        private System.Windows.Forms.OpenFileDialog send_fileSelect;
        private System.Windows.Forms.Button send_sendButton;
        private System.Windows.Forms.Button send_fileSelectButton;
        private System.Windows.Forms.NumericUpDown settings_listenPortInput;
        private System.Windows.Forms.NumericUpDown settings_serverPortInput;
        private System.Windows.Forms.TextBox settings_serverAddressInput;
        private System.Windows.Forms.Button settings_saveSettingsButton;
        private System.Windows.Forms.Label settings_listenPortInput_label;
        private System.Windows.Forms.Label settings_serverPortInput_label;
        private System.Windows.Forms.Label settings_serverAddressInput_label;
        private System.Windows.Forms.TextBox send_Status;
        private System.Windows.Forms.ProgressBar send_progressBar;
        private System.Windows.Forms.ProgressBar receive_progressBar;
        private System.Windows.Forms.FolderBrowserDialog receive_selectFolder;
        private System.Windows.Forms.Button receive_startServerButton;
        private System.Windows.Forms.Button receive_selectFolderButton;
        private System.Windows.Forms.TextBox receive_Status;
    }
}

