using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherApp
{
    public static class DES
    {

        /*STEPS:
         * 
         * Pad plaintext into blocks of 64 bits
         * Initial permutation of plain text
         * R(i) and k(i) sent to function f
         * result of f XOR with L(i)
         * 
         * Function f:
         *  Expand R(i) into 48 bits
         *      separate into sets of 4 bits
         *      add bit before set to position 0
         *      add bit right after set to position 5
         *      Example for set 1-4
         *          Previous bit (bit #32) goes to position 0 in new 6 bit set
         *          next bit (bit #5) goes to position 5 in new 6 bit set
         *      Example for set 5-8
         *          bit #4 goes to position 0
         *          bit #9 goes to position 5
         *          
         *          
         *  XOR with k(i)
         *  Split into 8 sets of 6 bits each
         *  Sets sent through S-box giving 4 bits of output
         *  
         * S-Box:
         *  6 bit blocks fed into 8 S-Boxes
         *  64 entires in a 4x16 table
         *  each entry is a 4 bit value
         *  Table row selection uses most and least significant bits of 6bit input
         *  Table column selection uses inner 4 bits of 6bit input
         *  
         * Key schedule:
         *  Initial key permutation PC-1
         *  split into C0 and D0
         *  Shift left each half
         *  Rounds 1, 2, 9, 16 rotate by 1 bit
         *  All other rounds rotate by 2 bits
         *  rotations take place within each half
         *  Permuteate again using PC-2 to turn 56 bits into 48 bits
         *  
         *  For decryption key round 16 is used first and decrements
         * 
         *  
         * 
         * Final inverse permutation
         */

        private static string text;
        private static string cipherText;
        private static int[] initialPerm = {58, 50, 42, 34, 26, 18, 10, 2,
                             60, 52, 44, 36, 28, 20, 12, 4,
                             62, 54, 46, 38, 30, 22, 14, 6,
                             64, 56, 48, 40, 32, 24, 16, 8,
                             57, 49, 41, 33, 25, 17, 9, 1,
                             59, 51, 43, 35, 27, 19, 11, 3,
                             61, 53, 45, 37, 29, 21, 13, 5,
                             63, 55, 47, 39, 31, 23, 15, 7 };

        public static string encrypt(string m, int k)
        {
            cipherText = "";
            text = m;
            string test = "H";

            m = padText(text);


            return cipherText;
        }

        private static string padText(string m)
        {
            int mod64 = (m.Length * 2) % 64;
            int charsToPad = (64 - mod64)/2;

            for(int i = 0; i < charsToPad; i++)
            {
                m += '\u0000';
            }

            return m;
        }

        private static byte[] permutation(byte[] m, int[] array, int n)
        {


            return m;
        }
        

    }
}
