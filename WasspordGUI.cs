using System;
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
            showHideAccountsPasswordsToolStripMenuItem.Text = WasspordSettings.Display ? "Show / Hide (ON)" : "Show / Hide (OFF)";
            autosaveToolStripMenuItem.Text = WasspordSettings.Autosave ? "Autosave (ON)" : "Autosave (OFF)";
        }
        private void AddAccountButton_Click(object sender, EventArgs e)
		{
            Account("add");
			if (WasspordSettings.Autosave == true)
				WasspordFilesHandler.Save(WasspordFilesHandler.Filename, WasspordFilesHandler.Folder);
            PrintRows();
        }

		private void DeleteAccountButton_Click(object sender, EventArgs e)
		{
            Account("delete");
            if (WasspordSettings.Autosave == true)
                WasspordFilesHandler.Save(WasspordFilesHandler.Filename, WasspordFilesHandler.Folder);
            PrintRows();
        }

		private void UpdatePasswordButton_Click(object sender, EventArgs e)
		{
            Account("update");
            if (WasspordSettings.Autosave == true)
                WasspordFilesHandler.Save(WasspordFilesHandler.Filename, WasspordFilesHandler.Folder);
            PrintRows();
        }

		private void WasspordGUI_Load(object sender, EventArgs e)
		{
            Location = new Point((Screen.PrimaryScreen.Bounds.Width - Width) / 2, (Screen.PrimaryScreen.Bounds.Height - Height) / 2);
			LocationTextbox.ForeColor = UsernameTextbox.ForeColor = PasswordTextbox.ForeColor = Color.FromName("White");
            if (WasspordSettings.Display == false)
                PasswordTextbox.ForeColor = Color.FromName("Black");
        }

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (WasspordFilesHandler.Filename == null)
			{
				saveAsToolStripMenuItem_Click(sender, e);
			}
			else
			{
				WasspordFilesHandler.Save(WasspordFilesHandler.Folder, WasspordFilesHandler.Filename);
			}
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
            bool save = SaveFileDialog();
            if (save == true)
            {
                SavePasswordPrompt();
                WasspordFilesHandler.Save(WasspordFilesHandler.Folder, WasspordFilesHandler.Filename);
            }
        }

		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
            string wppw = WasspordFilesHandler.GetWasspordPassword();
            WasspordFilesHandler.SetWasspordPassword("");
            bool load = OpenFileDialog();
            if (load == true)
            {
                string pass = WasspordFilesHandler.GetWasspordPassword();
                if (pass != "") // if password is not null
                {
                    bool passwordprompt = PasswordPrompt();
                    if (passwordprompt == true) // if password check passes
                    {
                        PrintRows(); // print rows
                    }
                    else if (passwordprompt == false) // if password check fails
                    {
                        Error("Invalid password given.");
                        newToolStripMenuItem_Click(sender, e); // We just reset and blank the thing.
                    }
                }
                else
                {
                    PrintRows(); // print rows
                }
            }
            else
            {
                WasspordFilesHandler.SetWasspordPassword(wppw);
            }
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
			AboutFormLinkLabel2.Text = "Glitches or Bugs? Click here to report them.";
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
            WasspordSettings.UpdateSettings("Display");
            showHideAccountsPasswordsToolStripMenuItem.Text = WasspordSettings.Display ? "Show / Hide (ON)" : "Show / Hide (OFF)";
        }

		private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form HelpForm = new Form();
			HelpForm.Text = "How to use Wasspord";
			HelpForm.Width = 400;
			HelpForm.Height = 400;
			HelpForm.MaximizeBox = false;
			HelpForm.FormBorderStyle = FormBorderStyle.FixedSingle;
			Label HelpFormLabel = new Label();
			HelpFormLabel.Location = new Point(75, 50);
			HelpFormLabel.Size = new Size(250, 300);
			HelpFormLabel.Text = "Wasspord's features and what they do:\r\n\r\n* Save/Save As/Load: Saves to a loaded file, saves to a new file, and loads an existing account file. These files are .wasspord extension files, and contains the details for your accounts.\r\n* Add Account: Adds an account to the account list.\r\n* Update Password: Updates an account's password.\r\n* Delete Account: Deletes an account.\r\n* Show / Hide: Shows/hides your account details. Hidden by default.\r\n* Autosave: Toggles the ability to automatically save to the loaded account file. Off by default.\r\n* Custom Accounts Folder: Sets a custom default folder for .wasspord files.\r\n* Generate Password: Generates a randomized password that is secure.\r\n* Validate Password: Validates a password against a standard to ensure it's strong.";
			HelpFormLabel.BackColor = Color.Transparent;
			LinkLabel HelpFormLinkLabel = new LinkLabel();
			HelpFormLinkLabel.Location = new Point(75, 30);
			HelpFormLinkLabel.Size = new Size(250, 15);
			HelpFormLinkLabel.Text = "Report bugs and glitches here!";
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
        private void LinkClicked(string link, object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(link);
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
			if (WasspordSettings.Autosave == true)
			{
                WasspordSettings.UpdateSettings("Autosave");
			}
			else if (WasspordSettings.Autosave == false)
			{
                WasspordSettings.UpdateSettings("Autosave");
			}
            autosaveToolStripMenuItem.Text = WasspordSettings.Autosave ? "Autosave (ON)" : "Autosave (OFF)";
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
                        WasspordAccounts.ManageAccount("add", LocationTextbox.Text, UsernameTextbox.Text, PasswordTextbox.Text);
                        break;
                    case "update":
                        WasspordAccounts.ManageAccount("update", LocationTextbox.Text, UsernameTextbox.Text, PasswordTextbox.Text);
                        break;
                    case "delete":
                        WasspordAccounts.ManageAccount("delete", LocationTextbox.Text, UsernameTextbox.Text);
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
        private bool OpenFileDialog()
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Title = "Open";
            of.Filter = "Wasspord Text File|*.wasspord";
            of.InitialDirectory = WasspordFilesHandler.Folder;
            of.RestoreDirectory = true;
            if (of.ShowDialog() == DialogResult.OK)
            {
                WasspordFilesHandler.Filename = Path.GetFileName(of.FileName);
                WasspordFilesHandler.Folder = Path.GetDirectoryName(of.FileName); // this is temporary and not actually saved into the settings.json.
                WasspordFilesHandler.Load(WasspordFilesHandler.Folder, WasspordFilesHandler.Filename);
                return true;
            }
            else
            {
                return false;
            }
        }

        /* SaveFileDialog: Used to save .wasspord files. */
        private bool SaveFileDialog()
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Title = "Save";
            sf.Filter = "Wasspord Text File|*.wasspord";
            sf.InitialDirectory = WasspordFilesHandler.Folder;
            sf.RestoreDirectory = true;
            if (sf.ShowDialog() == DialogResult.OK)
            {
                WasspordFilesHandler.Filename = Path.GetFileName(sf.FileName);
                WasspordFilesHandler.Folder = Path.GetDirectoryName(sf.FileName); // this is temporary and not actually saved into the settings.json.
                WasspordFilesHandler.Save(WasspordFilesHandler.Folder, WasspordFilesHandler.Filename);
                return true;
            }
            else
            {
                return false;
            }
        }

        /* AccountsFolderDialog: Used to select the default Accounts folder to save/open files from by default. */
        // This method and UpdateSettings("folder", string) is the only way to actually change the folder the program opens with,
        // as any Folder value change caused by loading/saving a file is only temporary and only lasts as long as the program is up.
        private void AccountsFolderDialog()
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();

            if (fd.ShowDialog() == DialogResult.OK)
            {
                string path = fd.SelectedPath;
                path = fd.SelectedPath.Replace(@"\", @"\\"); // replace \'s with \\ to avoid a JSON error using escape characters
                WasspordSettings.UpdateSettings("Folder", path);
                if(WasspordFilesHandler.Filename == null)
                    WasspordFilesHandler.Folder = WasspordSettings.Folder; // Might as well update it to the custom folder if Filename is null
            }
        }

        /* PrintRows: Does exactly what it says, it prints rows out of the dictionary. */
        private void PrintRows()
        {
            // Maybe should look for a better way to do this that wouldn't cause us to empty the textboxes everytime this is called.
            LocationTextbox.Text = UsernameTextbox.Text = PasswordTextbox.Text = "";
            for (int i = 0; i < WasspordAccounts.Accounts.Count; i++)
            {
                var item = WasspordAccounts.GetRow(i);
                LocationTextbox.Text += Encryption.Decrypt(item[0]) + Environment.NewLine;
                UsernameTextbox.Text += Encryption.Decrypt(item[1]) + Environment.NewLine;
                PasswordTextbox.Text += Encryption.Decrypt(item[2]) + Environment.NewLine;
            }
        }

        /* PasswordPrompt: Prompts for password */
        private bool PasswordPrompt()
        {
            string pass = WasspordFilesHandler.GetWasspordPassword();
            bool success = false;
            Form PassForm = new Form();
            PassForm.Text = "Load - Input Password";
            PassForm.Width = 300;
            PassForm.Height = 200;
            Label PassLabel = new Label();
            PassLabel.Text = "Password:";
            PassLabel.Location = new Point(12, 38);
            PassLabel.Width = 110;
            PassLabel.Height = 13;
            TextBox PassTextbox = new TextBox();
            PassTextbox.Location = new Point(128, 35);
            PassTextbox.Width = 144;
            PassTextbox.Height = 20;
            Button OKButton = new Button();
            OKButton.Width = 75;
            OKButton.Height = 23;
            OKButton.Text = "OK";
            OKButton.Location = new Point(100, 125);
            OKButton.Click += (s, ev) =>
            {
                if (PassTextbox.Text == pass)
                {
                    success = true;
                    PassForm.Close();
                }
                else
                {
                    success = false;
                    PassForm.Close();
                }
            };
            PassForm.Controls.Add(PassLabel);
            PassForm.Controls.Add(PassTextbox);
            PassForm.Controls.Add(OKButton);
            PassForm.Load += (s, ev) =>
            {
                PassForm.Location = new Point((WX + (Width - PassForm.Width) / 2), (WY + (Height - PassForm.Height) / 2));
            };
            PassForm.ShowDialog();

            return success;
        }

        /* SavePasswordPrompt: Save a password to a .wasspord file to protect it */
        private void SavePasswordPrompt()
        {
            Form PassForm = new Form();
            PassForm.Text = "Save As - Input Password";
            PassForm.Width = 300;
            PassForm.Height = 200;
            Label PassLabel = new Label();
            PassLabel.Text = "Password:";
            PassLabel.Location = new Point(12, 38);
            PassLabel.Width = 110;
            PassLabel.Height = 13;
            TextBox PassTextbox = new TextBox();
            PassTextbox.Location = new Point(128, 35);
            PassTextbox.Width = 144;
            PassTextbox.Height = 20;
            Button OKButton = new Button();
            OKButton.Width = 75;
            OKButton.Height = 23;
            OKButton.Text = "OK";
            OKButton.Location = new Point(100, 125);
            OKButton.Click += (s, ev) =>
            {
                if (PassTextbox.Text != "")
                {
                    WasspordFilesHandler.SetWasspordPassword(PassTextbox.Text);
                    PassForm.Close();
                }
                else
                {
                    WasspordFilesHandler.SetWasspordPassword("");
                    PassForm.Close();
                }
            };
            PassForm.Controls.Add(PassLabel);
            PassForm.Controls.Add(PassTextbox);
            PassForm.Controls.Add(OKButton);
            PassForm.Load += (s, ev) =>
            {
                PassForm.Location = new Point((WX + (Width - PassForm.Width) / 2), (WY + (Height - PassForm.Height) / 2));
            };
            PassForm.ShowDialog();
        }

        /* Error: Error window, takes message and error optionally as parameters */
        private void Error(string message, string error = "ERROR")
        {
            Form ErrorForm = new Form();
            ErrorForm.Text = error;
            ErrorForm.Width = 300;
            ErrorForm.Height = 150;
            ErrorForm.MaximizeBox = false;
            ErrorForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            Button ErrorFormOKButton = new Button();
            ErrorFormOKButton.Text = "OK";
            ErrorFormOKButton.Width = 75;
            ErrorFormOKButton.Height = 23;
            ErrorFormOKButton.Location = new Point(100, 75);
            Label ErrorFormLabel = new Label();
            ErrorFormLabel.Text = message;
            ErrorFormLabel.Location = new Point(60, 30);
            ErrorFormLabel.Width = 200;
            ErrorForm.Controls.Add(ErrorFormOKButton);
            ErrorForm.Controls.Add(ErrorFormLabel);
            ErrorFormOKButton.Click += (s, ev) =>
            {
                ErrorForm.Close();
            };
            ErrorForm.Load += (s, ev) =>
            {
                ErrorForm.Location = new Point((WX + (Width - ErrorForm.Width) / 2), (WY + (Height - ErrorForm.Height) / 2));
            };
            ErrorForm.ShowDialog();
        }
    }
}
