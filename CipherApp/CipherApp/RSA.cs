using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

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

        public static long E = 65537;

        public static string encrypt(string plainText)
        {
            string cipherText = "";

            long p = GeneratePrime();
            long q = GeneratePrime();
            long totient = (p - 1) * (q - 1);

            

            return cipherText;
        }

        public static string decrypt(string cipherText)
        {
            string plainText = "";

            return plainText;
        }

        private static long GeneratePrime()
        {
            long prime = 0;
            byte[] rnd = new byte[8];

            //Random number generator safe for crypto use
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(rnd);

            prime = Convert.ToInt64(rnd);
            

            return prime;
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
