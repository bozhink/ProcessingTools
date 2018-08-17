// <copyright file="Startup.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Core.Api
{
    using System;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Start-up of the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Gets configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Collection of services.</param>
        /// <returns>Service provider.</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddXmlDataContractSerializerFormatters()
                .AddXmlSerializerFormatters()
                .AddDataAnnotations()
                .AddCors();

            services
                .AddMvc(o =>
                {
                    o.MaxModelValidationErrors = 50;
                })
                .AddJsonOptions(o => o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddXmlDataContractSerializerFormatters()
                .AddXmlSerializerFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors(
                options =>
                {
                    options.AddPolicy("CorsPolicy", policy =>
                    {
                        policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("*");
                    });
                });

            var builder = new ContainerBuilder();

            // Add bindings
            builder.Populate(services);

            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Hosting environment.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseCors("CorsPolicy");
        }
    }
}
