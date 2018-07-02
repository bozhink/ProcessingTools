// <copyright file="ApplicationBuilderExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Extensions
{
    using Microsoft.AspNetCore.Builder;
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
    }
}
