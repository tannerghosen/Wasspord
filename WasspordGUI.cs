using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wasspord
{
    public partial class WasspordGUI : Form
    {
        public bool Autosave = false;
        public WasspordGUI()
        {
            InitializeComponent();
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            Wasspord.Save();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            Wasspord.Load();
            OutputTextbox.Text = Wasspord.Display();
        }

        private void AddAccountButton_Click(object sender, EventArgs e)
        {
            new AddAccountGUI().ShowDialog();
            if (Autosave == true)
                Wasspord.Save();
            OutputTextbox.Text = Wasspord.Display();
        }

        private void DeleteAccountButton_Click(object sender, EventArgs e)
        {
            new DeleteAccountGUI().ShowDialog();
            if (Autosave == true)
                Wasspord.Save();
            OutputTextbox.Text = Wasspord.Display();
        }

        private void UpdatePasswordButton_Click(object sender, EventArgs e)
        {
            new UpdatePasswordGUI().ShowDialog();
            if (Autosave == true)
                Wasspord.Save();
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
    }
}
