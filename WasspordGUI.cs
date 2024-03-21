using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Wasspord
{
	public partial class WasspordGUI : Form
	{

		public bool Autosave = Properties.Settings.Default.Autosave;
		public bool Display = Properties.Settings.Default.Display;
		public string Openfilename = "";
		public string Openfilepath = "";
		public WasspordGUI()
		{
			InitializeComponent();
			/*if (Autosave)
			{
				AutosaveCheckbox.Checked = true;
			}
			else if (!Autosave)
			{
				AutosaveCheckbox.Checked = false;
			}*/
			AutosaveCheckbox.Checked = Autosave ? true : false;
		}

		private void AddAccountButton_Click(object sender, EventArgs e)
		{
			new AddAccountGUI().ShowDialog();
			if (Autosave == true)
				Wasspord.Save(Openfilename, Openfilepath);
			OutputTextbox.Text = Wasspord.Display();
		}

		private void DeleteAccountButton_Click(object sender, EventArgs e)
		{
			new DeleteAccountGUI().ShowDialog();
			if (Autosave == true)
				Wasspord.Save(Openfilename, Openfilepath);
			OutputTextbox.Text = Wasspord.Display();
		}

		private void UpdatePasswordButton_Click(object sender, EventArgs e)
		{
			new UpdatePasswordGUI().ShowDialog();
			if (Autosave == true)
				Wasspord.Save(Openfilename, Openfilepath);
			OutputTextbox.Text = Wasspord.Display();
		}

		private void WasspordGUI_Load(object sender, EventArgs e)
		{
			OutputTextbox.Text = Wasspord.Display();
			if (Display == true)
				OutputTextbox.ForeColor = Color.FromName("White");
		}

		private void AutosaveCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			if (AutosaveCheckbox.Checked == true)
			{
				Properties.Settings.Default.Autosave = Autosave = true;
				Properties.Settings.Default.Save();
			}
			else if (AutosaveCheckbox.Checked == false)
			{
				Properties.Settings.Default.Autosave = Autosave = false;
				Properties.Settings.Default.Save();
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Openfilename == "" && Openfilepath == "")
			{
				saveAsToolStripMenuItem_Click(sender, e);
			}
			else
			{
				Wasspord.Save(Openfilepath, Openfilename);
			}
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog sf = new SaveFileDialog();
			sf.Title = "Save";
			sf.Filter = "Wasspord Text File|*.wasspord";
			sf.RestoreDirectory = true;
			if (sf.ShowDialog() == DialogResult.OK)
			{
				Openfilename = Path.GetFileName(sf.FileName);
				Openfilepath = Path.GetDirectoryName(sf.FileName);
				Wasspord.Save(Openfilepath, Openfilename);
			}
		}

		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog of = new OpenFileDialog();
			of.Title = "Open";
			of.Filter = "Wasspord Text File|*.wasspord";
			of.RestoreDirectory = true;
			if (of.ShowDialog() == DialogResult.OK)
			{
				Openfilename = Path.GetFileName(of.FileName);
				Openfilepath = Path.GetDirectoryName(of.FileName);
				Wasspord.Load(Openfilepath, Openfilename);
				OutputTextbox.Text = Wasspord.Display();
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
			AboutFormLabel.Location = new Point(100, 175);
			AboutFormLabel.Size = new Size(250, 50);
			AboutFormLabel.Text = "The Password Manager\r\nBy Tanner Ghosen\r\n2023 - 2024";
			LinkLabel AboutFormLinkLabel = new LinkLabel();
			AboutFormLinkLabel.Location = new Point(100, 225);
			AboutFormLinkLabel.Size = new Size(250, 15);
			AboutFormLinkLabel.Text = "https://www.github.com/tannerghosen/Wasspord";
			AboutFormLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(AboutFormLinkClicked);
			LinkLabel AboutFormLinkLabel2 = new LinkLabel();
			AboutFormLinkLabel2.Location = new Point(100, 240);
			AboutFormLinkLabel2.Size = new Size(250, 15);
			AboutFormLinkLabel2.Text = "Glitches or Bugs? Report them here.";
			AboutFormLinkLabel2.LinkClicked += new LinkLabelLinkClickedEventHandler(HowToFormLinkClicked);
			AboutForm.Controls.Add(AboutFormLabel);
			AboutForm.Controls.Add(AboutFormLinkLabel);
			AboutForm.Controls.Add(AboutFormLinkLabel2);
			AboutForm.ShowDialog();
		}
		private void AboutFormLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://www.github.com/tannerghosen/Wasspord");
		}

		private void DisplayButton_Click(object sender, EventArgs e)
		{
			OutputTextbox.ForeColor = OutputTextbox.ForeColor == Color.FromName("Black") ? Color.FromName("White"): Color.FromName("Black");
			Properties.Settings.Default.Display = Display = !Properties.Settings.Default.Display;
			Properties.Settings.Default.Save();
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
			HelpFormLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(HowToFormLinkClicked);
			HelpForm.Controls.Add(HelpFormLabel);
			HelpForm.Controls.Add(HelpFormLinkLabel);
			HelpForm.ShowDialog();
		}
		private void HowToFormLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
			if (Autosave == true)
			{
				Properties.Settings.Default.Autosave = Autosave = false;
				Properties.Settings.Default.Save();
				AutosaveCheckbox.Checked = false;
			}
			else if (Autosave == false)
			{
				Properties.Settings.Default.Autosave = Autosave = true;
				Properties.Settings.Default.Save();
				AutosaveCheckbox.Checked = true;
			}
		}

		private void showHideAccountsPasswordsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DisplayButton_Click(sender, e);
		}
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Wasspord.Reset();
			OutputTextbox.Text = "";
			Openfilename = "";
			Openfilepath = "";
		}

        private void generatePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form PassForm = new Form();
            PassForm.Text = "Generated Password";
            PassForm.Width = 300;
            PassForm.Height = 150;
            PassForm.MaximizeBox = false;
            PassForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            /*Button PassFormOKButton = new Button();
            PassFormOKButton.Text = "OK";
            PassFormOKButton.Width = 75;
			PassFormOKButton.Height = 23;
            PassFormOKButton.Location = new Point(100, 75);
            */
            TextBox PassFormTextBox = new TextBox();
            PassFormTextBox.Text = Wasspord.GeneratePassword();
            PassFormTextBox.Location = new Point(80, 50);
            PassFormTextBox.Height = 23;
            PassFormTextBox.Width = 120;
            Label PassFormLabel = new Label();
            PassFormLabel.Text = "Here's your generated password!";
            PassFormLabel.Location = new Point(60, 30);
            PassFormLabel.Width = 200;
            //PassForm.Controls.Add(PassFormOKButton);
            PassForm.Controls.Add(PassFormTextBox);
            PassForm.Controls.Add(PassFormLabel);
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
			};
            ValidateForm.Controls.Add(ValidateFormTextBox);
            ValidateForm.Controls.Add(ValidateFormLabel);
            ValidateForm.Controls.Add(ValidateFormOKButton);
            ValidateForm.ShowDialog();
        }
        private void ValidateFormOKButton_Click(string password)
        {
			MessageBox.Show(Wasspord.ValidatePassword(password));
        }
    }
}
