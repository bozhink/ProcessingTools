// <copyright file="CertificateValidationService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Security
{
    using System.Security.Cryptography.X509Certificates;
    using ProcessingTools.Contracts.Services.Security;

    /// <summary>
    /// Certificate validation service.
    /// </summary>
    public class CertificateValidationService : ICertificateValidationService
    {
        /// <inheritdoc/>
        public bool ValidateCertificate(X509Certificate2 certificate)
        {
            if (certificate is null)
            {
                return false;
            }

            throw new System.NotImplementedException();
        }
    }
}
