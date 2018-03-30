// <copyright file="Startup.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Autofac;
    using Autofac.Core;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Web.Documents.Data;
    using ProcessingTools.Web.Documents.Extensions;
    using ProcessingTools.Web.Documents.Models;
    using ProcessingTools.Web.Models.Shared;
    using ProcessingTools.Web.Services;
    using ProcessingTools.Web.Services.Contracts;

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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(this.configuration.GetConnectionString(ConfigurationConstants.DefaultConnectionConnectionStringName)));

            services.AddIdentity<ApplicationUser, ApplicationRole>(
                options =>
                {
                    // Password settings
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequiredUniqueChars = 2;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    // Signin settings
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;

                    // User settings
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(
                options =>
                {
                    options.Cookie.Name = "921532ED76434551BB453EA4ABFC8DA8";
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.SlidingExpiration = true;
                    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                });

            services.AddMvc();

            // See https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims
            // See https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ElevatedRights", policy =>
                  policy.RequireRole("Administrator", "PowerUser", "BackupAdministrator"));
            });

            services.Configure<RazorViewEngineOptions>(options =>
            {
                ////options.AreaViewLocationFormats.Clear();
                ////options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
            });

            var builder = new ContainerBuilder();

            // Add bindings
            builder.Populate(services);

            builder.RegisterType<ApplicationContextFactory>().AsSelf().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<ApplicationContextFactory>().ApplicationContext).As<IApplicationContext>().InstancePerDependency();
            builder.Register(c => c.Resolve<IApplicationContext>().UserContext).As<IUserContext>().InstancePerDependency();

            builder
                .RegisterType<ProcessingTools.Data.Common.Mongo.MongoDatabaseProvider>()
                .As<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>()
                .Named<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)
                .WithParameter(InjectionConstants.ConnectionStringParameterName, this.configuration.GetConnectionString(ConfigurationConstants.DocumentsDatabaseMongoDBConnectionStringName))
                .WithParameter(InjectionConstants.DatabaseNameParameterName, this.configuration[ConfigurationConstants.DocumentsMongoDBDatabaseName])
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ProcessingTools.Data.Common.Mongo.MongoDatabaseProvider>()
                .As<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>()
                .Named<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>(InjectionConstants.MongoDBHistoryDatabaseBindingName)
                .WithParameter(InjectionConstants.ConnectionStringParameterName, this.configuration.GetConnectionString(ConfigurationConstants.HistoryDatabaseMongoDBConnectionStringName))
                .WithParameter(InjectionConstants.DatabaseNameParameterName, this.configuration[ConfigurationConstants.HistoryMongoDBDatabaseName])
                .InstancePerLifetimeScope();

            builder
                .Register<Func<IEnumerable<IDatabaseInitializer>>>(c => () => new IDatabaseInitializer[]
                {
                    c.Resolve<ProcessingTools.Data.Documents.Mongo.MongoDocumentsDatabaseInitializer>()
                })
                .As<Func<IEnumerable<IDatabaseInitializer>>>()
                .InstancePerDependency();

            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerDependency();

            builder
                .RegisterType<ProcessingTools.Services.History.ObjectHistoryDataService>()
                .As<ProcessingTools.Services.Contracts.History.IObjectHistoryDataService>()
                .InstancePerDependency();
            builder
                .RegisterType<ProcessingTools.Data.History.Mongo.MongoObjectHistoryDataAccessObject>()
                .As<ProcessingTools.Data.Contracts.History.IObjectHistoryDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>(InjectionConstants.MongoDBHistoryDatabaseBindingName)))
                .InstancePerDependency();

            builder
                .RegisterType<ProcessingTools.Web.Services.Documents.PublishersService>()
                .As<ProcessingTools.Web.Services.Contracts.Documents.IPublishersService>()
                .InstancePerDependency();
            builder
                .RegisterType<ProcessingTools.Web.Services.Documents.JournalsService>()
                .As<ProcessingTools.Web.Services.Contracts.Documents.IJournalsService>()
                .InstancePerDependency();
            builder
                .RegisterType<ProcessingTools.Web.Services.Documents.ArticlesService>()
                .As<ProcessingTools.Web.Services.Contracts.Documents.IArticlesService>()
                .InstancePerDependency();

            builder
                .RegisterType<ProcessingTools.Web.Services.Admin.DatabasesService>()
                .As<ProcessingTools.Web.Services.Contracts.Admin.IDatabasesService>()
                .InstancePerDependency();

            builder
                .RegisterType<ProcessingTools.Services.Documents.PublishersDataService>()
                .As<ProcessingTools.Services.Contracts.Documents.IPublishersDataService>()
                .InstancePerDependency();
            builder
                .RegisterType<ProcessingTools.Services.Documents.JournalsDataService>()
                .As<ProcessingTools.Services.Contracts.Documents.IJournalsDataService>()
                .InstancePerDependency();
            builder
                .RegisterType<ProcessingTools.Services.Documents.ArticlesDataService>()
                .As<ProcessingTools.Services.Contracts.Documents.IArticlesDataService>()
                .InstancePerDependency();

            builder
                .RegisterType<ProcessingTools.Services.Admin.DatabasesService>()
                .As<ProcessingTools.Services.Contracts.Admin.IDatabasesService>()
                .InstancePerDependency();

            builder
                .RegisterType<ProcessingTools.Data.Documents.Mongo.MongoPublishersDataAccessObject>()
                .As<ProcessingTools.Data.Contracts.Documents.IPublishersDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerDependency();
            builder
                .RegisterType<ProcessingTools.Data.Documents.Mongo.MongoJournalsDataAccessObject>()
                .As<ProcessingTools.Data.Contracts.Documents.IJournalsDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerDependency();
            builder
                .RegisterType<ProcessingTools.Data.Documents.Mongo.MongoArticlesDataAccessObject>()
                .As<ProcessingTools.Data.Contracts.Documents.IArticlesDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerDependency();

            builder
                .RegisterType<ProcessingTools.Data.Documents.Mongo.MongoDocumentsDatabaseInitializer>()
                .AsSelf()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerDependency();

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
#pragma warning disable S1075 // URIs should not be hardcoded
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            if (env.IsDevelopment() || env.IsStaging())
            {
                this.ServeStaticFiles(app, env, "node_modules/jquery/dist", "/lib/jquery/dist");
                this.ServeStaticFiles(app, env, "node_modules/jquery-validation/dist", "/lib/jquery-validation/dist");
                this.ServeStaticFiles(app, env, "node_modules/jquery-validation-unobtrusive", "/lib/jquery-validation-unobtrusive");
                this.ServeStaticFiles(app, env, "node_modules/bootstrap", "/lib/bootstrap");
                this.ServeStaticFiles(app, env, "node_modules/davidshimjs-qrcodejs", "/lib/qrcodejs");
            }

            app.UseAuthentication();
            app.UseApplicationContext();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
#pragma warning restore S1075 // URIs should not be hardcoded
        }

        private void ServeStaticFiles(IApplicationBuilder app, IHostingEnvironment env, string rootPath, string requestPath)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, rootPath)),
                RequestPath = requestPath
            });
        }
    }
}
