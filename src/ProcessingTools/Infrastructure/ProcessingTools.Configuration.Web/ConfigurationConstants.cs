// <copyright file="ConfigurationConstants.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Web
{
    /// <summary>
    /// Configuration constants.
    /// </summary>
    public static class ConfigurationConstants
    {
        /// <summary>
        /// Logging section name.
        /// </summary>
        public const string LoggingSectionName = "Logging";

        /// <summary>
        /// Kestrel section name.
        /// </summary>
        public const string KestrelSectionName = "Kestrel";

        /// <summary>
        /// HttpServer:Endpoints section name.
        /// </summary>
        public const string HttpServerEndpointsSectionName = "HttpServer:Endpoints";

        /// <summary>
        /// HTTPS scheme.
        /// </summary>
        public const string HttpsScheme = "HTTPS";

        /// <summary>
        /// Host name of the localhost.
        /// </summary>
        public const string Localhost = "LOCALHOST";

        /// <summary>
        /// Default port for listening of the server.
        /// </summary>
        public const int DefaultPort = 5000;
    }
}
