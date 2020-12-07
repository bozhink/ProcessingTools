// <copyright file="KestrelServerOptionsExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Web
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Security.Authentication;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.AspNetCore.Server.Kestrel.Https;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using ProcessingTools.Configuration.Web.Resources;

    /// <summary>
    /// Kestrel server options extensions.
    /// </summary>
    public static class KestrelServerOptionsExtensions
    {
        /// <summary>
        /// Configure Kestrel to require certificate.
        /// </summary>
        /// <param name="options">Kestrel server options.</param>
        /// <returns>Updated options.</returns>
        public static KestrelServerOptions RequireCertificate(this KestrelServerOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.ConfigureHttpsDefaults(listenOptions =>
            {
                listenOptions.CheckCertificateRevocation = true;
                listenOptions.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
                listenOptions.HandshakeTimeout = TimeSpan.FromSeconds(1);
                listenOptions.SslProtocols = SslProtocols.Tls12;
            });

            return options;
        }

        /// <summary>
        /// Configure endpoints.
        /// </summary>
        /// <param name="options">Kestrel server options.</param>
        /// <returns>Updated options.</returns>
        public static KestrelServerOptions ConfigureEndpoints(this KestrelServerOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var configuration = options.ApplicationServices.GetRequiredService<IConfiguration>();
            var environment = options.ApplicationServices.GetRequiredService<IHostEnvironment>();

            var endpoints = configuration.GetSection(ConfigurationConstants.HttpServerEndpointsSectionName)
                .GetChildren()
                .ToDictionary(section => section.Key, section =>
                {
                    var endpoint = new EndpointConfiguration();
                    section.Bind(endpoint);
                    return endpoint;
                });

            foreach (var endpoint in endpoints)
            {
                var config = endpoint.Value;

                bool isHttps = config.Scheme?.ToUpperInvariant() == ConfigurationConstants.HttpsScheme;

                var port = config.Port ?? ConfigurationConstants.DefaultPort;

                var ipAddresses = new List<IPAddress>();

                if (config.Host?.ToUpperInvariant() == "LOCALHOST")
                {
                    ipAddresses.Add(IPAddress.IPv6Loopback);
                    ipAddresses.Add(IPAddress.Loopback);
                }
                else if (IPAddress.TryParse(config.Host ?? string.Empty, out var address))
                {
                    ipAddresses.Add(address);
                }
                else
                {
                    ipAddresses.Add(IPAddress.IPv6Any);
                }

                foreach (var address in ipAddresses)
                {
                    options.Listen(address, port, listenOptions =>
                    {
                        if (isHttps)
                        {
                            var certificate = LoadCertificate(config, environment);
                            listenOptions.UseHttps(certificate);
                        }
                    });
                }
            }

            return options;
        }

        private static X509Certificate2 LoadCertificate(EndpointConfiguration config, IHostEnvironment environment)
        {
            if (config.StoreName != null && config.StoreLocation != null)
            {
                using (var store = new X509Store(config.StoreName, Enum.Parse<StoreLocation>(config.StoreLocation)))
                {
                    store.Open(OpenFlags.ReadOnly);

                    var certificate = store.Certificates.Find(
                        X509FindType.FindBySubjectName,
                        config.Host,
                        validOnly: !environment.IsDevelopment());

                    if (certificate.Count < 1)
                    {
                        throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, StringResources.CertificateNotFoundForHostFormat, config.Host));
                    }

                    return certificate[0];
                }
            }

            if (config.FilePath != null && config.Password != null)
            {
                return new X509Certificate2(config.FilePath, config.Password);
            }

            throw new InvalidOperationException(StringResources.NoValidCertificateConfiguration);
        }
    }
}
