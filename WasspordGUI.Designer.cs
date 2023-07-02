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
            this.SaveButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.AddAccountButton = new System.Windows.Forms.Button();
            this.UpdatePasswordButton = new System.Windows.Forms.Button();
            this.DeleteAccountButton = new System.Windows.Forms.Button();
            this.AutosaveCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // OutputTextbox
            // 
            this.OutputTextbox.AcceptsReturn = true;
            this.OutputTextbox.Location = new System.Drawing.Point(65, 50);
            this.OutputTextbox.Multiline = true;
            this.OutputTextbox.Name = "OutputTextbox";
            this.OutputTextbox.ReadOnly = true;
            this.OutputTextbox.Size = new System.Drawing.Size(500, 350);
            this.OutputTextbox.TabIndex = 0;
            this.OutputTextbox.WordWrap = false;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(12, 12);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(93, 12);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 2;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // AddAccountButton
            // 
            this.AddAccountButton.Location = new System.Drawing.Point(12, 426);
            this.AddAccountButton.Name = "AddAccountButton";
            this.AddAccountButton.Size = new System.Drawing.Size(85, 23);
            this.AddAccountButton.TabIndex = 3;
            this.AddAccountButton.Text = "Add Account";
            this.AddAccountButton.UseVisualStyleBackColor = true;
            this.AddAccountButton.Click += new System.EventHandler(this.AddAccountButton_Click);
            // 
            // UpdatePasswordButton
            // 
            this.UpdatePasswordButton.Location = new System.Drawing.Point(199, 426);
            this.UpdatePasswordButton.Name = "UpdatePasswordButton";
            this.UpdatePasswordButton.Size = new System.Drawing.Size(100, 23);
            this.UpdatePasswordButton.TabIndex = 4;
            this.UpdatePasswordButton.Text = "Update Password";
            this.UpdatePasswordButton.UseVisualStyleBackColor = true;
            this.UpdatePasswordButton.Click += new System.EventHandler(this.UpdatePasswordButton_Click);
            // 
            // DeleteAccountButton
            // 
            this.DeleteAccountButton.Location = new System.Drawing.Point(103, 426);
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
            this.AutosaveCheckbox.Location = new System.Drawing.Point(542, 406);
            this.AutosaveCheckbox.Name = "AutosaveCheckbox";
            this.AutosaveCheckbox.Size = new System.Drawing.Size(71, 17);
            this.AutosaveCheckbox.TabIndex = 6;
            this.AutosaveCheckbox.Text = "Autosave";
            this.AutosaveCheckbox.UseVisualStyleBackColor = true;
            this.AutosaveCheckbox.CheckedChanged += new System.EventHandler(this.AutosaveCheckbox_CheckedChanged);
            // 
            // WasspordGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 461);
            this.Controls.Add(this.AutosaveCheckbox);
            this.Controls.Add(this.DeleteAccountButton);
            this.Controls.Add(this.UpdatePasswordButton);
            this.Controls.Add(this.AddAccountButton);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.OutputTextbox);
            this.Name = "WasspordGUI";
            this.Text = "Wasspord";
            this.Load += new System.EventHandler(this.WasspordGUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox OutputTextbox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button AddAccountButton;
        private System.Windows.Forms.Button UpdatePasswordButton;
        private System.Windows.Forms.Button DeleteAccountButton;
        private System.Windows.Forms.CheckBox AutosaveCheckbox;
    }
}

