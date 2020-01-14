// <copyright file="IEndpointConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Endpoint configuration.
    /// </summary>
    public interface IEndpointConfiguration
    {
        /// <summary>
        /// Gets the host of the endpoint.
        /// </summary>
        string Host { get; }

        /// <summary>
        /// Gets the port of the endpoint.
        /// </summary>
        int? Port { get; }

        /// <summary>
        /// Gets the scheme of the endpoint.
        /// </summary>
        string Scheme { get; }

        /// <summary>
        /// Gets the name of certificate store.
        /// </summary>
        string StoreName { get; }

        /// <summary>
        /// Gets the location of the certificate store.
        /// </summary>
        string StoreLocation { get; }

        /// <summary>
        /// Gets the file path of the certificate file.
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// Gets the password of the certificate.
        /// </summary>
        string Password { get; }
    }
}
