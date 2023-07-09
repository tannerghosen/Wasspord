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
    public partial class DeleteAccountGUI : Form
    {
        public DeleteAccountGUI()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Wasspord.DeleteAccount(LocationTextbox.Text, UsernameTextbox.Text);
            this.Close();
        }
    }
}
