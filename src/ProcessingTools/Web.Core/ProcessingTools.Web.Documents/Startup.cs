// <copyright file="Startup.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents
{
    using System;
    using System.IO;
    using Autofac;
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
    using Newtonsoft.Json.Serialization;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Web.Documents.Data;
    using ProcessingTools.Web.Documents.Extensions;
    using ProcessingTools.Web.Documents.Formatters;
    using ProcessingTools.Web.Documents.Models;
    using ProcessingTools.Web.Documents.Settings;
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

            services.AddMvcCore()
                .AddApiExplorer()
                .AddAuthorization()
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddXmlDataContractSerializerFormatters()
                .AddXmlSerializerFormatters()
                .AddViews()
                .AddRazorViewEngine()
                .AddCacheTagHelper()
                .AddDataAnnotations()
                .AddCors();

            services.AddMvc(o => o.InputFormatters.Insert(0, new RawRequestBodyFormatter()))
                .AddJsonOptions(o => o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddXmlDataContractSerializerFormatters()
                .AddXmlSerializerFormatters();

            services.AddCors();

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

            builder.RegisterType<ProcessingTools.Harvesters.Meta.JatsArticleMetaHarvester>().As<ProcessingTools.Harvesters.Contracts.Meta.IJatsArticleMetaHarvester>().InstancePerDependency();
            builder.RegisterType<ProcessingTools.Services.IO.XmlReadService>().As<ProcessingTools.Services.Contracts.IO.IXmlReadService>().InstancePerDependency();

            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerDependency();

            builder.RegisterModule(new XmlTransformersAutofacModule
            {
                Configuration = this.configuration
            });
            builder.RegisterModule<TransformersFactoriesAutofacModule>();
            builder.RegisterModule<ProcessorsAutofacModule>();
            builder.RegisterModule<InterceptorsAutofacModule>();
            builder.RegisterModule(new DataAutofacModule
            {
                Configuration = this.configuration
            });
            builder.RegisterModule<ServicesWebAutofacModule>();
            builder.RegisterModule<ServicesAutofacModule>();

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
                app.UseExceptionHandler("/Error");
            }

            app.UseStatusCodePagesWithRedirects("/Error/Code/{0}");

            app.UseStaticFiles();
            if (env.IsDevelopment() || env.IsStaging())
            {
                this.ServeStaticFiles(app, env, "node_modules", "/node_modules");
                this.ServeStaticFiles(app, env, "node_modules", "/lib");
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
