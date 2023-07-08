using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

namespace Wasspord
{
    public partial class WasspordGUI : Form
    {

        public bool Autosave = Properties.Settings.Default.Autosave;
        public string Openfilename = "";
        public string Openfilepath = "";
        public WasspordGUI()
        {
            InitializeComponent();
            if (Autosave)
            {
                AutosaveCheckbox.Checked = true;
            }
            else if (!Autosave)
            {
                AutosaveCheckbox.Checked = false;
            }
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
        }

        private void AutosaveCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutosaveCheckbox.Checked == true)
            {
                Properties.Settings.Default.Autosave = Autosave = true;
                Properties.Settings.Default.Save();
            }
            else if (AutosaveCheckbox.Checked == false)
                Properties.Settings.Default.Autosave = Autosave = false;
                Properties.Settings.Default.Save();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Wasspord.Save(Openfilepath, Openfilename);
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
            System.Windows.Forms.Label AboutFormLabel = new System.Windows.Forms.Label();
            AboutFormLabel.Location = new Point(100, 175);
            AboutFormLabel.Size = new Size(250, 50);
            AboutFormLabel.Text = "The Password Manager\r\nBy Tanner Ghosen\r\n2023";
            LinkLabel AboutFormLinkLabel = new LinkLabel();
            AboutFormLinkLabel.Location = new Point(100, 225);
            AboutFormLinkLabel.Size = new Size(250, 15);
            AboutFormLinkLabel.Text = "https://www.github.com/tannerghosen/Wasspord";
            AboutFormLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(AboutFormLinkClicked);
            AboutForm.Controls.Add(AboutFormLabel);
            AboutForm.Controls.Add(AboutFormLinkLabel);
            AboutForm.ShowDialog();
        }
        private void AboutFormLinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.github.com/tannerghosen/Wasspord");
        }
    }
}
