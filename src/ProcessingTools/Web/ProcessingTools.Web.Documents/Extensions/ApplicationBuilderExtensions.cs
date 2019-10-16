// <copyright file="ApplicationBuilderExtensions.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Extensions
{
    using System.IO;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.FileProviders;
    using ProcessingTools.Web.Documents.Middleware;

    /// <summary>
    /// <see cref="IApplicationBuilder"/> extensions.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use application context factory middleware.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/> instance.</param>
        /// <returns>Updated <see cref="IApplicationBuilder"/> instance.</returns>
        public static IApplicationBuilder UseApplicationContext(this IApplicationBuilder app) => app.UseMiddleware<ApplicationContextMiddleware>();

        /// <summary>
        /// Serve static files from specified directory.
        /// </summary>
        /// <param name="app">Application builder to be configured.</param>
        /// <param name="environment">Hosting environment.</param>
        /// <param name="rootPath">Root physical directory path.</param>
        /// <param name="requestPath">Virtual path for requests.</param>
        /// <returns>Configures application builder.</returns>
        public static IApplicationBuilder ServeStaticFiles(this IApplicationBuilder app, IWebHostEnvironment environment, string rootPath, string requestPath)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(environment.ContentRootPath, rootPath)),
                RequestPath = requestPath,
            });

            return app;
        }
    }
}
