using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Numerics;

namespace CipherApp
{

    public static class RSA
    {
        /*STEPS
         * 
         *  SETUP
         *      Generate two large primes, p and q
         *      n = pq;
         *      Pick e such that GCD(e, totient(n)) = 1
         *      Calculate d = e^-1 mod(totient(n));
         *      Get rid of p, q, totient(n)
         *      Public key is (e, n). Private key d
         *
         *  Encryption
         *      Ciphertext = Plaintext^e mod(n)
         *      
         *  Decryption
         *      Plaintext = Ciphertext^d mod(n)
         *      
         *      TODO:
         *          Right now, if the number representation of the plaintext is > N,
         *          You cannot get the plaintext back after encrypting. This is because hex value is modded N so it can't be as large it needs to be
         *          Solution:
         *              Check the size of (long) plainText, if it is > N, divide it into smaller pieces
         *      
         *      
         */

        //Constants
        private static ulong D = 0;
        private static ulong N = 0;
        private static int SUBSTR_SIZE = 16;
        public static ulong E = 65537;


        //encrypt function
        public static string encrypt(string plainText)
        {
            string cipherText = "";
            string hexText = "";
            byte[] pBytes = ASCIIEncoding.ASCII.GetBytes(plainText);

            //convert plaintext characters into hexadecimal data
            foreach(byte b in pBytes)
            {
                hexText += Convert.ToString(b, 16);
            }

            ulong totient, p, q;

            //generate a set of primes until the totient is coprime with E
            //it is common to do this instead of find E for a given totient.
            do
            {
                p = GeneratePrime();
                q = GeneratePrime();
                N = p * q;
                totient = (p - 1) * (q - 1);
            } while (gcd(E, totient) != 1);
            
            
            
            //the private key, D, is found by MOD inverse of E
            D = (ulong)ModInverse((long)E, (long)totient);

            //RSACalculation almost the same for both encrypt and decrypt, so we supply a bool parameter
            //to flag minor changes in the algorithm
            cipherText = RSACalculation(true, hexText);

            //Show the generated keys using message box
            System.Windows.Forms.MessageBox.Show("Key Generated!\n\nPublic: (E, N) = (" + E + ", " + N + ")\nPrivate: (D, N) = (" + D + ", " + N + ")");

            return cipherText;
        }

        public static string decrypt(string cipherText)
        {
            string plainText = "";

            plainText = RSACalculation(false, cipherText);
            
            string hexText = plainText;
            byte[] pBytes = new byte[hexText.Length / 2];

            //convert hex data into ASCII characters
            for (int i = 0; i < hexText.Length; i = i + 2)
            {
                pBytes[i / 2] = (Convert.ToByte(hexText.Substring(i, 2), 16));
            }
            plainText = System.Text.ASCIIEncoding.ASCII.GetString(pBytes);

            return plainText;
        }

        private static ulong FastModExpo(ulong num, ulong exp, ulong mod)
        {
            ulong result = 1;
            //loop through each binary digit of exponent
            while(exp > 0)
            {
                /*
                 * This works by splitting num^exp into the binary representation
                 * EX. 5^5 = 5^(1+4) = 5^1*5^4
                 * 
                 * For each bit in the exponent, we calculate the num^x portion
                 * but we only multiply it in if the bit is 1
                 * 
                 * EX. 5^117
                 *  117 = 1110101 = (2^6 + 2^5 + 2^4 + 2^2 + 2^0) = (64 + 32 + 16 + 4 + 1)
                 *  Thus, 5^117 = 5^(64 + 32 + 16 + 4 + 1) = (5^64 * 5^32 * 5^16 * 5^4 * 5^1)
                 *  
                 */
                if ((exp & 1) == 1)
                {
                    /*
                     * First iteration:
                     *  bit is 1 and num is 5^1 so we multiply num into result
                     * Second Iteration:
                     *  bit is 0 and num is 5^2 so we don't multiply it in
                     * Third:
                     *  bit is 1 and num is 5^4 so we multiply it in
                     * Etc..
                     */
                    result = (result * num) % mod;
                }
                num = (num * num) % mod;
                exp = exp >> 1;
            }

            return result;
        }

        private static string RSACalculation(bool isEncrypting, string hexText)
        {
            string returnText = "";
            int count = hexText.Length;
            //We encrypt/decrypt SUBSTR_SIZE (8 or whatever you want) hex characters at a time
            for (int i = 0; i < count; i = i + SUBSTR_SIZE)
            {
                
                string tempHex = "";
                //Split hex into smaller sub strings
                if (i + SUBSTR_SIZE <= count)
                {
                    tempHex = hexText.Substring(i, SUBSTR_SIZE);
                }
                else
                {
                    tempHex = hexText.Substring(i, count % SUBSTR_SIZE);
                }
                
                //convert to ulong and perform modular exponentiation
                ulong tempInt = (ulong)Convert.ToInt64(tempHex, 16);
                ulong permInt = tempInt;
                //If the text is larger than N, we will not be able to decrypt
                if (tempInt > N)
                    Console.WriteLine("Warning! Plaintext larger than N!");

                permInt = FastModExpo(permInt, (isEncrypting ? D : E), N);
                
                tempHex = Convert.ToString((long)permInt, 16);
                
                //Pad left so we retain the same number of hex characters even if cipher number is smaller
                if(isEncrypting)
                    tempHex = tempHex.PadLeft(SUBSTR_SIZE, '0');
                returnText += tempHex;
            }
            return returnText;
        }
        private static ulong gcd(ulong a, ulong b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        private static ulong GeneratePrime()
        {
            long prime = 0;
            byte[] rnd = new byte[4];
            bool isPrime = false;

            do
            {
                //Random number generator safe for crypto use
                var rng = new RNGCryptoServiceProvider();
                rng.GetBytes(rnd);
                prime = (long)BitConverter.ToInt16(rnd, 0);
                isPrime = CheckPrime(prime);
                rng.Dispose();

            } while (!isPrime);
            
            

            return (ulong)prime;
        }

        private static bool CheckPrime(long p)
        {
            long max = (long)Math.Sqrt(p);

            if (p <= 0)
            {
                return false;
            }
            else if (p % 2 == 0)
            {
                return false;
            }
            for(int i = 3; i <= max; i = i + 2) //check odd numbers for 
            {
                if (p % i == 0) return false;
            }

            return true;
        }

        public static long ModInverse(long a, long m)
        {
            
            long m0 = m;
            (long x, long y) = (1, 0);

            //Extended Euclidian algorithm to find mod inverse
            while (a > 1)
            {
                
                long q = a / m;

                (a, m) = (m, a % m);

                
                (x, y) = (y, x - q * y); //find x and y such that GCD = M*X + A*Y


            }
            //if x is negative return x + mod to make positive
            return x < 0 ? x + m0 : x;
        }

        


    }
}
