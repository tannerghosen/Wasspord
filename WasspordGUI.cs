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

namespace Wasspord
{
    public partial class WasspordGUI : Form
    {
        public bool Autosave = false;
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
                Autosave = true;
            else if (AutosaveCheckbox.Checked == false)
                Autosave = false;
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
            // MessageBox is weirdly backwards, that's funny!
            System.Windows.Forms.MessageBox.Show("The Password Manager\nBy Tanner Ghosen\n2023\n\nhttps://www.github.com/tannerghosen/Wasspord","Wasspord");
        }
    }
}
