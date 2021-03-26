﻿using System;
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
    public partial class Form1: Form
    {

        private string railFenceCipher(string s) //function to encrypt using rail-fence
        {
            int key = int.Parse(tbKey.Text); //get the key from text box

            if (key <= 1)
            {
                return s;
            }
            cipherText = "";
            bool goingDown = true;
            int railCounter = 0; //railCounter is used to keep track of which rail we are currently on (adding character to)
            int textLength = tbInputText.Text.Length;
            char[,] cipherArray = new char[textLength, key]; //create a 2D array of size textLength*key to store the zig zag placement

            //Loop that inserts each character in the string to its location in the 2D array
            for (int i = 0; i < textLength; i++)
            {
                if (Char.IsLetterOrDigit(s[i]))
                {
                    cipherArray[i, railCounter] = s[i];
                    //increment railCounter up or down depending on if we are heading up or down the rails
                    if (goingDown)
                    {
                        railCounter++;
                    }
                    else
                    {
                        //cipherArray[i, (2 * (key - 1) - railCounter)] = s[i];
                        railCounter--;
                    }
                    //check if we are on the bottom rail
                    if (railCounter == key - 1)
                    {
                        goingDown = false;
                    }
                    //check if we are on the top rail
                    else if (railCounter == 0)
                    {
                        goingDown = true;
                    }
                }

            }

            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < textLength; j++)
                {
                    //go through every space in the 2D array, if we put a letter there, add the letter to the cipher text
                    if (Char.IsLetterOrDigit(cipherArray[j, i]))
                    {
                        cipherText += Char.ToLower(cipherArray[j, i]);
                    }
                }
            }
            return cipherText;
        }


    }


}
