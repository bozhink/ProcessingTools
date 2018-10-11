// <copyright file="StaticFilesConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using System.IO;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.FileProviders;

    /// <summary>
    /// Static files configuration.
    /// </summary>
    public static class StaticFilesConfiguration
    {
        /// <summary>
        /// Configure static files.
        /// </summary>
        /// <param name="app">Application builder to be configured.</param>
        /// <returns>Configures application builder.</returns>
        public static IApplicationBuilder ConfigureStaticFiles(this IApplicationBuilder app)
        {
            app.UseStaticFiles();
            return app;
        }

        /// <summary>
        /// Serve static files from specified directory.
        /// </summary>
        /// <param name="app">Application builder to be configured.</param>
        /// <param name="environment">Hosting environment.</param>
        /// <param name="rootPath">Root physical directory path.</param>
        /// <param name="requestPath">Virtual path for requests.</param>
        /// <returns>Configures application builder.</returns>
        public static IApplicationBuilder ServeStaticFiles(this IApplicationBuilder app, IHostingEnvironment environment, string rootPath, string requestPath)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(environment.ContentRootPath, rootPath)),
                RequestPath = requestPath
            });

            return app;
        }
    }
}
