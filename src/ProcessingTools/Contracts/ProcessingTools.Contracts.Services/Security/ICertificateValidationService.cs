// <copyright file="ICertificateValidationService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Security
{
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Certificate validation service.
    /// </summary>
    public interface ICertificateValidationService
    {
        /// <summary>
        /// Validate certificate.
        /// </summary>
        /// <param name="certificate">Certificate to be validated.</param>
        /// <returns>Value indicating whether the certificate is valid.</returns>
        bool ValidateCertificate(X509Certificate2 certificate);
    }
}
