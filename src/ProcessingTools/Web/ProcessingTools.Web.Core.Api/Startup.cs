﻿// <copyright file="Startup.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Core.Api
{
    using System;
    using System.Net.Http;
    using System.Text.Json;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using ProcessingTools.Clients.Bio.Taxonomy.CatalogueOfLife;
    using ProcessingTools.Clients.Bio.Taxonomy.Gbif;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Serialization;
    using ProcessingTools.Contracts.Web.Services.Bio.Taxonomy;
    using ProcessingTools.Contracts.Web.Services.Images;
    using ProcessingTools.Services.Bio.Taxonomy;
    using ProcessingTools.Services.Serialization;
    using ProcessingTools.Web.Core.Api.Handlers;
    using ProcessingTools.Web.Services.Bio.Taxonomy;
    using ProcessingTools.Web.Services.Images;

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
                .AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy", policy =>
                    {
                        policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("*");
                    });
                })
                .AddControllers(options =>
                {
                    options.RespectBrowserAcceptHeader = true;
                    options.MaxModelValidationErrors = 50;
                })
                .AddXmlDataContractSerializerFormatters()
                .AddXmlSerializerFormatters()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.AllowTrailingCommas = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                    options.JsonSerializerOptions.WriteIndented = false;
                })
                .AddNewtonsoftJson(options =>
                {
                    options.AllowInputFormatterExceptionMessages = true;
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
                    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
                    options.UseCamelCasing(false);
                })
                .AddFormatterMappings(options =>
                {
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddScoped(typeof(ITaxonClassificationResolverApiService<>), typeof(TaxonClassificationResolverApiService<>));

            services.AddScoped<ICatalogueOfLifeTaxonClassificationResolver, CatalogueOfLifeTaxonClassificationResolver>();
            services.AddScoped<ICatalogueOfLifeWebserviceClient, CatalogueOfLifeWebserviceClient>();

            services.AddScoped<IGbifTaxonClassificationResolver, GbifTaxonClassificationResolver>();
            services.AddScoped<IGbifApiV09Client, GbifApiV09Client>();

            services.AddScoped(typeof(IXmlSerializer<>), typeof(XmlSerializer<>));
            services.AddScoped(typeof(IXmlDeserializer<>), typeof(XmlDeserializer<>));
            services.AddScoped(typeof(IXmlDeserializer), typeof(XmlDeserializer));
            services.AddScoped(typeof(IJsonDeserializer<>), typeof(NewtonsoftJsonDeserializer<>));
            services.AddScoped(typeof(IJsonDeserializer), typeof(NewtonsoftJsonDeserializer));

            services.AddTransient<LoggingHandler>();

            services.AddHttpClient<CatalogueOfLifeWebserviceClient>(nameof(CatalogueOfLifeWebserviceClient))
                .ConfigureHttpClient(c =>
                {
                    string baseAddress = this.Configuration.GetValue<string>(ConfigurationConstants.ExternalServicesCatalogueOfLifeWebserviceBaseAddress);

                    c.BaseAddress = new Uri(baseAddress);
                    c.DefaultRequestHeaders.Add("User-Agent", "PT");
                    c.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    c.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddHttpMessageHandler<LoggingHandler>()
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                    UseDefaultCredentials = true,
                    UseCookies = false,
                    UseProxy = false,
                });

            services.AddHttpClient<GbifApiV09Client>(nameof(GbifApiV09Client))
                .ConfigureHttpClient(c =>
                {
                    string baseAddress = this.Configuration.GetValue<string>(ConfigurationConstants.ExternalServicesGbifApi09BaseAddress);

                    c.BaseAddress = new Uri(baseAddress);
                    c.DefaultRequestHeaders.Add("User-Agent", "PT");
                    c.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    c.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddHttpMessageHandler<LoggingHandler>()
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                    UseDefaultCredentials = true,
                    UseCookies = false,
                    UseProxy = false,
                });

            // Configure AutoMapper.
            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddMaps(typeof(ProcessingTools.Configuration.AutoMapper.AssemblySetup).Assembly);
            });

            services.AddSingleton<IMapper>(mapperConfiguration.CreateMapper());

            var builder = new ContainerBuilder();

            // Add bindings
            builder.Populate(services);

            builder.RegisterType<ImageWriterWebService>().As<IImageWriterWebService>().InstancePerLifetimeScope();

            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors("CorsPolicy");
        }
    }
}
