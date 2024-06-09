﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Wasspord
{
	public partial class WasspordGUI : Form
	{
        private int WX = 0;
        private int WY = 0;

        public WasspordGUI()
		{
			InitializeComponent();
            showHideAccountsPasswordsToolStripMenuItem.Text = Wasspord.Display ? "Show / Hide (ON)" : "Show / Hide (OFF)";
            autosaveToolStripMenuItem.Text = Wasspord.Autosave ? "Autosave (ON)" : "Autosave (OFF)";
        }
        private void AddAccountButton_Click(object sender, EventArgs e)
		{
            Account("add");
			if (Wasspord.Autosave == true)
				Wasspord.Save(Wasspord.Filename, Wasspord.Folder);
			LocationTextbox.Text = Wasspord.Print("Location");
            UsernameTextbox.Text = Wasspord.Print("Username");
            PasswordTextbox.Text = Wasspord.Print("Password");
        }

		private void DeleteAccountButton_Click(object sender, EventArgs e)
		{
            Account("delete");
            if (Wasspord.Autosave == true)
                Wasspord.Save(Wasspord.Filename, Wasspord.Folder);
            LocationTextbox.Text = Wasspord.Print("Location");
            UsernameTextbox.Text = Wasspord.Print("Username");
            PasswordTextbox.Text = Wasspord.Print("Password");
        }

		private void UpdatePasswordButton_Click(object sender, EventArgs e)
		{
            Account("update");
            if (Wasspord.Autosave == true)
                Wasspord.Save(Wasspord.Filename, Wasspord.Folder);
            LocationTextbox.Text = Wasspord.Print("Location");
            UsernameTextbox.Text = Wasspord.Print("Username");
            PasswordTextbox.Text = Wasspord.Print("Password");
        }

		private void WasspordGUI_Load(object sender, EventArgs e)
		{
            Location = new Point((Screen.PrimaryScreen.Bounds.Width - Width) / 2, (Screen.PrimaryScreen.Bounds.Height - Height) / 2);
            LocationTextbox.Text = Wasspord.Print("Location");
            UsernameTextbox.Text = Wasspord.Print("Username");
            PasswordTextbox.Text = Wasspord.Print("Password");
			LocationTextbox.ForeColor = UsernameTextbox.ForeColor = PasswordTextbox.ForeColor = Color.FromName("White");
            if (Wasspord.Display == false)
                PasswordTextbox.ForeColor = Color.FromName("Black");
        }

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Wasspord.Filename == "" && Wasspord.Folder == Directory.GetCurrentDirectory() + "\\Accounts\\")
			{
				saveAsToolStripMenuItem_Click(sender, e);
			}
			else
			{
				Wasspord.Save(Wasspord.Folder, Wasspord.Filename);
			}
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
            SaveFileDialog();
        }

		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog();
            LocationTextbox.Text = Wasspord.Print("Location");
            UsernameTextbox.Text = Wasspord.Print("Username");
            PasswordTextbox.Text = Wasspord.Print("Password");
        }
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form AboutForm = new Form();
			AboutForm.Text = "Wasspord";
			AboutForm.Width = 400;
			AboutForm.Height = 300;
			AboutForm.MaximizeBox = false;
			AboutForm.FormBorderStyle = FormBorderStyle.FixedSingle;
			Label AboutFormLabel = new Label();
			AboutFormLabel.Location = new Point(125, 175);
			AboutFormLabel.Size = new Size(250, 50);
			AboutFormLabel.Text = "The Password Manager\r\nBy Tanner Ghosen\r\n2023 - 2024";
			LinkLabel AboutFormLinkLabel = new LinkLabel();
			AboutFormLinkLabel.Location = new Point(75, 225);
			AboutFormLinkLabel.Size = new Size(250, 15);
			AboutFormLinkLabel.Text = "https://www.github.com/tannerghosen/Wasspord";
			AboutFormLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(GitHubLinkClicked);
			LinkLabel AboutFormLinkLabel2 = new LinkLabel();
			AboutFormLinkLabel2.Location = new Point(75, 240);
			AboutFormLinkLabel2.Size = new Size(250, 15);
			AboutFormLinkLabel2.Text = "Glitches or Bugs? Report them here.";
			AboutFormLinkLabel2.LinkClicked += new LinkLabelLinkClickedEventHandler(IssuesLinkClicked);
			AboutForm.Controls.Add(AboutFormLabel);
			AboutForm.Controls.Add(AboutFormLinkLabel);
			AboutForm.Controls.Add(AboutFormLinkLabel2);
            AboutForm.Load += (s, ev) =>
            {
                AboutForm.Location = new Point((WX + (Width - AboutForm.Width) / 2), (WY + (Height - AboutForm.Height) / 2));
            };
			AboutForm.ShowDialog();
		}

		private void DisplayButton_Click(object sender, EventArgs e)
		{
            PasswordTextbox.ForeColor = PasswordTextbox.ForeColor == Color.FromName("Black") ? Color.FromName("White") : Color.FromName("Black");
            Wasspord.UpdateSettings("Display");
            showHideAccountsPasswordsToolStripMenuItem.Text = Wasspord.Display ? "Show / Hide (ON)" : "Show / Hide (OFF)";
        }

		private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form HelpForm = new Form();
			HelpForm.Text = "How to use Wasspord";
			HelpForm.Width = 400;
			HelpForm.Height = 300;
			HelpForm.MaximizeBox = false;
			HelpForm.FormBorderStyle = FormBorderStyle.FixedSingle;
			Label HelpFormLabel = new Label();
			HelpFormLabel.Location = new Point(75, 50);
			HelpFormLabel.Size = new Size(250, 300);
			HelpFormLabel.Text = "Wasspord's features and what they do:\r\n\r\n* Save/Save As/Load: Saves to a loaded file, saves to a new file, and loads an existing account file. These files are .wasspord extension files, and contains the details for your accounts.\r\n* Add Account: Adds an account to the account list.\r\n* Update Password: Updates an account's password.\r\n* Delete Account: Deletes an account.\r\n* Show / Hide: Shows/hides your account details. Hidden by default.\r\n* Autosave: Toggles the ability to automatically save to the loaded account file. Off by default.";
			HelpFormLabel.BackColor = Color.Transparent;
			LinkLabel HelpFormLinkLabel = new LinkLabel();
			HelpFormLinkLabel.Location = new Point(75, 30);
			HelpFormLinkLabel.Size = new Size(250, 15);
			HelpFormLinkLabel.Text = "Report bugs and glitches to the repo's issues!";
			HelpFormLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(IssuesLinkClicked);
			HelpForm.Controls.Add(HelpFormLabel);
			HelpForm.Controls.Add(HelpFormLinkLabel);
            HelpForm.Load += (s, ev) =>
            {
                HelpForm.Location = new Point((WX + (Width - HelpForm.Width) / 2), (WY + (Height - HelpForm.Height) / 2));
            };
            HelpForm.ShowDialog();
		}

        private void GitHubLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.github.com/tannerghosen/Wasspord");
        }
        private void IssuesLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/tannerghosen/Wasspord/issues");
        }

        private void addAccountToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddAccountButton_Click(sender, e);
		}

		private void updatePasswordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdatePasswordButton_Click(sender, e);
		}

		private void deleteAccountToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DeleteAccountButton_Click(sender, e);
		}

		private void autosaveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Wasspord.Autosave == true)
			{
                Wasspord.UpdateSettings("Autosave");
			}
			else if (Wasspord.Autosave == false)
			{
                Wasspord.UpdateSettings("Autosave");
			}
            autosaveToolStripMenuItem.Text = Wasspord.Autosave ? "Autosave (ON)" : "Autosave (OFF)";
        }

		private void showHideAccountsPasswordsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DisplayButton_Click(sender, e);
		}
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Wasspord.Reset();
			LocationTextbox.Text = UsernameTextbox.Text = PasswordTextbox.Text = "";
        }

        private void generatePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form PassForm = new Form();
            PassForm.Text = "Generated Password";
            PassForm.Width = 300;
            PassForm.Height = 150;
            PassForm.MaximizeBox = false;
            PassForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            Button PassFormOKButton = new Button();
            PassFormOKButton.Text = "OK";
            PassFormOKButton.Width = 75;
			PassFormOKButton.Height = 23;
            PassFormOKButton.Location = new Point(100, 75);
            TextBox PassFormTextBox = new TextBox();
            PassFormTextBox.Text = WasspordExtras.GeneratePassword();
            PassFormTextBox.Location = new Point(80, 50);
            PassFormTextBox.Height = 23;
            PassFormTextBox.Width = 120;
            Label PassFormLabel = new Label();
            PassFormLabel.Text = "Here's your generated password!";
            PassFormLabel.Location = new Point(60, 30);
            PassFormLabel.Width = 200;
            PassForm.Controls.Add(PassFormOKButton);
            PassForm.Controls.Add(PassFormTextBox);
            PassForm.Controls.Add(PassFormLabel);
            PassFormOKButton.Click += (s, ev) =>
            {
				PassForm.Close();
            };
            PassForm.Load += (s, ev) =>
            {
                PassForm.Location = new Point((WX + (Width - PassForm.Width) / 2), (WY + (Height - PassForm.Height) / 2));
            };
            PassForm.ShowDialog();
        }

        private void validatePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Form ValidateForm = new Form();
            ValidateForm.Text = "Validate Password";
            ValidateForm.Width = 300;
            ValidateForm.Height = 150;
            TextBox ValidateFormTextBox = new TextBox();
            ValidateFormTextBox.Text = "";
            ValidateFormTextBox.Location = new Point(80, 50);
            ValidateFormTextBox.Height = 23;
            ValidateFormTextBox.Width = 120;
            Label ValidateFormLabel = new Label();
            ValidateFormLabel.Text = "Input your password in the textbox below,\nand press OK to validate it.";
            ValidateFormLabel.Location = new Point(45, 20);
            ValidateFormLabel.Width = 300;
			ValidateFormLabel.Height = 50;
            Button ValidateFormOKButton = new Button();
            ValidateFormOKButton.Text = "OK";
            ValidateFormOKButton.Width = 75;
            ValidateFormOKButton.Height = 23;
            ValidateFormOKButton.Location = new Point(100, 75);
            ValidateFormOKButton.Click += (s, ev) =>
			{
				ValidateFormOKButton_Click(ValidateFormTextBox.Text);
                ValidateForm.Close();
            };
            ValidateForm.Controls.Add(ValidateFormTextBox);
            ValidateForm.Controls.Add(ValidateFormLabel);
            ValidateForm.Controls.Add(ValidateFormOKButton);
            ValidateForm.Load += (s, ev) =>
            {
                ValidateForm.Location = new Point((WX + (Width - ValidateForm.Width) / 2), (WY + (Height - ValidateForm.Height) / 2));
            };
            ValidateForm.ShowDialog();
        }
        private void ValidateFormOKButton_Click(string password)
        {
            Form ValidateForm = new Form();
            ValidateForm.Text = "Validate Password";
            ValidateForm.Width = 350;
            ValidateForm.Height = 175;
            Label ValidateFormLabel = new Label();
            ValidateFormLabel.Text = WasspordExtras.ValidatePassword(password);
            ValidateFormLabel.Location = new Point(0, 20);
            ValidateFormLabel.Width = 335;
            ValidateFormLabel.Height = 75;
            Button ValidateFormOKButton = new Button();
            ValidateFormOKButton.Text = "OK";
            ValidateFormOKButton.Width = 75;
            ValidateFormOKButton.Height = 23;
            ValidateFormOKButton.Location = new Point(125, 100);
            ValidateFormOKButton.Click += (s, ev) =>
            {
                ValidateForm.Close();
            };
            ValidateForm.Controls.Add(ValidateFormLabel);
            ValidateForm.Controls.Add(ValidateFormOKButton);
            ValidateForm.Load += (s, ev) =>
            {
                ValidateForm.Location = new Point((WX + (Width - ValidateForm.Width) / 2), (WY + (Height - ValidateForm.Height) / 2));
            };
            ValidateForm.ShowDialog();
        }

        private void accountsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountsFolderDialog();
        }

        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            LocationTextbox.Top = -ScrollBar.Value;
            UsernameTextbox.Top = -ScrollBar.Value;
            PasswordTextbox.Top = -ScrollBar.Value;
        }
        private void WasspordGUI_LocationChanged(object sender, EventArgs e)
        {
            WX = Location.X;
            WY = Location.Y;
            //Logger.Write("Locations: X: " + WX + " Y: " + WY + ".","DEBUG");
        }

        /* Account: Sends adding/deleting/updating requests to Wasspord via GUI depending on type.
         * Parameters: type (determines request being made and GUI displayed */
        private void Account(string type)
        {
            Form AccountForm = new Form();
            AccountForm.Text = type.Substring(0, 1).ToUpper() + type.Substring(1, type.Length-1) + " Account";
            AccountForm.Width = 300;
            AccountForm.Height = 200;
            Label WhereLabel = new Label();
            WhereLabel.Text = "Location (Site/Place):";
            WhereLabel.Location = new Point(12, 38);
            WhereLabel.Width = 110;
            WhereLabel.Height = 13;
            Label UsernameLabel = new Label();
            UsernameLabel.Text = "Username:";
            UsernameLabel.Width = 58;
            UsernameLabel.Height = 13;
            UsernameLabel.Location = new Point(10, 64);
            Label PasswordLabel = new Label();
            PasswordLabel.Text = "Password:";
            PasswordLabel.Width = 56;
            PasswordLabel.Height = 13;
            PasswordLabel.Location = new Point(10, 92);
            TextBox LocationTextbox = new TextBox();
            LocationTextbox.Location = new Point(128, 35);
            LocationTextbox.Width = 144;
            LocationTextbox.Height = 20;
            TextBox UsernameTextbox = new TextBox();
            UsernameTextbox.Location = new Point(76, 61);
            UsernameTextbox.Width = 196;
            UsernameTextbox.Height = 20;
            TextBox PasswordTextbox = new TextBox();
            PasswordTextbox.Location = new Point(76, 87);
            PasswordTextbox.Width = 196;
            PasswordTextbox.Height = 20;
            Button OKButton = new Button();
            OKButton.Width = 75;
            OKButton.Height = 23;
            OKButton.Text = "OK";
            OKButton.Location = new Point(100, 125);
            OKButton.Click += (s, ev) =>
            {
                switch (type)
                {
                    case "add":
                        Wasspord.ManageAccount("add", LocationTextbox.Text, UsernameTextbox.Text, PasswordTextbox.Text);
                        break;
                    case "update":
                        Wasspord.ManageAccount("update", LocationTextbox.Text, UsernameTextbox.Text, PasswordTextbox.Text);
                        break;
                    case "delete":
                        Wasspord.ManageAccount("delete", LocationTextbox.Text, UsernameTextbox.Text);
                        break;
                }
                AccountForm.Close();
            };
            AccountForm.Controls.Add(WhereLabel);
            AccountForm.Controls.Add(UsernameLabel);
            AccountForm.Controls.Add(LocationTextbox);
            AccountForm.Controls.Add(UsernameTextbox);
            if (type == "add" || type == "update")
            {
                AccountForm.Controls.Add(PasswordLabel);
                AccountForm.Controls.Add(PasswordTextbox);
            }
            AccountForm.Controls.Add(OKButton);
            AccountForm.Load += (s, ev) =>
            {
                AccountForm.Location = new Point((WX + (Width - AccountForm.Width) / 2), (WY + (Height - AccountForm.Height) / 2));
            };
            AccountForm.ShowDialog();

        }
        /* OpenFileDialog: Used to load .wasspord files. */
        private static void OpenFileDialog()
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Title = "Open";
            of.Filter = "Wasspord Text File|*.wasspord";
            of.InitialDirectory = Wasspord.Folder;
            of.RestoreDirectory = true;
            //Debug.WriteLine("DEBUG: Load Directory: " + of.InitialDirectory);
            if (of.ShowDialog() == DialogResult.OK)
            {
                Wasspord.ClearAccounts();
                Wasspord.Filename = Path.GetFileName(of.FileName);
                Wasspord.Folder = Path.GetDirectoryName(of.FileName);
                Wasspord.Load(Wasspord.Folder, Wasspord.Filename);
            }
        }

        /* SaveFileDialog: Used to save .wasspord files. */
        private static void SaveFileDialog()
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Title = "Save";
            sf.Filter = "Wasspord Text File|*.wasspord";
            sf.InitialDirectory = Wasspord.Folder;
            //Debug.WriteLine("DEBUG: Save Directory: " + sf.InitialDirectory);
            sf.RestoreDirectory = true;
            if (sf.ShowDialog() == DialogResult.OK)
            {
                Wasspord.Filename = Path.GetFileName(sf.FileName);
                Wasspord.Folder = Path.GetDirectoryName(sf.FileName);
                Wasspord.Save(Wasspord.Folder, Wasspord.Filename);
            }
        }

        /* AccountsFolderDialog: Used to select the default Accounts folder to save/open files from by default. */
        private static void AccountsFolderDialog()
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();

            if (fd.ShowDialog() == DialogResult.OK)
            {
                string path = fd.SelectedPath;
                path = fd.SelectedPath.Replace(@"\", @"\\"); // replace \'s with \\ to avoid a JSON error using escape characters
                Wasspord.UpdateSettings("Folder", path);
            }
        }
    }
}
