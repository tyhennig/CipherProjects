using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace CipherApp
{
    public partial class Form1 : Form
    {

        private string cipherText = "";
        private string plainText = "";
        public Form1()
        {
            InitializeComponent();

            lbCiphers.Items.Add("Rail-Fence");
            lbCiphers.Items.Add("DES");

        }
        


        private string encryptText(string s) //function to select correct cipher algorithm. Chance to add more ciphers in the future
        {
            if(lbCiphers.SelectedItem == null)
            {
                MessageBox.Show("Please select a cipher to use!", "Warning!");
                return null;
            }
            if(s.Length <= 0)
            {
                MessageBox.Show("Please type plain text to encrypt");
                return null;
            }

            switch (lbCiphers.SelectedItem.ToString())
            {
                case "Rail-Fence":
                    cipherText = RailFence.encrypt(s, tbKey.Text);
                    break;
                case "DES":
                    cipherText = DES.encrypt(s, tbKey.Text);
                    break;
                default:
                    break;
            }
            return cipherText;
        }

        private string decryptText(string s)
        {
            if (lbCiphers.SelectedItem == null)
            {
                MessageBox.Show("Please select a cipher to use!", "Warning!");
                return null;
            }

            switch (lbCiphers.SelectedItem.ToString())
            {
                case "Rail-Fence":
                    plainText = RailFence.decrypt(s, tbKey.Text);
                    break;
                default:
                    break;
            }
            return plainText;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void bEncrypt_Click(object sender, EventArgs e) //When encrypt button is clicked
        {
            plainText = tbInputText.Text;
            tbOutputText.Text = encryptText(plainText);
        }

        private void bDecrypt_Click(object sender, EventArgs e) //When decrypt button is clicked
        {
            cipherText = tbInputText.Text;
            tbOutputText.Text = decryptText(cipherText);
        }
    }
}
