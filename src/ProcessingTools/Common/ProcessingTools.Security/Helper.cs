// <copyright file="Helper.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Security
{
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Helper class.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Calculates MD5 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <returns>Evaluated MD5 hash as string.</returns>
        public static string CalculateMD5Hash(string source, Encoding encoding)
        {
            using (var hashAlgorithm = MD5.Create())
            {
                return CalculateHash(source, encoding, hashAlgorithm);
            }
        }

        /// <summary>
        /// Calculates MD5 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated MD5 hash as string.</returns>
        public static string CalculateMD5Hash(string source) => CalculateMD5Hash(source, Encoding.UTF8);

        /// <summary>
        /// Calculates SHA1 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <returns>Evaluated MD5 hash as string.</returns>
        public static string CalculateSHA1Hash(string source, Encoding encoding)
        {
            using (var hashAlgorithm = SHA1.Create())
            {
                return CalculateHash(source, encoding, hashAlgorithm);
            }
        }

        /// <summary>
        /// Calculates SHA1 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated MD5 hash as string.</returns>
        public static string CalculateSHA1Hash(string source) => CalculateSHA1Hash(source, Encoding.UTF8);

        /// <summary>
        /// Calculates SHA256 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <returns>Evaluated MD5 hash as string.</returns>
        public static string CalculateSHA256Hash(string source, Encoding encoding)
        {
            using (var hashAlgorithm = SHA256.Create())
            {
                return CalculateHash(source, encoding, hashAlgorithm);
            }
        }

        /// <summary>
        /// Calculates SHA256 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated MD5 hash as string.</returns>
        public static string CalculateSHA256Hash(string source) => CalculateSHA256Hash(source, Encoding.UTF8);

        /// <summary>
        /// Calculates SHA512 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <returns>Evaluated MD5 hash as string.</returns>
        public static string CalculateSHA512Hash(string source, Encoding encoding)
        {
            using (var hashAlgorithm = SHA512.Create())
            {
                return CalculateHash(source, encoding, hashAlgorithm);
            }
        }

        /// <summary>
        /// Calculates SHA512 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated MD5 hash as string.</returns>
        public static string CalculateSHA512Hash(string source) => CalculateSHA512Hash(source, Encoding.UTF8);

        private static string CalculateHash(string source, Encoding encoding, HashAlgorithm hashAlgorithm)
        {
            byte[] bytes = encoding.GetBytes(source);
            byte[] hash = hashAlgorithm.ComputeHash(bytes);

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("X2"));
            }

            return stringBuilder.ToString();
        }
    }
}
