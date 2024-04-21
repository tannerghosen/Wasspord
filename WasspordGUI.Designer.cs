namespace Wasspord
{
    using System;
    partial class WasspordGUI
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
            this.LocationTextbox = new System.Windows.Forms.TextBox();
            this.AddAccountButton = new System.Windows.Forms.Button();
            this.UpdatePasswordButton = new System.Windows.Forms.Button();
            this.DeleteAccountButton = new System.Windows.Forms.Button();
            this.AutosaveCheckbox = new System.Windows.Forms.CheckBox();
            this.WasspordMenustrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideAccountsPasswordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autosaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miscToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generatePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validatePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToUseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.UsernameTextbox = new System.Windows.Forms.TextBox();
            this.PasswordTextbox = new System.Windows.Forms.TextBox();
            this.LocationsLabel = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.ScrollBar = new System.Windows.Forms.VScrollBar();
            this.WasspordMenustrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LocationTextbox
            // 
            this.LocationTextbox.AcceptsReturn = true;
            this.LocationTextbox.BackColor = System.Drawing.Color.Black;
            this.LocationTextbox.ForeColor = System.Drawing.Color.Black;
            this.LocationTextbox.Location = new System.Drawing.Point(0, 0);
            this.LocationTextbox.Multiline = true;
            this.LocationTextbox.Name = "LocationTextbox";
            this.LocationTextbox.ReadOnly = true;
            this.LocationTextbox.Size = new System.Drawing.Size(175, 9999);
            this.LocationTextbox.TabIndex = 0;
            this.LocationTextbox.UseSystemPasswordChar = true;
            this.LocationTextbox.WordWrap = false;
            // 
            // AddAccountButton
            // 
            this.AddAccountButton.Location = new System.Drawing.Point(65, 414);
            this.AddAccountButton.Name = "AddAccountButton";
            this.AddAccountButton.Size = new System.Drawing.Size(85, 23);
            this.AddAccountButton.TabIndex = 3;
            this.AddAccountButton.Text = "Add Account";
            this.AddAccountButton.UseVisualStyleBackColor = true;
            this.AddAccountButton.Click += new System.EventHandler(this.AddAccountButton_Click);
            // 
            // UpdatePasswordButton
            // 
            this.UpdatePasswordButton.Location = new System.Drawing.Point(156, 414);
            this.UpdatePasswordButton.Name = "UpdatePasswordButton";
            this.UpdatePasswordButton.Size = new System.Drawing.Size(100, 23);
            this.UpdatePasswordButton.TabIndex = 4;
            this.UpdatePasswordButton.Text = "Update Password";
            this.UpdatePasswordButton.UseVisualStyleBackColor = true;
            this.UpdatePasswordButton.Click += new System.EventHandler(this.UpdatePasswordButton_Click);
            // 
            // DeleteAccountButton
            // 
            this.DeleteAccountButton.Location = new System.Drawing.Point(262, 414);
            this.DeleteAccountButton.Name = "DeleteAccountButton";
            this.DeleteAccountButton.Size = new System.Drawing.Size(90, 23);
            this.DeleteAccountButton.TabIndex = 5;
            this.DeleteAccountButton.Text = "Delete Account";
            this.DeleteAccountButton.UseVisualStyleBackColor = true;
            this.DeleteAccountButton.Click += new System.EventHandler(this.DeleteAccountButton_Click);
            // 
            // AutosaveCheckbox
            // 
            this.AutosaveCheckbox.AutoSize = true;
            this.AutosaveCheckbox.Checked = true;
            this.AutosaveCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutosaveCheckbox.Location = new System.Drawing.Point(551, 418);
            this.AutosaveCheckbox.Name = "AutosaveCheckbox";
            this.AutosaveCheckbox.Size = new System.Drawing.Size(71, 17);
            this.AutosaveCheckbox.TabIndex = 6;
            this.AutosaveCheckbox.Text = "Autosave";
            this.AutosaveCheckbox.UseVisualStyleBackColor = true;
            this.AutosaveCheckbox.CheckedChanged += new System.EventHandler(this.AutosaveCheckbox_CheckedChanged);
            // 
            // WasspordMenustrip
            // 
            this.WasspordMenustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.accountsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.miscToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.WasspordMenustrip.Location = new System.Drawing.Point(0, 0);
            this.WasspordMenustrip.Name = "WasspordMenustrip";
            this.WasspordMenustrip.Size = new System.Drawing.Size(634, 24);
            this.WasspordMenustrip.TabIndex = 7;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.newToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.loadToolStripMenuItem.Text = "Open";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // accountsToolStripMenuItem
            // 
            this.accountsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAccountToolStripMenuItem,
            this.updatePasswordToolStripMenuItem,
            this.deleteAccountToolStripMenuItem});
            this.accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
            this.accountsToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.accountsToolStripMenuItem.Text = "Accounts";
            // 
            // addAccountToolStripMenuItem
            // 
            this.addAccountToolStripMenuItem.Name = "addAccountToolStripMenuItem";
            this.addAccountToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.addAccountToolStripMenuItem.Text = "Add Account";
            this.addAccountToolStripMenuItem.Click += new System.EventHandler(this.addAccountToolStripMenuItem_Click);
            // 
            // updatePasswordToolStripMenuItem
            // 
            this.updatePasswordToolStripMenuItem.Name = "updatePasswordToolStripMenuItem";
            this.updatePasswordToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.updatePasswordToolStripMenuItem.Text = "Update Password";
            this.updatePasswordToolStripMenuItem.Click += new System.EventHandler(this.updatePasswordToolStripMenuItem_Click);
            // 
            // deleteAccountToolStripMenuItem
            // 
            this.deleteAccountToolStripMenuItem.Name = "deleteAccountToolStripMenuItem";
            this.deleteAccountToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.deleteAccountToolStripMenuItem.Text = "Delete Account";
            this.deleteAccountToolStripMenuItem.Click += new System.EventHandler(this.deleteAccountToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideAccountsPasswordsToolStripMenuItem,
            this.autosaveToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // showHideAccountsPasswordsToolStripMenuItem
            // 
            this.showHideAccountsPasswordsToolStripMenuItem.Name = "showHideAccountsPasswordsToolStripMenuItem";
            this.showHideAccountsPasswordsToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.showHideAccountsPasswordsToolStripMenuItem.Text = "Show / Hide Accounts/Passwords";
            this.showHideAccountsPasswordsToolStripMenuItem.Click += new System.EventHandler(this.showHideAccountsPasswordsToolStripMenuItem_Click);
            // 
            // autosaveToolStripMenuItem
            // 
            this.autosaveToolStripMenuItem.Name = "autosaveToolStripMenuItem";
            this.autosaveToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.autosaveToolStripMenuItem.Text = "Autosave";
            this.autosaveToolStripMenuItem.Click += new System.EventHandler(this.autosaveToolStripMenuItem_Click);
            // 
            // miscToolStripMenuItem
            // 
            this.miscToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generatePasswordToolStripMenuItem,
            this.validatePasswordToolStripMenuItem});
            this.miscToolStripMenuItem.Name = "miscToolStripMenuItem";
            this.miscToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.miscToolStripMenuItem.Text = "Misc";
            // 
            // generatePasswordToolStripMenuItem
            // 
            this.generatePasswordToolStripMenuItem.Name = "generatePasswordToolStripMenuItem";
            this.generatePasswordToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.generatePasswordToolStripMenuItem.Text = "Generate Password";
            this.generatePasswordToolStripMenuItem.Click += new System.EventHandler(this.generatePasswordToolStripMenuItem_Click);
            // 
            // validatePasswordToolStripMenuItem
            // 
            this.validatePasswordToolStripMenuItem.Name = "validatePasswordToolStripMenuItem";
            this.validatePasswordToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.validatePasswordToolStripMenuItem.Text = "Validate Password";
            this.validatePasswordToolStripMenuItem.Click += new System.EventHandler(this.validatePasswordToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.howToUseToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // howToUseToolStripMenuItem
            // 
            this.howToUseToolStripMenuItem.Name = "howToUseToolStripMenuItem";
            this.howToUseToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.howToUseToolStripMenuItem.Text = "How To Use";
            this.howToUseToolStripMenuItem.Click += new System.EventHandler(this.howToUseToolStripMenuItem_Click);
            // 
            // DisplayButton
            // 
            this.DisplayButton.Location = new System.Drawing.Point(470, 414);
            this.DisplayButton.Name = "DisplayButton";
            this.DisplayButton.Size = new System.Drawing.Size(75, 23);
            this.DisplayButton.TabIndex = 8;
            this.DisplayButton.Text = "Show / Hide";
            this.DisplayButton.UseVisualStyleBackColor = true;
            this.DisplayButton.Click += new System.EventHandler(this.DisplayButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.UsernameTextbox);
            this.panel1.Controls.Add(this.PasswordTextbox);
            this.panel1.Controls.Add(this.LocationTextbox);
            this.panel1.Location = new System.Drawing.Point(65, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 350);
            this.panel1.TabIndex = 9;
            // 
            // UsernameTextbox
            // 
            this.UsernameTextbox.AcceptsReturn = true;
            this.UsernameTextbox.BackColor = System.Drawing.Color.Black;
            this.UsernameTextbox.ForeColor = System.Drawing.Color.Black;
            this.UsernameTextbox.Location = new System.Drawing.Point(175, 0);
            this.UsernameTextbox.Multiline = true;
            this.UsernameTextbox.Name = "UsernameTextbox";
            this.UsernameTextbox.ReadOnly = true;
            this.UsernameTextbox.Size = new System.Drawing.Size(150, 9999);
            this.UsernameTextbox.TabIndex = 11;
            this.UsernameTextbox.UseSystemPasswordChar = true;
            this.UsernameTextbox.WordWrap = false;
            // 
            // PasswordTextbox
            // 
            this.PasswordTextbox.AcceptsReturn = true;
            this.PasswordTextbox.BackColor = System.Drawing.Color.Black;
            this.PasswordTextbox.ForeColor = System.Drawing.Color.Black;
            this.PasswordTextbox.Location = new System.Drawing.Point(325, 0);
            this.PasswordTextbox.Multiline = true;
            this.PasswordTextbox.Name = "PasswordTextbox";
            this.PasswordTextbox.ReadOnly = true;
            this.PasswordTextbox.Size = new System.Drawing.Size(175, 9999);
            this.PasswordTextbox.TabIndex = 10;
            this.PasswordTextbox.UseSystemPasswordChar = true;
            this.PasswordTextbox.WordWrap = false;
            // 
            // LocationsLabel
            // 
            this.LocationsLabel.AutoSize = true;
            this.LocationsLabel.Location = new System.Drawing.Point(120, 28);
            this.LocationsLabel.Name = "LocationsLabel";
            this.LocationsLabel.Size = new System.Drawing.Size(48, 13);
            this.LocationsLabel.TabIndex = 10;
            this.LocationsLabel.Text = "Location";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(288, 28);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(55, 13);
            this.UsernameLabel.TabIndex = 11;
            this.UsernameLabel.Text = "Username";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(443, 28);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(53, 13);
            this.PasswordLabel.TabIndex = 12;
            this.PasswordLabel.Text = "Password";
            // 
            // ScrollBar
            // 
            this.ScrollBar.Location = new System.Drawing.Point(568, 44);
            this.ScrollBar.Maximum = 9649;
            this.ScrollBar.Name = "ScrollBar";
            this.ScrollBar.Size = new System.Drawing.Size(17, 350);
            this.ScrollBar.SmallChange = 10;
            this.ScrollBar.TabIndex = 12;
            this.ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBar_Scroll);
            // 
            // WasspordGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 461);
            this.Controls.Add(this.ScrollBar);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.LocationsLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.DisplayButton);
            this.Controls.Add(this.AutosaveCheckbox);
            this.Controls.Add(this.DeleteAccountButton);
            this.Controls.Add(this.UpdatePasswordButton);
            this.Controls.Add(this.AddAccountButton);
            this.Controls.Add(this.WasspordMenustrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.WasspordMenustrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(650, 500);
            this.MinimumSize = new System.Drawing.Size(650, 500);
            this.Name = "WasspordGUI";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Wasspord";
            this.Load += new System.EventHandler(this.WasspordGUI_Load);
            this.WasspordMenustrip.ResumeLayout(false);
            this.WasspordMenustrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox LocationTextbox;
        private System.Windows.Forms.Button AddAccountButton;
        private System.Windows.Forms.Button UpdatePasswordButton;
        private System.Windows.Forms.Button DeleteAccountButton;
        private System.Windows.Forms.CheckBox AutosaveCheckbox;
        private System.Windows.Forms.MenuStrip WasspordMenustrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.Button DisplayButton;
		private System.Windows.Forms.ToolStripMenuItem howToUseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem accountsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addAccountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem updatePasswordToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteAccountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showHideAccountsPasswordsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem autosaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miscToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generatePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validatePasswordToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox PasswordTextbox;
        public System.Windows.Forms.TextBox UsernameTextbox;
        private System.Windows.Forms.Label LocationsLabel;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.VScrollBar ScrollBar;
    }
}

