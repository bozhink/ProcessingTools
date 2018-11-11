// <copyright file="CertificateValidator.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Security
{
    using System;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    /// <summary>
    /// Validation service with crypto certificate.
    /// </summary>
    public class CertificateValidator
    {
        /// <summary>
        /// Gets or sets the encoding.
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// Gets personal <see cref="X509Certificate2"/> by certificate subject.
        /// </summary>
        /// <param name="certificateSubject">Certificate subject.</param>
        /// <returns>Instance of <see cref="X509Certificate2"/>.</returns>
        public X509Certificate2 GetPersonalX509CertificateBySubject(string certificateSubject)
        {
            // Access Personal (MY) certificate store of current user
            X509Store my = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            my.Open(OpenFlags.ReadOnly);

            // Find the certificate we'll use to sign
            foreach (X509Certificate2 certificate in my.Certificates)
            {
                if (certificate.Subject.Contains(certificateSubject))
                {
                    // We found it.
                    // Get its associated CSP (Crypto Service Provider) and private key
                    return certificate;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets <see cref="X509Certificate2"/> by file name.
        /// </summary>
        /// <param name="fileName">File name of the certificate (.cer) file.</param>
        /// <returns>Instance of <see cref="X509Certificate2"/>.</returns>
        public X509Certificate2 GetX509CertificateByFileName(string fileName)
        {
            // Load the certificate we'll use from a file
            X509Certificate2 certificate = new X509Certificate2(fileName);
            return certificate;
        }

        /// <summary>
        /// Sign text with specified certificate.
        /// </summary>
        /// <param name="text">Text to be signed.</param>
        /// <param name="certificate">Certificate to be used.</param>
        /// <returns>Sign as byte array.</returns>
        public byte[] Sign(string text, X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            if (string.IsNullOrEmpty(text))
            {
                return Array.Empty<byte>();
            }

            using (var cryptoServiceProvider = (RSACryptoServiceProvider)certificate.PrivateKey)
            {
                byte[] hash = SecurityUtilities.GetHash<SHA256Managed>(text, this.Encoding);
                return cryptoServiceProvider.SignHash(hash, CryptoConfig.MapNameToOID("SHA256"));
            }
        }

        /// <summary>
        /// Verifies signature of text with specified certificate.
        /// </summary>
        /// <param name="text">Text to be verified.</param>
        /// <param name="signature">Signature to be verified.</param>
        /// <param name="certificate">Certificate to be used.</param>
        /// <returns>Verification result.</returns>
        public bool Verify(string text, byte[] signature, X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            if (string.IsNullOrEmpty(text) || signature == null || signature.Length < 1)
            {
                return false;
            }

            // Get its associated CSP (Crypto Service Provider) and public key
            using (var cryptoServiceProvider = (RSACryptoServiceProvider)certificate.PublicKey.Key)
            {
                byte[] hash = SecurityUtilities.GetHash<SHA256Managed>(text, this.Encoding);

                return cryptoServiceProvider.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA256"), signature);
            }
        }
    }
}
