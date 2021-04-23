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
        private static ulong D = 0;
        private static ulong N = 0;
        public static ulong E = 65537;

        public static string encrypt(string plainText)
        {
            string cipherText = "";
            string hexText = "";
            byte[] pBytes = ASCIIEncoding.ASCII.GetBytes(plainText);

            foreach(byte b in pBytes)
            {
                hexText += Convert.ToString(b, 16);
            }

            ulong totient, p, q;
            do
            {
                p = GeneratePrime();
                q = GeneratePrime();
                N = p * q;
                totient = (p - 1) * (q - 1);
            } while (gcd(E, totient) != 1);
            
            /*FOR DEBUGGING*/
            //long p = 173;
            //long q = 149;

            
            
            
            D = (ulong)ModInverse((long)E, (long)totient);
            int count = hexText.Length;
            
            for(int i = 0; i < count; i = i + 16)
            {
                //byte[] temp = new byte[8];
                //Copy 8 bytes at a time unless there aren't 8 bytes, then copy however many are left
                string tempHex = "";
                if(i + 16 < count)
                {
                    tempHex = hexText.Substring(i, 16);

                }
                else
                {
                    tempHex = hexText.Substring(i, count % 16);
                }
                
                ulong tempInt = (ulong)Convert.ToInt64(tempHex, 16);
                ulong permInt = tempInt;
                if (tempInt > N)
                    Console.WriteLine("Warning! Plaintext larger than N!");
                for (int j = 1; j < (long)E; j++)
                {
                    FastModExpo(permInt, E, N);
                    permInt = (permInt * tempInt) % (ulong)N;
                    
                }
                
                cipherText += Convert.ToString((long)permInt, 16);
                //cipherText += ASCIIEncoding.ASCII.GetString(temp);
            }
            

            return cipherText;
        }

        public static string decrypt(string cipherText)
        {
            string plainText = "";

            
            
            int count = cipherText.Length;

            for (int i = 0; i < count; i = i + 16)
            {
                //byte[] temp = new byte[8];
                //Copy 8 bytes at a time unless there aren't 8 bytes, then copy however many are left
                string tempHex = "";
                if (i + 16 < count)
                {
                    tempHex = cipherText.Substring(i, 16);

                }
                else
                {
                    tempHex = cipherText.Substring(i, count % 16);
                }

                ulong tempInt = (ulong)Convert.ToInt64(tempHex, 16);
                ulong permInt = tempInt;
                for (int j = 1; j < (long)D; j++)
                {
                    permInt = (permInt * tempInt) % (ulong)N;
                    FastModExpo(permInt, D, N);
                }

                plainText += Convert.ToString((long)permInt, 16);
                //cipherText += ASCIIEncoding.ASCII.GetString(temp);
            }

            string hexText = plainText;
            byte[] pBytes = new byte[hexText.Length / 2];
            for (int i = 0; i < hexText.Length; i = i + 2)
            {
                pBytes[i / 2] = (Convert.ToByte(hexText.Substring(i, 2), 16));
            }
            plainText = System.Text.ASCIIEncoding.ASCII.GetString(pBytes);

            return plainText;
        }

        private static long FastModExpo(ulong num, ulong exp, ulong mod)
        {
            return 0;
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
                prime = (long)BitConverter.ToInt32(rnd, 0);
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

            while (a > 1)
            {
                long q = a / m;
                (a, m) = (m, a % m);


                (x, y) = (y, x - q * y); //find x and y such that GCD = M*X + A*Y


            }
            return x < 0 ? x + m0 : x;
        }

        


    }
}
