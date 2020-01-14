// <copyright file="EndpointConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Models
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Server endpoint configuration.
    /// </summary>
    public class EndpointConfiguration : IEndpointConfiguration
    {
        /// <summary>
        /// Gets or sets the host of the endpoint.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port of the endpoint.
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// Gets or sets the scheme of the endpoint.
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Gets or sets the name of certificate store.
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// Gets or sets the location of the certificate store.
        /// </summary>
        public string StoreLocation { get; set; }

        /// <summary>
        /// Gets or sets the file path of the certificate file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the password of the certificate.
        /// </summary>
        public string Password { get; set; }
    }
}
