﻿using System;
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
        }

        private void AddAccountButton_Click(object sender, EventArgs e)
        {
            new AddAccountGUI().ShowDialog();
        }

        private void DeleteAccountButton_Click(object sender, EventArgs e)
        {
            new DeleteAccountGUI().ShowDialog();
        }

        private void UpdatePasswordButton_Click(object sender, EventArgs e)
        {
            new UpdatePasswordGUI().ShowDialog();
        }
    }
}
