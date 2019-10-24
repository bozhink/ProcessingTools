// <copyright file="CertificatesController.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Core.Api.Controllers
{
    using System;
    using System.Net;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string certificateName)
        {
            using (var certificate = this.BuildSelfSignedServerCertificate(certificateName))
            {
                return this.File(certificate.Export(X509ContentType.Pfx, "WeNeedASaf3rPassword"), "certificate/pfx");
            }
        }

        // See https://stackoverflow.com/questions/42786986/how-to-create-a-valid-self-signed-x509certificate2-programmatically-not-loadin
        // See https://stackoverflow.com/questions/48196350/generate-and-sign-certificate-request-using-pure-net-framework
        private X509Certificate2 BuildSelfSignedServerCertificate(string certificateName)
        {
            SubjectAlternativeNameBuilder sanBuilder = new SubjectAlternativeNameBuilder();
            sanBuilder.AddIpAddress(IPAddress.Loopback);
            sanBuilder.AddIpAddress(IPAddress.IPv6Loopback);
            sanBuilder.AddDnsName("localhost");
            sanBuilder.AddDnsName(Environment.MachineName);

            X500DistinguishedName distinguishedName = new X500DistinguishedName($"CN={certificateName}");

            using (RSA parent = RSA.Create(4096))
            {
                using (RSA rsa = RSA.Create(4096))
                {
                    CertificateRequest parentRequest = new CertificateRequest(
                        "CN=Experimental Issuing Authority",
                        parent,
                        HashAlgorithmName.SHA512,
                        RSASignaturePadding.Pkcs1);

                    parentRequest.CertificateExtensions.Add(
                        new X509BasicConstraintsExtension(true, false, 0, true));

                    parentRequest.CertificateExtensions.Add(
                        new X509SubjectKeyIdentifierExtension(parentRequest.PublicKey, false));

                    using (X509Certificate2 parentCertificate = parentRequest.CreateSelfSigned(
                        DateTimeOffset.UtcNow.AddDays(-45),
                        DateTimeOffset.UtcNow.AddDays(365)))
                    {
                        var request = new CertificateRequest(
                            distinguishedName,
                            rsa,
                            HashAlgorithmName.SHA512,
                            RSASignaturePadding.Pkcs1);

                        request.CertificateExtensions.Add(
                            new X509BasicConstraintsExtension(false, false, 0, false));

                        request.CertificateExtensions.Add(
                            new X509KeyUsageExtension(X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature, false));

                        request.CertificateExtensions.Add(
                            new X509EnhancedKeyUsageExtension(
                                new OidCollection
                                {
                                    new Oid("1.3.6.1.5.5.7.3.1"),
                                },
                                true));

                        request.CertificateExtensions.Add(
                            new X509SubjectKeyIdentifierExtension(request.PublicKey, false));

                        request.CertificateExtensions.Add(sanBuilder.Build());

                        //// var certificate = request.CreateSelfSigned(new DateTimeOffset(DateTime.UtcNow.AddDays(-1)), new DateTimeOffset(DateTime.UtcNow.AddDays(3650)));
                        using (X509Certificate2 certificate = request.Create(
                            parentCertificate,
                            DateTimeOffset.UtcNow.AddDays(-1),
                            DateTimeOffset.UtcNow.AddDays(90),
                            new byte[] { 1, 2, 3, 4 }))
                        {
                            certificate.FriendlyName = certificateName;

                            byte[] data = certificate.Export(X509ContentType.Pfx, "WeNeedASaf3rPassword");
                            return new X509Certificate2(data, "WeNeedASaf3rPassword", X509KeyStorageFlags.Exportable);
                        }
                    }
                }
            }
        }
    }
}
