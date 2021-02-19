using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * 
 * TODO:
 * Keys less than 3 do not work.
 * Decrypt Function
 * 
 * 
 * 
 * 
 * 
 * 
 */

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

        }

        private string railFenceCipher(string s)
        {
            int key = int.Parse(tbKey.Text);

            if (key <= 1)
            {
                return s;
            }
            cipherText = "";
            bool goingDown = true;
            int railCounter = 0;
            int textLength = tbInputText.Text.Length;
            char[,] cipherArray = new char[textLength, key];
            

            for(int i = 0; i < textLength; i++)
            {
                if(Char.IsLetterOrDigit(s[i]))
                {

                    cipherArray[i, railCounter] = s[i];

                    if (goingDown)
                    {
                        //cipherArray[i, railCounter] = s[i];
                        railCounter++;
                    }
                    else
                    {
                        //cipherArray[i, (2 * (key - 1) - railCounter)] = s[i];
                        railCounter--;
                    }

                    if (railCounter == key-1)
                    {
                        goingDown = false;

                    }
                    else if (railCounter == 0)
                    {
                        goingDown = true;
                    }
                }
                
            }

            for(int i = 0; i < key; i++)
            {
                for(int j = 0; j < textLength; j++)
                {
                    if(Char.IsLetterOrDigit(cipherArray[j,i]))
                    {
                        cipherText += Char.ToLower(cipherArray[j, i]);
                    }
                }
            }

            return cipherText;
        }

        private string railFenceDecipher(string s)
        {
            int key = int.Parse(tbKey.Text);
             
            if (key <= 1)
            {
                return s;
            }
            plainText = "";
            bool goingDown = true;
            int railCounter = 0;
            int textLength = tbInputText.Text.Length;
            char[,] cipherArray = new char[textLength, key];

            for (int i = 0; i < textLength; i++)
            {
                if (Char.IsLetterOrDigit(s[i]))
                {
                    cipherArray[i, railCounter] = '*';

                    if (goingDown)
                    {
                        railCounter++;
                    }
                    else
                    {
                        railCounter--;
                    }

                    if (railCounter == key - 1)
                    {
                        goingDown = false;
                    }
                    else if (railCounter == 0)
                    {
                        goingDown = true;
                    }
                }

            }

            int letterIncrement = 0;
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < textLength; j++)
                {
                    if(cipherArray[j,i] == '*')
                    {
                        cipherArray[j, i] = s[letterIncrement++];
                    }
                }
            }
            railCounter = 0;
            goingDown = true;
            for (int i = 0; i < textLength; i++)
            {
                plainText += cipherArray[i, railCounter];

                if (goingDown)
                {
                    railCounter++;
                }
                else
                {
                    railCounter--;
                }

                if (railCounter == key - 1)
                {
                    goingDown = false;
                }
                else if (railCounter == 0)
                {
                    goingDown = true;
                }
                

            }



            return plainText;
        }


        private string encryptText(string s)
        {
            if(lbCiphers.SelectedItem == null)
            {
                MessageBox.Show("Please select a cipher to use!", "Warning!");
                return null;
            }

            switch (lbCiphers.SelectedItem.ToString())
            {
                case "Rail-Fence":
                    cipherText = railFenceCipher(s);
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
                    plainText = railFenceDecipher(s);
                    break;
                default:
                    break;
            }

            return plainText;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void bEncrypt_Click(object sender, EventArgs e)
        {
            plainText = tbInputText.Text;
            tbOutputText.Text = encryptText(plainText);
        }

        private void bDecrypt_Click(object sender, EventArgs e)
        {
            cipherText = tbInputText.Text;
            tbOutputText.Text = decryptText(cipherText);
        }
    }
}
