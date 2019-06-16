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

namespace Crypton
{
    public partial class Form1 : Form
    {
        private string encryptPass { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var inputForm = new InputForm();
            inputForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stream stream = null;
            //Update - remove parenthesis
            var selectFileDialog = new OpenFileDialog();
            if (selectFileDialog.ShowDialog() == DialogResult.OK && (stream = selectFileDialog.OpenFile()) != null)
            {
                if (string.IsNullOrWhiteSpace(encryptPass))
                {
                    using (var keyInputForm = new KeyInput())
                    {
                        this.Hide();
                        keyInputForm.ShowDialog();
                        keyInputForm.Focus();
                        encryptPass = keyInputForm.pass;
                        this.Show();
                    }
                }
                try
                {
                    string fileName = selectFileDialog.FileName;
                    const Int32 BufferSize = 128;
                    using (var fileStream = File.OpenRead(fileName))
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                    {
                        String line;
                        while ((line = streamReader.ReadLine()) != null)
                            MessageBox.Show(EncryptEngine.Decrypt(line, encryptPass));
                    }
                }
                catch
                {
                    MessageBox.Show("შეცდომა დეშიფრაციისას");
                }
                encryptPass = null;
            }
        }
    }
}
