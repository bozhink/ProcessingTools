// <copyright file="Startup.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

// See https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html
// See https://stackoverflow.com/questions/56385277/configure-autofac-in-asp-net-core-3-0-preview-5-or-higher
namespace ProcessingTools.Web.Core.Api
{
    using System;
    using System.Net.Http;
    using System.Text.Json;
    using Autofac;
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
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
        /// <param name="env">Hosting environment.</param>
        public Startup(IWebHostEnvironment env)
        {
            if (env is null)
            {
                throw new ArgumentNullException(nameof(env));
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ConfigureServices is where you register dependencies. This gets
        /// called by the runtime before the ConfigureContainer method, below.
        /// </summary>
        /// <param name="services">Collection of services.</param>
        /// <remarks>
        /// Add services to the collection. Don't build or return
        /// any IServiceProvider or the ConfigureContainer method
        /// won't get called.
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services
                .AddCors(options =>
                {
                    options.AddPolicy("AllOriginsCorsPolicy", policy =>
                    {
                        ////policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("*");
                        policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("*");
                    });

                    options.AddPolicy("StrictCorsPolicy", policy =>
                    {
                        ////policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("*");
                        policy.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(o => o.StartsWith("192.168.", StringComparison.InvariantCultureIgnoreCase));
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
        }

        /// <summary>
        /// ConfigureContainer is where you can register things directly
        /// with Autofac. This runs after ConfigureServices so the things
        /// here will override registrations made in ConfigureServices.
        /// Don't build the container; that gets done for you by the factory.
        /// </summary>
        /// <param name="builder">Instance of the <see cref="ContainerBuilder"/>.</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            // Configure AutoMapper.
            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddMaps(typeof(ProcessingTools.Configuration.AutoMapper.AssemblySetup).Assembly);
            });

            builder.RegisterInstance(mapperConfiguration.CreateMapper()).As<IMapper>().SingleInstance();

            builder.RegisterType<ImageWriterWebService>().As<IImageWriterWebService>().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Configure is where you add middleware. This is called after
        /// ConfigureContainer. You can use IApplicationBuilder.ApplicationServices
        /// here if you need to resolve things from the container.
        /// </summary>
        /// <param name="app">Application builder.</param>
        public void Configure(IApplicationBuilder app)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Configure for the Development environment.
        /// </summary>
        /// <param name="app">Application builder.</param>
        public void ConfigureDevelopment(IApplicationBuilder app)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseDeveloperExceptionPage();

            this.Configure(app);

            app.UseCors("AllOriginsCorsPolicy");
        }

        /// <summary>
        /// Configure for the Staging environment.
        /// </summary>
        /// <param name="app">Application builder.</param>
        public void ConfigureStaging(IApplicationBuilder app)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            this.Configure(app);

            app.UseCors("StrictCorsPolicy");
        }

        /// <summary>
        /// Configure for the Production environment.
        /// </summary>
        /// <param name="app">Application builder.</param>
        public void ConfigureProduction(IApplicationBuilder app)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            this.ConfigureStaging(app);
        }
    }
}
