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
			this.OutputTextbox = new System.Windows.Forms.TextBox();
			this.AddAccountButton = new System.Windows.Forms.Button();
			this.UpdatePasswordButton = new System.Windows.Forms.Button();
			this.DeleteAccountButton = new System.Windows.Forms.Button();
			this.AutosaveCheckbox = new System.Windows.Forms.CheckBox();
			this.WasspordMenustrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.DisplayButton = new System.Windows.Forms.Button();
			this.WasspordMenustrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// OutputTextbox
			// 
			this.OutputTextbox.AcceptsReturn = true;
			this.OutputTextbox.ForeColor = System.Drawing.SystemColors.WindowText;
			this.OutputTextbox.Location = new System.Drawing.Point(65, 50);
			this.OutputTextbox.Multiline = true;
			this.OutputTextbox.Name = "OutputTextbox";
			this.OutputTextbox.ReadOnly = true;
			this.OutputTextbox.Size = new System.Drawing.Size(500, 350);
			this.OutputTextbox.TabIndex = 0;
			this.OutputTextbox.UseSystemPasswordChar = true;
			this.OutputTextbox.Visible = false;
			this.OutputTextbox.WordWrap = false;
			// 
			// AddAccountButton
			// 
			this.AddAccountButton.Location = new System.Drawing.Point(65, 406);
			this.AddAccountButton.Name = "AddAccountButton";
			this.AddAccountButton.Size = new System.Drawing.Size(85, 23);
			this.AddAccountButton.TabIndex = 3;
			this.AddAccountButton.Text = "Add Account";
			this.AddAccountButton.UseVisualStyleBackColor = true;
			this.AddAccountButton.Click += new System.EventHandler(this.AddAccountButton_Click);
			// 
			// UpdatePasswordButton
			// 
			this.UpdatePasswordButton.Location = new System.Drawing.Point(252, 406);
			this.UpdatePasswordButton.Name = "UpdatePasswordButton";
			this.UpdatePasswordButton.Size = new System.Drawing.Size(100, 23);
			this.UpdatePasswordButton.TabIndex = 4;
			this.UpdatePasswordButton.Text = "Update Password";
			this.UpdatePasswordButton.UseVisualStyleBackColor = true;
			this.UpdatePasswordButton.Click += new System.EventHandler(this.UpdatePasswordButton_Click);
			// 
			// DeleteAccountButton
			// 
			this.DeleteAccountButton.Location = new System.Drawing.Point(156, 406);
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
			this.AutosaveCheckbox.Location = new System.Drawing.Point(551, 412);
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
            this.saveAsToolStripMenuItem});
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
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// DisplayButton
			// 
			this.DisplayButton.Location = new System.Drawing.Point(470, 406);
			this.DisplayButton.Name = "DisplayButton";
			this.DisplayButton.Size = new System.Drawing.Size(75, 23);
			this.DisplayButton.TabIndex = 8;
			this.DisplayButton.Text = "Show / Hide";
			this.DisplayButton.UseVisualStyleBackColor = true;
			this.DisplayButton.Click += new System.EventHandler(this.DisplayButton_Click);
			// 
			// WasspordGUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(634, 461);
			this.Controls.Add(this.DisplayButton);
			this.Controls.Add(this.AutosaveCheckbox);
			this.Controls.Add(this.DeleteAccountButton);
			this.Controls.Add(this.UpdatePasswordButton);
			this.Controls.Add(this.AddAccountButton);
			this.Controls.Add(this.OutputTextbox);
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
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox OutputTextbox;
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
	}
}

