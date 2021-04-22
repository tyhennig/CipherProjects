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
         */
        private static long D = 0;
        private static long N = 0;
        public static long E = 3;

        public static string encrypt(string plainText)
        {
            string cipherText = "";
            byte[] pBytes = ASCIIEncoding.ASCII.GetBytes(plainText);


            //long p = GeneratePrime();
            //long q = GeneratePrime();
            /*FOR DEBUGGING*/
            long p = 11;
            long q = 3;

            N = p * q;
            long totient = (p - 1) * (q - 1);
            D = ModInverse(E, totient);
            int count = pBytes.Count();
            
            for(int i = 0; i < count; i = i + 8)
            {
                byte[] temp = new byte[8];
                //Copy 8 bytes at a time unless there aren't 8 bytes, then copy however many are left
                if (i + 8 > count)
                    Array.Copy(pBytes, i, temp, 0, count % 8);
                else
                    Array.Copy(pBytes, i, temp, 0, 8);
                ulong lNum = (ulong)BitConverter.ToInt64(temp, 0);
                ulong exp = lNum;
                for (int j = 1; j < E; j++)
                {
                    exp = exp * lNum % (ulong)N;
                    
                }
                temp = BitConverter.GetBytes(exp);
                cipherText += Convert.ToString(BitConverter.ToInt64(temp, 0), 16);
                //cipherText += ASCIIEncoding.ASCII.GetString(temp);
            }
            

            return cipherText;
        }

        public static string decrypt(string cipherText)
        {
            string plainText = "";

            byte[] pBytes = ASCIIEncoding.ASCII.GetBytes(cipherText);
            int count = pBytes.Count();

            for (int i = 0; i < count; i = i + 8)
            {
                byte[] temp = new byte[8];
                if (i + 8 > count)
                    Array.Copy(pBytes, i, temp, 0, count % 8);
                else
                    Array.Copy(pBytes, i, temp, 0, 8);
                ulong lNum = (ulong)BitConverter.ToInt64(temp, 0);
                ulong exp = lNum;
                for (int j = 1; j < E; j++)
                {
                    exp = exp * lNum % (ulong)N;

                }
                temp = BitConverter.GetBytes(exp);
                plainText += ASCIIEncoding.ASCII.GetString(temp);
            }

            return plainText;
        }

        private static long GeneratePrime()
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
            
            

            return prime;
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

        private static long FindE(long n)
        {
            return n;
        }

        private static long CalculateD(long totient)
        {
            //Use extended euclidian algorithm to find D

            return totient;
        }


    }
}
