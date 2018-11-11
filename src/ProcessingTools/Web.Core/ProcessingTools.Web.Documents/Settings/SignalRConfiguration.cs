// <copyright file="SignalRConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using ProcessingTools.Web.Documents.Hubs;

    /// <summary>
    /// SignalR configuration.
    /// </summary>
    public static class SignalRConfiguration
    {
        /// <summary>
        /// Configure SignalR.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        /// <returns>Configured service collection.</returns>
        public static IServiceCollection ConfigureSignalR(this IServiceCollection services)
        {
            services.AddSignalR();
            return services;
        }

        /// <summary>
        /// Configure SignalR.
        /// </summary>
        /// <param name="app">Application builder to be configured.</param>
        /// <returns>Configures application builder.</returns>
        public static IApplicationBuilder ConfigureSignalR(this IApplicationBuilder app)
        {
#pragma warning disable S1075 // URIs should not be hardcoded
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/r/chat");
            });
#pragma warning restore S1075 // URIs should not be hardcoded

            return app;
        }
    }
}
