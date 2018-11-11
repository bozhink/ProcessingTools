// <copyright file="Startup.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ProcessingTools.Web.Documents.Extensions;
    using ProcessingTools.Web.Documents.Settings;

    /// <summary>
    /// Start-up of the application.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Collection of services.</param>
        /// <returns>Service provider.</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMemoryCache()
                .ConfigureSignalR()
                .ConfigureDatabases(this.configuration)
                .ConfigureIdentity()
                .ConfigureAuthentication(this.configuration)
                .ConfigureAuthorization()
                .ConfigureMvcCore()
                .ConfigureMvc()
                .ConfigureHttps()
                .ConfigureCors();

            services.Configure<RazorViewEngineOptions>(options =>
            {
                ////options.AreaViewLocationFormats.Clear();
                ////options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
            });

            return services.BuildServiceProvider(this.configuration);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Hosting environment.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
#pragma warning disable S1075 // URIs should not be hardcoded
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithRedirects("/Error/Code/{0}");

            app.UseHttpsRedirection();
            
            app.ConfigureStaticFiles();
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.ServeStaticFiles(env, "node_modules", "/node_modules");
                app.ServeStaticFiles(env, "node_modules", "/lib");
            }

            app.ConfigureCors();
            app.UseWebSockets();
            app.ConfigureSignalR();

            app.ConfigureAuthentication();
            app.UseApplicationContext();
            app.ConfigureMvc();
#pragma warning restore S1075 // URIs should not be hardcoded
        }
    }
}
