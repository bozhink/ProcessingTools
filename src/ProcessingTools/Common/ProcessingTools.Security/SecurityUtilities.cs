// <copyright file="SecurityUtilities.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Security
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    /// <summary>
    /// Utilities.
    /// </summary>
    public static class SecurityUtilities
    {
        /// <summary>
        /// Default encoding.
        /// </summary>
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;

        /// <summary>
        /// Converts byte array to Base 64 URL.
        /// </summary>
        /// <param name="input">Byte array to be encoded.</param>
        /// <returns>Base 64 URL encoded string.</returns>
        public static string ToBase64Url(byte[] input)
        {
            StringBuilder result = new StringBuilder(Convert.ToBase64String(input).TrimEnd('='));
            result.Replace('+', '-');
            result.Replace('/', '_');
            return result.ToString();
        }

        /// <summary>
        /// Converts Base 64 URL encoded string to byte array.
        /// </summary>
        /// <param name="base64ForUrlInput">Base 64 URL encoded string to be converted.</param>
        /// <returns>Converted byte array.</returns>
        public static byte[] FromBase64Url(string base64ForUrlInput)
        {
            int padChars = (base64ForUrlInput.Length % 4) == 0 ? 0 : (4 - (base64ForUrlInput.Length % 4));
            StringBuilder result = new StringBuilder(base64ForUrlInput, base64ForUrlInput.Length + padChars);
            result.Append(string.Empty.PadRight(padChars, '='));
            result.Replace('-', '+');
            result.Replace('_', '/');
            return Convert.FromBase64String(result.ToString());
        }

        /// <summary>
        /// Encodes byte array to Base64 URL.
        /// </summary>
        /// <param name="source">Source byte array to be encoded.</param>
        /// <returns>Base 64 URL encoded string.</returns>
        /// <remarks>
        /// See https://tools.ietf.org/html/draft-ietf-jose-json-web-signature-08#appendix-C
        /// </remarks>
        public static string Base64UrlEncode(byte[] source)
        {
            string s = Convert.ToBase64String(source); // Regular base64 encoder
            s = s.Split('=')[0]; // Remove any trailing '='s
            s = s.Replace('+', '-'); // 62nd char of encoding
            s = s.Replace('/', '_'); // 63rd char of encoding
            return s;
        }

        /// <summary>
        /// Decodes Base 64 URL string to byte array.
        /// </summary>
        /// <param name="source">Source string to be decoded.</param>
        /// <returns>Decoded byte array.</returns>
        /// <remarks>
        /// See https://tools.ietf.org/html/draft-ietf-jose-json-web-signature-08#appendix-C
        /// </remarks>
        public static byte[] Base64UrlDecode(string source)
        {
            string s = source;
            s = s.Replace('-', '+'); // 62nd char of encoding
            s = s.Replace('_', '/'); // 63rd char of encoding

            // Pad with trailing '='s
            switch (s.Length % 4)
            {
                case 0: break; // No pad chars in this case
                case 2: s += "=="; break; // Two pad chars
                case 3: s += "="; break; // One pad char
                default: throw new InvalidOperationException("Illegal base64url string!");
            }

            return Convert.FromBase64String(s); // Standard base64 decoder
        }

        /// <summary>
        /// Gets instance of <see cref="HashAlgorithm" /> for RSA.
        /// </summary>
        /// <param name="algorithm">Hashing algorithm in format `RS\d+`, e.g. `RS256`.</param>
        /// <returns>Instance of <see cref="HashAlgorithm" />.</returns>
        public static HashAlgorithm GetRsaHashAlgorithm(string algorithm)
        {
            if (algorithm.Length < 3 || algorithm.Substring(0, 2) != "RS")
            {
                throw new InvalidOperationException($"Hash algorithm `{algorithm}` must be valid RSA algorithm.");
            }

            switch (algorithm.Substring(2))
            {
                case "256":
                    return SHA256.Create();

                case "384":
                    return SHA384.Create();

                case "512":
                    return SHA512.Create();

                default:
                    throw new NotSupportedException($"Hash algorithm {algorithm} is not supported");
            }
        }

        /// <summary>
        /// Gets instance of <see cref="HashAlgorithm" /> for RSA.
        /// </summary>
        /// <param name="algorithm">Hashing algorithm in format `RS\d+`, e.g. `RS256`.</param>
        /// <returns>Instance of <see cref="HashAlgorithm" />.</returns>
        public static HashAlgorithm GetRsaCryptoServiceProvider(string algorithm)
        {
            if (algorithm.Length < 3 || algorithm.Substring(0, 2) != "RS")
            {
                throw new InvalidOperationException($"Hash algorithm `{algorithm}` must be valid RSA algorithm.");
            }

            switch (algorithm.Substring(2))
            {
                case "256":
                    return new SHA256CryptoServiceProvider();

                case "384":
                    return new SHA384CryptoServiceProvider();

                case "512":
                    return new SHA512CryptoServiceProvider();

                default:
                    throw new NotSupportedException($"Hash algorithm {algorithm} is not supported");
            }
        }

        /// <summary>
        /// Sign hash of source data with RSA algorithm.
        /// </summary>
        /// <param name="source">Source byte array to be signed.</param>
        /// <param name="algorithm">RSA algorithm for hashing.</param>
        /// <param name="certificate">Certificate to be used.</param>
        /// <returns>RSA signature of the source.</returns>
        public static byte[] RsaSignHash(byte[] source, string algorithm, X509Certificate2 certificate)
        {
            if (source == null || string.IsNullOrEmpty(algorithm) || certificate == null)
            {
                return Array.Empty<byte>();
            }

            HashAlgorithm hashAlgorithm = GetRsaHashAlgorithm(algorithm);
            using (hashAlgorithm)
            {
                using (var cryptoServiceProvider = (RSACryptoServiceProvider)certificate.PrivateKey)
                {
                    string oid = MapNameToOID(hashAlgorithm);
                    byte[] hash = hashAlgorithm.ComputeHash(source);
                    return cryptoServiceProvider.SignHash(hash, oid);
                }
            }
        }

        /// <summary>
        /// Verify hash of source data RSA signature.
        /// </summary>
        /// <param name="source">Signed source byte array.</param>
        /// <param name="signature">Signature as byte array.</param>
        /// <param name="algorithm">RSA algorithm for hashing.</param>
        /// <param name="certificate">Certificate to be used.</param>
        /// <returns>Verification result of the RSA signature.</returns>
        public static bool RsaVerifyHash(byte[] source, byte[] signature, string algorithm, X509Certificate2 certificate)
        {
            if (source == null || signature == null || string.IsNullOrEmpty(algorithm) || certificate == null)
            {
                return false;
            }

            HashAlgorithm hashAlgorithm = GetRsaHashAlgorithm(algorithm);
            using (hashAlgorithm)
            {
                using (var cryptoServiceProvider = (RSACryptoServiceProvider)certificate.PublicKey.Key)
                {
                    string oid = MapNameToOID(hashAlgorithm);
                    byte[] hash = hashAlgorithm.ComputeHash(source);
                    return cryptoServiceProvider.VerifyHash(hash, oid, signature);
                }
            }
        }

        /// <summary>
        /// Gets the object identifier (OID) of the algorithm corresponding to the specified simple name.
        /// </summary>
        /// <param name="hashAlgorithm">Hash algorithm for which to get the OID.</param>
        /// <returns>The OID of the specified algorithm</returns>
        public static string MapNameToOID(HashAlgorithm hashAlgorithm)
        {
            if (hashAlgorithm is SHA256)
            {
                return CryptoConfig.MapNameToOID(nameof(SHA256));
            }

            if (hashAlgorithm is SHA384)
            {
                return CryptoConfig.MapNameToOID(nameof(SHA384));
            }

            if (hashAlgorithm is SHA512)
            {
                return CryptoConfig.MapNameToOID(nameof(SHA512));
            }

            return null;
        }

        /// <summary>
        /// Gets hash of specified string source.
        /// </summary>
        /// <param name="source">String source object to be hashed.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <param name="hashAlgorithm">Hash algorithm to be applied.</param>
        /// <returns>Hash of the source.</returns>
        public static byte[] GetHash(string source, Encoding encoding, HashAlgorithm hashAlgorithm)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (hashAlgorithm == null)
            {
                throw new ArgumentNullException(nameof(hashAlgorithm));
            }

            if (string.IsNullOrEmpty(source))
            {
                return Array.Empty<byte>();
            }

            byte[] data = encoding.GetBytes(source);
            byte[] hash = hashAlgorithm.ComputeHash(data);
            return hash;
        }

        /// <summary>
        /// Gets hash of specified string source.
        /// </summary>
        /// <param name="source">String source object to be hashed.</param>
        /// <param name="hashAlgorithm">Hash algorithm to be applied.</param>
        /// <returns>Hash of the source.</returns>
        public static byte[] GetHash(string source, HashAlgorithm hashAlgorithm) => GetHash(source, DefaultEncoding, hashAlgorithm);

        /// <summary>
        /// Gets hash of specified string source.
        /// </summary>
        /// <typeparam name="T">Type of the hash algorithm.</typeparam>
        /// <param name="source">String source object to be hashed.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <returns>Hash of the source.</returns>
        public static byte[] GetHash<T>(string source, Encoding encoding)
            where T : HashAlgorithm, new()
        {
            using (HashAlgorithm hashAlgorithm = new T())
            {
                return GetHash(source, encoding, hashAlgorithm);
            }
        }

        /// <summary>
        /// Gets hash of specified string source.
        /// </summary>
        /// <typeparam name="T">Type of the hash algorithm.</typeparam>
        /// <param name="source">String source object to be hashed.</param>
        /// <returns>Hash of the source.</returns>
        public static byte[] GetHash<T>(string source)
            where T : HashAlgorithm, new()
        {
            return GetHash<T>(source, DefaultEncoding);
        }

        /// <summary>
        /// Gets hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <param name="hashAlgorithm">Hash algorithm.</param>
        /// <returns>Evaluated hash as string.</returns>
        public static string GetHashAsString(string source, Encoding encoding, HashAlgorithm hashAlgorithm)
        {
            byte[] hash = GetHash(source, encoding, hashAlgorithm);

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("X2"));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="hashAlgorithm">Hash algorithm.</param>
        /// <returns>Evaluated hash as string.</returns>
        public static string GetHashAsString(string source, HashAlgorithm hashAlgorithm) => GetHashAsString(source, DefaultEncoding, hashAlgorithm);

        /// <summary>
        /// Gets MD5 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <returns>Evaluated MD5 hash as string.</returns>
        public static string GetMD5HashAsString(string source, Encoding encoding)
        {
#pragma warning disable CA5351 // Do not use insecure cryptographic algorithm MD5.
            using (var hashAlgorithm = MD5.Create())
            {
                return GetHashAsString(source, encoding, hashAlgorithm);
            }
#pragma warning restore CA5351 // Do not use insecure cryptographic algorithm MD5.
        }

        /// <summary>
        /// Gets MD5 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated MD5 hash as string.</returns>
        public static string GetMD5HashAsString(string source) => GetMD5HashAsString(source, DefaultEncoding);

        /// <summary>
        /// Calculates SHA1 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <returns>Evaluated SHA1 hash as string.</returns>
        public static string GetSHA1HashAsString(string source, Encoding encoding)
        {
#pragma warning disable CA5350 // Do not use insecure cryptographic algorithm SHA1.
            using (var hashAlgorithm = SHA1.Create())
            {
                return GetHashAsString(source, encoding, hashAlgorithm);
            }
#pragma warning restore CA5350 // Do not use insecure cryptographic algorithm SHA1.
        }

        /// <summary>
        /// Gets SHA1 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA1 hash as string.</returns>
        public static string GetSHA1HashAsString(string source) => GetSHA1HashAsString(source, DefaultEncoding);

        /// <summary>
        /// Gets SHA256 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <returns>Evaluated SHA256 hash as string.</returns>
        public static string GetSHA256HashAsString(string source, Encoding encoding)
        {
            using (var hashAlgorithm = SHA256.Create())
            {
                return GetHashAsString(source, encoding, hashAlgorithm);
            }
        }

        /// <summary>
        /// Gets SHA256 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA256 hash as string.</returns>
        public static string GetSHA256HashAsString(string source) => GetSHA256HashAsString(source, DefaultEncoding);

        /// <summary>
        /// Gets SHA384 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <returns>Evaluated SHA384 hash as string.</returns>
        public static string GetSHA384HashAsString(string source, Encoding encoding)
        {
            using (var hashAlgorithm = SHA384.Create())
            {
                return GetHashAsString(source, encoding, hashAlgorithm);
            }
        }

        /// <summary>
        /// Gets SHA384 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA384 hash as string.</returns>
        public static string GetSHA384HashAsString(string source) => GetSHA384HashAsString(source, DefaultEncoding);

        /// <summary>
        /// Gets SHA512 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <returns>Evaluated SHA512 hash as string.</returns>
        public static string GetSHA512HashAsString(string source, Encoding encoding)
        {
            using (var hashAlgorithm = SHA512.Create())
            {
                return GetHashAsString(source, encoding, hashAlgorithm);
            }
        }

        /// <summary>
        /// Gets SHA512 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA512 hash as string.</returns>
        public static string GetSHA512HashAsString(string source) => GetSHA512HashAsString(source, DefaultEncoding);

        /// <summary>
        /// Gets hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <param name="hashAlgorithm">Hash algorithm.</param>
        /// <returns>Evaluated hash as Base 64 string.</returns>
        public static string GetHashAsBase64String(string source, Encoding encoding, HashAlgorithm hashAlgorithm)
        {
            byte[] hash = GetHash(source, encoding, hashAlgorithm);

            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Gets hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="hashAlgorithm">Hash algorithm.</param>
        /// <returns>Evaluated hash as Base 64 string.</returns>
        public static string GetHashAsBase64String(string source, HashAlgorithm hashAlgorithm) => GetHashAsBase64String(source, DefaultEncoding, hashAlgorithm);

        /// <summary>
        /// Gets MD5 hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <returns>Evaluated MD5 hash as Base 64 string.</returns>
        public static string GetMD5HashAsBase64String(string source, Encoding encoding)
        {
#pragma warning disable CA5351 // Do not use insecure cryptographic algorithm MD5.
            using (var hashAlgorithm = MD5.Create())
            {
                return GetHashAsBase64String(source, encoding, hashAlgorithm);
            }
#pragma warning restore CA5351 // Do not use insecure cryptographic algorithm MD5.
        }

        /// <summary>
        /// Gets MD5 hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated MD5 hash as Base 64 string.</returns>
        public static string GetMD5HashAsBase64String(string source) => GetMD5HashAsBase64String(source, DefaultEncoding);

        /// <summary>
        /// Calculates SHA1 hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <returns>Evaluated SHA1 hash as Base 64 string.</returns>
        public static string GetSHA1HashAsBase64String(string source, Encoding encoding)
        {
#pragma warning disable CA5350 // Do not use insecure cryptographic algorithm SHA1.
            using (var hashAlgorithm = SHA1.Create())
            {
                return GetHashAsBase64String(source, encoding, hashAlgorithm);
            }
#pragma warning restore CA5350 // Do not use insecure cryptographic algorithm SHA1.
        }

        /// <summary>
        /// Gets SHA1 hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA1 hash as Base 64 string.</returns>
        public static string GetSHA1HashAsBase64String(string source) => GetSHA1HashAsBase64String(source, DefaultEncoding);

        /// <summary>
        /// Gets SHA256 hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <returns>Evaluated SHA256 hash as Base 64 string.</returns>
        public static string GetSHA256HashAsBase64String(string source, Encoding encoding)
        {
            using (var hashAlgorithm = SHA256.Create())
            {
                return GetHashAsBase64String(source, encoding, hashAlgorithm);
            }
        }

        /// <summary>
        /// Gets SHA256 hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA256 hash as Base 64 string.</returns>
        public static string GetSHA256HashAsBase64String(string source) => GetSHA256HashAsBase64String(source, DefaultEncoding);

        /// <summary>
        /// Gets SHA384 hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <returns>Evaluated SHA384 hash as Base 64 string.</returns>
        public static string GetSHA384HashAsBase64String(string source, Encoding encoding)
        {
            using (var hashAlgorithm = SHA384.Create())
            {
                return GetHashAsBase64String(source, encoding, hashAlgorithm);
            }
        }

        /// <summary>
        /// Gets SHA384 hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA384 hash as Base 64 string.</returns>
        public static string GetSHA384HashAsBase64String(string source) => GetSHA384HashAsBase64String(source, DefaultEncoding);

        /// <summary>
        /// Gets SHA512 hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <param name="encoding">Encoding of the source.</param>
        /// <returns>Evaluated SHA512 hash as Base 64 string.</returns>
        public static string GetSHA512HashAsBase64String(string source, Encoding encoding)
        {
            using (var hashAlgorithm = SHA512.Create())
            {
                return GetHashAsBase64String(source, encoding, hashAlgorithm);
            }
        }

        /// <summary>
        /// Gets SHA512 hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA512 hash as Base 64 string.</returns>
        public static string GetSHA512HashAsBase64String(string source) => GetSHA512HashAsBase64String(source, DefaultEncoding);

        /// <summary>
        /// Loads <see cref="X509Certificate2"/> from byte array.
        /// </summary>
        /// <param name="bytes">Byte array with the certificate data.</param>
        /// <returns>Instance of <see cref="X509Certificate2"/>.</returns>
        /// <remarks>
        /// See http://paulstovell.com/blog/x509certificate2
        /// </remarks>
        public static X509Certificate2 LoadCertificateFromByteArray(byte[] bytes)
        {
            X509Certificate2 certificate;
            string file = Path.Combine(Path.GetTempPath(), $"{DateTime.UtcNow:yyyyMMddhhmmssffffff}{Guid.NewGuid()}");
            try
            {
                File.WriteAllBytes(file, bytes);
                certificate = new X509Certificate2(file);
            }
            finally
            {
                File.Delete(file);
            }

            return certificate;
        }

        /// <summary>
        /// Exports public key as *.cer - formatted file.
        /// </summary>
        /// <param name="fileName">Output file name.</param>
        /// <param name="certificate">Certificate to be exported.</param>
        /// <param name="password">Password for the certificate.</param>
        /// <remarks>
        /// See http://paulstovell.com/blog/x509certificate2
        /// </remarks>
        public static void ExportCerFile(string fileName, X509Certificate2 certificate, string password)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            File.WriteAllBytes(fileName, certificate.Export(X509ContentType.Cert, password));
        }

        /// <summary>
        /// Exports public as private keys as *.pfx - formatted file.
        /// </summary>
        /// <param name="fileName">Output file name.</param>
        /// <param name="certificate">Certificate to be exported.</param>
        /// <param name="password">Password for the certificate.</param>
        /// <remarks>
        /// See http://paulstovell.com/blog/x509certificate2
        /// </remarks>
        public static void ExportPfxFile(string fileName, X509Certificate2 certificate, string password)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            File.WriteAllBytes(fileName, certificate.Export(X509ContentType.Pkcs12, password));
        }
    }
}
