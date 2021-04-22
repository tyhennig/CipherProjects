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
        private static long D = 0;
        private static long N = 0;
        public static long E = 65537;

        public static string encrypt(string plainText)
        {
            string cipherText = "";
            byte[] pBytes = ASCIIEncoding.ASCII.GetBytes(plainText);
            
            long p = GeneratePrime();
            long q = GeneratePrime();
            N = p * q;
            long totient = (p - 1) * (q - 1);
            D = ModInverse(E, totient);

            for(int i = 1; i >= E; i++)
            {

            }

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
            byte[] rnd = new byte[4];
            bool isPrime = false;

            do
            {
                //Random number generator safe for crypto use
                var rng = new RNGCryptoServiceProvider();
                rng.GetBytes(rnd);
                prime = Convert.ToInt64(rnd);
                isPrime = CheckPrime(prime);
                rng.Dispose();

            } while (isPrime);
            
            

            return prime;
        }

        private static bool CheckPrime(long p)
        {
            long max = (long)Math.Sqrt(p);

            if (p < 0) return false;
            if (p % 2 == 0) return false;
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
