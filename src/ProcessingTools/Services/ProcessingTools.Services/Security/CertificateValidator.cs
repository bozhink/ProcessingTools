// <copyright file="CertificateValidator.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Security
{
    using System;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services.Security;

    /// <summary>
    /// Validation service with crypto certificate.
    /// </summary>
    public class CertificateValidator : ICertificateValidator
    {
        /// <inheritdoc/>
        public Encoding Encoding { get; set; } = Defaults.Encoding;

        /// <inheritdoc/>
        public X509Certificate2 GetPersonalX509CertificateBySubject(string certificateSubject)
        {
            // Access Personal (MY) certificate store of current user
            using (X509Store my = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                my.Open(OpenFlags.ReadOnly);

                // Find the certificate we'll use to sign
                foreach (X509Certificate2 certificate in my.Certificates)
                {
                    if (certificate.Subject.Contains(certificateSubject, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // We found it.
                        // Get its associated CSP (Crypto Service Provider) and private key
                        return certificate;
                    }
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public X509Certificate2 GetX509CertificateByFileName(string fileName)
        {
            // Load the certificate we'll use from a file
            X509Certificate2 certificate = new X509Certificate2(fileName);
            return certificate;
        }

        /// <inheritdoc/>
        public byte[] Sign(string text, X509Certificate2 certificate)
        {
            if (certificate is null)
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

        /// <inheritdoc/>
        public bool Verify(string text, byte[] signature, X509Certificate2 certificate)
        {
            if (certificate is null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            if (string.IsNullOrEmpty(text) || signature is null || signature.Length < 1)
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
