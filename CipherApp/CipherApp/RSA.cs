using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static string encrypt(string plainText)
        {
            string cipherText = "";

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

            return prime;
        }

        private static long FindE(long n)
        {
            return n;
        }

        private static long CalculateD(long e, long totient)
        {
            return e;
        }


    }
}
