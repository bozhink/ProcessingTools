// <copyright file="Startup.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Tagger
{
    using System;
    using System.Text.Json;
    using Autofac;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Configuration.Autofac;
    using ProcessingTools.Configuration.DependencyInjection;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.IO;
    using ProcessingTools.Contracts.Services.Meta;
    using ProcessingTools.HealthChecks;
    using ProcessingTools.Web.Api.Tagger.Formatters;
    using ProcessingTools.Web.Api.Tagger.Middleware;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Application startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="environment">Hosting environment.</param>
        public Startup(IWebHostEnvironment environment)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure services.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // See https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-3.0
            services.Configure<KestrelServerOptions>(this.Configuration.GetSection(ConfigurationConstants.KestrelSectionName));

            services
                .AddHealthChecks()
                .AddCheck<VersionHealthCheck>(name: VersionHealthCheck.HealthCheckName);

            services
                .AddCors(options =>
                {
                    options.AddPolicy("AllOriginsCorsPolicy", policy =>
                    {
                        policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                    });

                    options.AddPolicy("StrictCorsPolicy", policy =>
                    {
                        policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(o => o.StartsWith("192.168.", StringComparison.InvariantCultureIgnoreCase));
                    });
                })
                .AddControllers(options =>
                {
                    options.RespectBrowserAcceptHeader = false;
                    options.ReturnHttpNotAcceptable = false;
                    options.MaxModelValidationErrors = 50;

                    ////options.InputFormatters.Add(new PlainTextInputFormatter());
                    ////options.OutputFormatters.Add(new PlainTextOutputFormatter());
                })
                .AddXmlDataContractSerializerFormatters()
                .AddXmlSerializerFormatters()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.AllowTrailingCommas = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                    options.JsonSerializerOptions.WriteIndented = false;
                })
                .AddNewtonsoftJson(options =>
                {
                    options.AllowInputFormatterExceptionMessages = true;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    options.SerializerSettings.Formatting = Formatting.None;
                    options.UseCamelCasing(processDictionaryKeys: false);
                })
                .AddFormatterMappings(options =>
                {
                })
                ;

            // Configure AutoMapper
            services.ConfigureAutoMapper();
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

            builder.RegisterType<ApplicationContextFactory>().AsSelf().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<ApplicationContextFactory>().ApplicationContext).As<IApplicationContext>().InstancePerDependency();
            builder.Register(c => c.Resolve<IApplicationContext>().UserContext).As<IUserContext>().InstancePerDependency();

            builder.RegisterType<ProcessingTools.Services.Meta.JatsArticleMetaHarvester>().As<IJatsArticleMetaHarvester>().InstancePerDependency();
            builder.RegisterType<ProcessingTools.Services.IO.XmlReadService>().As<IXmlReadService>().InstancePerDependency();

            builder.RegisterInstance(ProcessingTools.Common.Constants.Defaults.Encoding).As<System.Text.Encoding>().SingleInstance();

            builder.RegisterModule(new XmlTransformersAutofacModule { Configuration = this.Configuration });
            builder.RegisterModule<TransformersFactoriesAutofacModule>();
            builder.RegisterModule<ProcessorsAutofacModule>();
            builder.RegisterModule<ProcessingTools.Configuration.Autofac.Geo.CoordinatesAutofacModule>();
            builder.RegisterModule(new DataAutofacModule { Configuration = this.Configuration });
            builder.RegisterModule(new DataAccessAutofacModule { Configuration = this.Configuration });
            builder.RegisterModule(new ServicesAutofacModule { Configuration = this.Configuration });
            builder.RegisterModule<ServicesWebAutofacModule>();
        }

        /// <summary>
        /// Configure is where you add middleware. This is called after
        /// ConfigureContainer. You can use IApplicationBuilder.ApplicationServices
        /// here if you need to resolve things from the container.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (env is null)
            {
                throw new ArgumentNullException(nameof(env));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<ApplicationContextMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/health", HealthChecksExtensions.GetHealthCheckOptionsExcludingVersion(this.GetType().Assembly, env.IsDevelopment()));

                endpoints.MapHealthChecks("/version", HealthChecksExtensions.GetHealthCheckOptionsForVersion(this.GetType().Assembly, env.IsDevelopment()));
            });
        }

        /// <summary>
        /// Configure for the Development environment.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Hosting environment.</param>
        public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (env is null)
            {
                throw new ArgumentNullException(nameof(env));
            }

            this.Configure(app, env);

            app.UseCors("AllOriginsCorsPolicy");
        }

        /// <summary>
        /// Configure for the Staging environment.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Hosting environment.</param>
        public void ConfigureStaging(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (env is null)
            {
                throw new ArgumentNullException(nameof(env));
            }

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            this.Configure(app, env);

            app.UseCors("StrictCorsPolicy");
        }

        /// <summary>
        /// Configure for the Production environment.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Hosting environment.</param>
        public void ConfigureProduction(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (env is null)
            {
                throw new ArgumentNullException(nameof(env));
            }

            this.ConfigureStaging(app, env);
        }
    }
}
