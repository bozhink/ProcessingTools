// <copyright file="ICertificateValidator.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Security
{
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    /// <summary>
    /// Validation service with crypto certificate.
    /// </summary>
    public interface ICertificateValidator
    {
        /// <summary>
        /// Gets or sets the encoding.
        /// </summary>
        Encoding Encoding { get; set; }

        /// <summary>
        /// Gets personal <see cref="X509Certificate2"/> by certificate subject.
        /// </summary>
        /// <param name="certificateSubject">Certificate subject.</param>
        /// <returns>Instance of <see cref="X509Certificate2"/>.</returns>
        X509Certificate2 GetPersonalX509CertificateBySubject(string certificateSubject);

        /// <summary>
        /// Gets <see cref="X509Certificate2"/> by file name.
        /// </summary>
        /// <param name="fileName">File name of the certificate (.cer) file.</param>
        /// <returns>Instance of <see cref="X509Certificate2"/>.</returns>
        X509Certificate2 GetX509CertificateByFileName(string fileName);

        /// <summary>
        /// Sign text with specified certificate.
        /// </summary>
        /// <param name="text">Text to be signed.</param>
        /// <param name="certificate">Certificate to be used.</param>
        /// <returns>Sign as byte array.</returns>
        byte[] Sign(string text, X509Certificate2 certificate);

        /// <summary>
        /// Verifies signature of text with specified certificate.
        /// </summary>
        /// <param name="text">Text to be verified.</param>
        /// <param name="signature">Signature to be verified.</param>
        /// <param name="certificate">Certificate to be used.</param>
        /// <returns>Verification result.</returns>
        bool Verify(string text, byte[] signature, X509Certificate2 certificate);
    }
}
