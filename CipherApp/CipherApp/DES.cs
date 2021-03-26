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


        public static string encrypt(string m, int k)
        {
            string cipherText = "";



            return cipherText;
        }

    }
}
