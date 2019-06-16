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
    public partial class InputForm : Form
    {
        private string encryptPass = null;
        public InputForm()
        {
            InitializeComponent();
        }
        public InputForm(string pass)
        {
            this.encryptPass = pass;
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
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
            string encryptedText;
            if (!string.IsNullOrWhiteSpace(this.encryptPass))
            {
                encryptedText = EncryptEngine.Encrypt(inputText.Text, this.encryptPass);
                //SaveToWordFile(encryptedText); 
                SaveToTxt(encryptedText);
                MessageBox.Show("ოპერაცია წარმატებულად დასრულდა");
            }
            else
            {
                MessageBox.Show("გთხოვთ შეიყვანოთ ტექსტი!");
            }

        }
        private void SaveToTxt(string text)
        {
            var appDataPath = Application.StartupPath+"/Output/";
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }
            var filePath = Path.Combine(appDataPath, Guid.NewGuid() + ".txt");
            using (var output = File.Create(filePath))
            {
            
            }
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))//$"Bank/enCrypted_{new Guid()}.txt", false, Encoding.UTF8))
            {
                writer.WriteLine(text);
            }

        }

        private void SaveToWordFile(string saveText)
        {
            try
            {
                //Create an instance for word app  
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application  
                winword.ShowAnimation = false;

                //Set status for word application is to be visible or not.  
                winword.Visible = false;

                //Create a missing variable for missing value  
                object missing = System.Reflection.Missing.Value;

                //Create a new document  
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                //adding text to document  
                document.Content.SetRange(0, 0);
                document.Content.Text = saveText;
 
                //Save the document  
                object filename = @"c:\temp1.docx";
                document.SaveAs2(ref filename);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                winword.Quit(ref missing, ref missing, ref missing);
                winword = null;
                MessageBox.Show("Document created successfully !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

