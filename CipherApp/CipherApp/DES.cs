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

        private static int setsOf64;
        private static string text;
        private static string key;
        private static byte[] textBytes;
        private static byte[] keyBytes;
        private static List<byte[]> generatedKeys = new List<byte[]>(); 
        private static string binaryTextString;
        private static string binaryKeyString;
        private static string cipherText;
        private static readonly int[] ROTATE1 = { 0, 1, 8, 15 };
        private static ASCIIEncoding ascii = new ASCIIEncoding();
        private static readonly int[] initialPerm = 
            {58, 50, 42, 34, 26, 18, 10, 2,
             60, 52, 44, 36, 28, 20, 12, 4,
             62, 54, 46, 38, 30, 22, 14, 6,
             64, 56, 48, 40, 32, 24, 16, 8,
             57, 49, 41, 33, 25, 17, 9, 1,
             59, 51, 43, 35, 27, 19, 11, 3,
             61, 53, 45, 37, 29, 21, 13, 5,
             63, 55, 47, 39, 31, 23, 15, 7 };

        private static readonly int[] finalPerm =
        {
            40,  8,   48,  16,  56,  24,  64,  32,
            39,  7,   47,  15,  55,  23,  63,  31,
            38,  6,   46,  14,  54,  22,  62,  30,
            37,  5,   45,  13,  53,  21,  61,  29,
            36,  4,   44,  12,  52,  20,  60,  28,
            35,  3,   43,  11,  51,  19,  59,  27,
            34,  2,   42,  10,  50,  18,  58,  26,
            33,  1,   41,  9 ,  49,  17,  57,  25

        };

        private static readonly int[] expansionPerm =
        {
            32,  1 ,  2  , 3  , 4  , 5,
            4,   5,   6 ,  7 ,  8 ,  9,
            8,   9,   10,  11,  12,  13,
            12,  13,  14 , 15 , 16 , 17,
            16,  17,  18 , 19 , 20 , 21,
            20,  21,  22 , 23 , 24 , 25,
            24,  25,  26 , 27 , 28 , 29,
            28,  29,  30 , 31 , 32 , 1
        };

        private static readonly int[] keyPerm =
        {
            16,  7 ,  20,  21,  29,  12,  28,  17,
            1 ,  15,  23,  26,  5 ,  18,  31,  10,
            2 ,  8 ,  24,  14,  32,  27,  3 ,  9
,            19,  13,  30,  6 ,  22,  11,  4 ,  25
        };

        private static readonly int[] PC1Key =
        {
            57,  49,  41,  33,  25,  17,  9,   1,
            58,  50,  42,  34,  26,  18,  10,  2,
            59,  51,  43,  35,  27,  19,  11,  3,
            60,  52,  44,  36,  63,  55,  47,  39,
            31,  23,  15,  7,   62,  54,  46,  38,
            30,  22,  14,  6,   61,  53,  45,  37,
            29,  21,  13,  5,   28,  20,  12,  4
        };

        private static readonly int[] PC2Key =
        {
            14,  17,  11,  24,  01,  05,  03,  28,
            15,  06,  21,  10,  23,  19,  12,  04,
            26,  08,  16,  07,  27,  20,  13,  02,
            41,  52,  31,  37,  47,  55,  30,  40,
            51,  45,  33,  48,  44,  49,  39,  56,
            34,  53,  46,  42,  50,  36,  29,  32
        };

        private static readonly int[,,] sBox = {
                        {
                            {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
                            { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
                            { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
                            {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13} 
                        },
                        
                        { 
                            {15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10},
                          {3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5},
                          {0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15},
                          {13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9} 
                        },

                        { 
                            {10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8},
                            {13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1},
                            {13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7},
                            {1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12} 
                        },

                        { 
                            {7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15},
                            { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9},
                            {10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4},
                            {3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14} 
                        },

                        {   
                            {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9},
                            {14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6},
                            {4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14},
                            {11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 } 
                        },

                        { 
                            {12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11},
                            {10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8},
                            {9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6},
                            { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13} 
                        },

                        { 
                            {4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1},
                            {13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6},
                            {1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2},
                            {6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12} 
                        },

                        {   
                            {13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7},
                            { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2},
                            { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8},
                            { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 } 
                        } 
                };

        public static string encrypt(string m, string k)
        {
            cipherText = "";
            text = m;
            key = k; 

            textBytes = ascii.GetBytes(text);
            textBytes = padText(textBytes);
            keyBytes = ascii.GetBytes(key);

            binaryKeyString = byteToString(keyBytes);
            binaryTextString = byteToString(textBytes);

            //binaryTextString = "0000000100100011010001010110011110001001101010111100110111101111"; //used for testing
            //textBytes = stringToByte(binaryTextString);
            //binaryKeyString = "0001001100110100010101110111100110011011101111001101111111110001";
            keyBytes = stringToByte(binaryKeyString);

            generateKeys(keyBytes);
            
            setsOf64 = (textBytes.Length / 8);
           
            for (int set = 0; set < setsOf64; set++)
            {
                byte[] roundKey = stringToByte(binaryKeyString);
                binaryTextString = permutation(binaryTextString, initialPerm, 64);
                //textBytes = stringToByte(binaryTextString);

                byte[] setTextBytes = new byte[8];
                Array.Copy(textBytes, set * 8, setTextBytes, 0, 8); //copy subset of bytes into this sets array

                byte[] right = new byte[4]; //split 8 byte array into left and right sides
                byte[] left = new byte[4];

                Array.Copy(setTextBytes, 0, left, 0, 4);
                Array.Copy(setTextBytes, 4, right, 0, 4);

                for (int round = 0; round < 16; round++) //for 16 rounds of DES
                {
                    byte[] newRight;
                    string binaryRoundKey = "";


                    roundKey = generatedKeys.ElementAt(round);

                    
                    binaryRoundKey = byteToString(roundKey);
                    
                    newRight = f(right, roundKey);
                    for(int i = 0; i < left.Length; i++)
                    {
                        newRight[i] ^= left[i];
                    }

                    left = right;
                    right = newRight;
                }

                binaryTextString = byteToString(right);
                binaryTextString += byteToString(left);

                binaryTextString = permutation(binaryTextString, finalPerm, 64);
                //string temp = new string(ascii.GetChars(stringToByte(binaryTextString)));
                //cipherText += temp;
                cipherText += Convert.ToString(Convert.ToInt64(binaryTextString, 2), 16);
                

            }

            //cipherText = permutation(binaryTextString, finalPerm, 64);

            //cipherText = Convert.ToString(Convert.ToInt64(cipherText, 2), 16);
            return cipherText;
        }

        public static string decrypt(string m, string k)
        {
            cipherText = "";
            text = m;
            key = k;

            //textBytes = new byte[text.Length / 2];

            for(int i = 0; i < text.Length; i = i + 2)
            {
                textBytes[i/2] = (Convert.ToByte(text.Substring(i, 2), 16));
            }
            
            

            //textBytes = padText(textBytes);
            keyBytes = ascii.GetBytes(key);

            binaryKeyString = byteToString(keyBytes);
            binaryTextString = byteToString(textBytes);

            //binaryTextString = "0000000100100011010001010110011110001001101010111100110111101111"; //used for testing
            //textBytes = stringToByte(binaryTextString);
            //binaryKeyString = "0001001100110100010101110111100110011011101111001101111111110001";
            keyBytes = stringToByte(binaryKeyString);

            generateKeys(keyBytes);

            setsOf64 = (textBytes.Length / 8);

            for (int set = 0; set < setsOf64; set++)
            {
                byte[] roundKey = stringToByte(binaryKeyString);
                binaryTextString = permutation(binaryTextString, initialPerm, 64);
                //textBytes = stringToByte(binaryTextString);

                byte[] setTextBytes = new byte[8];
                Array.Copy(textBytes, set * 8, setTextBytes, 0, 8); //copy subset of bytes into this sets array

                byte[] right = new byte[4]; //split 8 byte array into left and right sides
                byte[] left = new byte[4];

                Array.Copy(setTextBytes, 0, left, 0, 4);
                Array.Copy(setTextBytes, 4, right, 0, 4);

                for (int round = 0; round < 16; round++) //for 16 rounds of DES
                {
                    byte[] newRight;
                    string binaryRoundKey = "";


                    roundKey = generatedKeys.ElementAt(15 - round);


                    binaryRoundKey = byteToString(roundKey);

                    newRight = f(right, roundKey);
                    for (int i = 0; i < left.Length; i++)
                    {
                        newRight[i] ^= left[i];
                    }

                    left = right;
                    right = newRight;
                }

                binaryTextString = byteToString(right);
                binaryTextString += byteToString(left);

                binaryTextString = permutation(binaryTextString, finalPerm, 64);
                string temp = new string(ascii.GetChars(stringToByte(binaryTextString)));
                cipherText += temp;
                //cipherText += Convert.ToString(Convert.ToInt64(binaryTextString, 2), 16);


            }

            //cipherText = permutation(binaryTextString, finalPerm, 64);

            //cipherText = Convert.ToString(Convert.ToInt64(cipherText, 2), 16);
            return cipherText;
        }


        private static void generateKeys(byte[] k)
        {
            
            binaryKeyString = byteToString(k);
            binaryKeyString = permutation(binaryKeyString, PC1Key, 56);

            for(int i = 0; i < 16; i++)
            {

                string newKey;
                //string binaryKeyString = byteToString(currentKey);
                string C = binaryKeyString.Substring(0, 28);
                string D = binaryKeyString.Substring(28, 28);

                C = rotateLeft(C, (ROTATE1.Contains(i) ? 1 : 2));
                D = rotateLeft(D, (ROTATE1.Contains(i) ? 1 : 2));
                binaryKeyString = C + D;
                newKey = permutation(binaryKeyString, PC2Key, 48);
                generatedKeys.Add(stringToByte(newKey));
                
                //newKey = stringToByte(binaryKeyString);

            }
            
        }

        private static byte[] getNewRoundKey(byte[] currentKey, int round)
        {
            
            byte[] newKey;
            string binaryKey = byteToString(currentKey);
            string C = binaryKey.Substring(0, 28);
            string D = binaryKey.Substring(28, 28);

            C = rotateLeft(C, (ROTATE1.Contains(round) ? 1 : 2));
            D = rotateLeft(D, (ROTATE1.Contains(round) ? 1 : 2));
            binaryKey = C + D;
            binaryKey = permutation(binaryKey, PC2Key, 48);
            newKey = stringToByte(binaryKey);

            return newKey;
        }

        private static byte[] f(byte[] right, byte[] key)
        {
            
            string e = permutation(byteToString(right), expansionPerm, 48);
            byte[] expByte = stringToByte(e);
            for (int i = 0; i < key.Length; i++)
            {
                expByte[i] ^= key[i];
            }

            e = byteToString(expByte);

            string sBoxResult = "";
            for(int i = 0; i < 8; i++)
            {
                string eSub = e.Substring(i * 6, 6);
                string rowS = "";
                rowS = rowS + eSub[0] + eSub[5];
                string columnS = "";
                columnS = eSub.Substring(1, 4);

                int row = Convert.ToInt32(rowS, 2);
                int column = Convert.ToInt32(columnS, 2);

                sBoxResult += (Convert.ToString(sBox[i, row, column], 2).PadLeft(4,'0'));

            }

            sBoxResult = permutation(sBoxResult, keyPerm, 32);

            return stringToByte(sBoxResult);
        }

        private static string rotateLeft(string s, int r)
        {
            string newString = "";
            for(int i = 0; i < s.Length; i++)
            {
                newString += s[(i + r) % s.Length];
            }

            return newString;
        }

        private static byte[] padText(byte[] b)
        {
            int mod64 = (b.Length) % 8; //how many characters over a multiple of 64
            int charsToPad = (8 - mod64);
            byte[] newArray = new byte[b.Length + charsToPad];
            Array.Copy(b, newArray, b.Length);

            for(int i = b.Length; i < charsToPad; i++)
            {
                newArray[i] = Byte.MinValue; //add null character to end of string
            }

            return newArray;
        }

       

        private static string byteToString(byte[] bArray)
        {
            string m = "";

            foreach(byte b in bArray)
            {
                m += Convert.ToString(b, 2).PadLeft(8, '0');
            }

            return m;
        }

        private static byte[] stringToByte(string s)
        {
            byte[] b = new byte[s.Length/8];
            string singleByte = "";

            for(int i = 0; i < b.Length; i++)
            {
                singleByte = s.Substring(i*8, 8);

                b[i] = Convert.ToByte(singleByte, 2);
            }

            

            return b;
        }

        private static string dropEndBits(string s, int newSize)
        {
            string key56 = "";
            for(int i = 0; i < newSize; i++)
            {

                //if((i+1) % 8 != 0)
                //{
                    key56 += s[i];
                //}
            }

            return key56;
        }

        private static string permutation(string m, int[] array, int n)
        {
            //char[] newPermutation = m.ToCharArray();
            StringBuilder sb = new StringBuilder(n);
            
            

            for(int i = 0; i < n; i++)
            {
                sb.Append(m[array[i] - 1]);
            }

            return sb.ToString();
        }
        

    }
}
