﻿namespace Wasspord
{
    partial class UpdatePasswordGUI
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
			this.OKButton = new System.Windows.Forms.Button();
			this.PasswordTextbox = new System.Windows.Forms.TextBox();
			this.UsernameTextbox = new System.Windows.Forms.TextBox();
			this.LocationTextbox = new System.Windows.Forms.TextBox();
			this.PasswordLabel = new System.Windows.Forms.Label();
			this.UsernameLabel = new System.Windows.Forms.Label();
			this.WhereLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// OKButton
			// 
			this.OKButton.Location = new System.Drawing.Point(100, 125);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(75, 23);
			this.OKButton.TabIndex = 13;
			this.OKButton.Text = "OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// PasswordTextbox
			// 
			this.PasswordTextbox.Location = new System.Drawing.Point(99, 83);
			this.PasswordTextbox.Name = "PasswordTextbox";
			this.PasswordTextbox.Size = new System.Drawing.Size(173, 20);
			this.PasswordTextbox.TabIndex = 12;
			// 
			// UsernameTextbox
			// 
			this.UsernameTextbox.Location = new System.Drawing.Point(76, 57);
			this.UsernameTextbox.Name = "UsernameTextbox";
			this.UsernameTextbox.Size = new System.Drawing.Size(196, 20);
			this.UsernameTextbox.TabIndex = 11;
			// 
			// LocationTextbox
			// 
			this.LocationTextbox.Location = new System.Drawing.Point(128, 31);
			this.LocationTextbox.Name = "LocationTextbox";
			this.LocationTextbox.Size = new System.Drawing.Size(144, 20);
			this.LocationTextbox.TabIndex = 10;
			// 
			// PasswordLabel
			// 
			this.PasswordLabel.AutoSize = true;
			this.PasswordLabel.Location = new System.Drawing.Point(12, 83);
			this.PasswordLabel.Name = "PasswordLabel";
			this.PasswordLabel.Size = new System.Drawing.Size(81, 13);
			this.PasswordLabel.TabIndex = 9;
			this.PasswordLabel.Text = "New Password:";
			// 
			// UsernameLabel
			// 
			this.UsernameLabel.AutoSize = true;
			this.UsernameLabel.Location = new System.Drawing.Point(10, 57);
			this.UsernameLabel.Name = "UsernameLabel";
			this.UsernameLabel.Size = new System.Drawing.Size(58, 13);
			this.UsernameLabel.TabIndex = 8;
			this.UsernameLabel.Text = "Username:";
			// 
			// WhereLabel
			// 
			this.WhereLabel.AutoSize = true;
			this.WhereLabel.Location = new System.Drawing.Point(12, 34);
			this.WhereLabel.Name = "WhereLabel";
			this.WhereLabel.Size = new System.Drawing.Size(110, 13);
			this.WhereLabel.TabIndex = 7;
			this.WhereLabel.Text = "Location (Site/Place):";
			// 
			// UpdatePasswordGUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 161);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.PasswordTextbox);
			this.Controls.Add(this.UsernameTextbox);
			this.Controls.Add(this.LocationTextbox);
			this.Controls.Add(this.PasswordLabel);
			this.Controls.Add(this.UsernameLabel);
			this.Controls.Add(this.WhereLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(300, 200);
			this.MinimumSize = new System.Drawing.Size(300, 200);
			this.Name = "UpdatePasswordGUI";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Update Password";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.TextBox PasswordTextbox;
        private System.Windows.Forms.TextBox UsernameTextbox;
        private System.Windows.Forms.TextBox LocationTextbox;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Label WhereLabel;
    }
}