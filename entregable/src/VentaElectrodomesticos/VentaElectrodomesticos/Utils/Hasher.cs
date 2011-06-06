using System.Security.Cryptography;
using System;
using System.Text;

namespace VentaElectrodomesticos.Utils
{
    class Hasher
    {
        private HashAlgorithm algorithm;

        public Hasher(HashAlgorithm algorithm)
        {
            this.algorithm = algorithm;
        }

        public String hash(String input)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }
    }

}