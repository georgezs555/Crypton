using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypton
{
    public partial class KeyInput : Form
    {
        public string pass;
        public KeyInput()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(passTxt.Text))
            {
                pass = passTxt.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("შეიყვანეთ პაროლი!");
            }
        }
    }
}
