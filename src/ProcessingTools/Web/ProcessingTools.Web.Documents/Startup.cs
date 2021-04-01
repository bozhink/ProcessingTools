// <copyright file="Startup.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents
{
    using System;
    using System.Text.Json;
    using Autofac;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Configuration.Autofac;
    using ProcessingTools.Configuration.DependencyInjection;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.IO;
    using ProcessingTools.Contracts.Services.Meta;
    using ProcessingTools.Contracts.Web.Services;
    using ProcessingTools.HealthChecks;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Documents.Controllers;
    using ProcessingTools.Web.Documents.Data;
    using ProcessingTools.Web.Documents.Extensions;
    using ProcessingTools.Web.Documents.Formatters;
    using ProcessingTools.Web.Documents.Hubs;
    using ProcessingTools.Web.Documents.Models;
    using ProcessingTools.Web.Models.Shared;
    using ProcessingTools.Web.Services;
    using RabbitMQ.Client;

    /// <summary>
    /// Start-up of the application.
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

            services.AddMemoryCache();
            services
                .AddHealthChecks()
                .AddCheck<VersionHealthCheck>(name: VersionHealthCheck.HealthCheckName)
                .AddDbContextCheck<ApplicationDbContext>();

            // Configure databases
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                string usersDatabaseType = (this.Configuration.GetValue<string>(ConfigurationConstants.UsersDatabaseType) ?? string.Empty).ToUpperInvariant();

                switch (usersDatabaseType)
                {
                    case "MSSQL":
                        options.UseSqlServer(this.Configuration.GetConnectionString(ConfigurationConstants.UsersDatabaseMSSQLConnectionStringName));
                        break;

                    default:
                        options.UseSqlite(this.Configuration.GetConnectionString(ConfigurationConstants.UsersDatabaseSQLiteConnectionStringName));
                        break;
                }
            });

            // Configure identity
            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
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

                    // Sign-in settings
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;

                    // User settings
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure authentication
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.Secure = CookieSecurePolicy.Always;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "921532ED76434551BB453EA4ABFC8DA8";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(10);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            var authenticationBuilder = services.AddAuthentication();

            if (!string.IsNullOrWhiteSpace(this.Configuration[ConfigurationConstants.AuthenticationFacebookAppId]) && !string.IsNullOrWhiteSpace(this.Configuration[ConfigurationConstants.AuthenticationFacebookAppSecret]))
            {
                authenticationBuilder.AddFacebook(options =>
                {
                    options.AppId = this.Configuration[ConfigurationConstants.AuthenticationFacebookAppId];
                    options.AppSecret = this.Configuration[ConfigurationConstants.AuthenticationFacebookAppSecret];
                });
            }

            if (!string.IsNullOrWhiteSpace(this.Configuration[ConfigurationConstants.AuthenticationTwitterConsumerKey]) && !string.IsNullOrWhiteSpace(this.Configuration[ConfigurationConstants.AuthenticationTwitterConsumerSecret]))
            {
                authenticationBuilder.AddTwitter(options =>
                {
                    options.ConsumerKey = this.Configuration[ConfigurationConstants.AuthenticationTwitterConsumerKey];
                    options.ConsumerSecret = this.Configuration[ConfigurationConstants.AuthenticationTwitterConsumerSecret];
                });
            }

            if (!string.IsNullOrWhiteSpace(this.Configuration[ConfigurationConstants.AuthenticationGoogleClientId]) && !string.IsNullOrWhiteSpace(this.Configuration[ConfigurationConstants.AuthenticationGoogleClientSecret]))
            {
                authenticationBuilder.AddGoogle(options =>
                {
                    options.ClientId = this.Configuration[ConfigurationConstants.AuthenticationGoogleClientId];
                    options.ClientSecret = this.Configuration[ConfigurationConstants.AuthenticationGoogleClientSecret];
                });
            }

            if (!string.IsNullOrWhiteSpace(this.Configuration[ConfigurationConstants.AuthenticationMicrosoftApplicationId]) && !string.IsNullOrWhiteSpace(this.Configuration[ConfigurationConstants.AuthenticationMicrosoftPassword]))
            {
                authenticationBuilder.AddMicrosoftAccount(options =>
                {
                    options.ClientId = this.Configuration[ConfigurationConstants.AuthenticationMicrosoftApplicationId];
                    options.ClientSecret = this.Configuration[ConfigurationConstants.AuthenticationMicrosoftPassword];
                });
            }

            // Configure authorization.
            // See https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims.
            // See https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles.
            // See https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-3.0&tabs=visual-studio#opt-in-to-runtime-compilation.
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ElevatedRights", policy =>
                {
                    policy.RequireRole("Administrator", "PowerUser", "BackupAdministrator");
                });

                options.AddPolicy("EmployeeOnly", policy =>
                {
                    policy.RequireClaim("EmployeeNumber");
                });

                options.AddPolicy("Founders", policy =>
                {
                    policy.RequireClaim("EmployeeNumber", "1", "2", "3", "4", "5");
                });

                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });

            // Configure SignalR
            services
                .AddSignalR(options =>
                {
                    options.MaximumReceiveMessageSize = 4096;
                })
                .AddNewtonsoftJsonProtocol(options =>
                {
                    options.PayloadSerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.PayloadSerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    options.PayloadSerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
                });

            // Configure MVC
            services
                .AddControllersWithViews(options =>
                {
                    options.RespectBrowserAcceptHeader = true;
                    options.MaxModelValidationErrors = 50;
                    options.InputFormatters.Insert(0, new VcardInputFormatter());
                    options.InputFormatters.Insert(1, new RawRequestBodyFormatter());
                    options.OutputFormatters.Insert(0, new VcardOutputFormatter());
                })
                .AddRazorRuntimeCompilation()
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
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddRazorPages();

            ////// Configure HTTPS
            ////// See https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.1&tabs=visual-studio
            //// services.AddHsts(options =>
            //// {
            ////     options.Preload = true;
            ////     options.IncludeSubDomains = true;
            ////     options.MaxAge = TimeSpan.FromDays(30);
            //// });

            ////services.AddHttpsRedirection(options =>
            ////{
            ////    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            ////    ////options.HttpsPort = 24173;
            ////});

            ////services.Configure<MvcOptions>(options =>
            ////{
            ////    options.Filters.Add(new RequireHttpsAttribute());
            ////});

            // Configure CORS
            services.AddCors(options =>
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
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            // See https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.0&tabs=visual-studio.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ProcessingTools.Web.Documents API V1",
                });
            });

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

            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerDependency();

            builder.RegisterInstance(ProcessingTools.Common.Constants.Defaults.Encoding).As<System.Text.Encoding>().SingleInstance();

            builder.RegisterModule(new XmlTransformersAutofacModule { Configuration = this.Configuration });
            builder.RegisterModule<TransformersFactoriesAutofacModule>();
            builder.RegisterModule<ProcessorsAutofacModule>();
            builder.RegisterModule<ProcessingTools.Configuration.Autofac.Geo.CoordinatesAutofacModule>();
            builder.RegisterModule(new DataAutofacModule { Configuration = this.Configuration });
            builder.RegisterModule(new DataAccessAutofacModule { Configuration = this.Configuration });
            builder.RegisterModule(new ServicesAutofacModule { Configuration = this.Configuration });
            builder.RegisterModule<ServicesWebAutofacModule>();

            // Rabbit MQ
            builder
                .Register(c => new ConnectionFactory
                {
                    HostName = this.Configuration.GetValue<string>(ConfigurationConstants.MessageQueueHostName),
                    Port = this.Configuration.GetValue<int>(ConfigurationConstants.MessageQueuePort),
                    VirtualHost = this.Configuration.GetValue<string>(ConfigurationConstants.MessageQueueVirtualHost),
                    UserName = this.Configuration.GetValue<string>(ConfigurationConstants.MessageQueueUserName),
                    Password = this.Configuration.GetValue<string>(ConfigurationConstants.MessageQueuePassword),
                    AutomaticRecoveryEnabled = true,
                    Ssl = new SslOption
                    {
                        AcceptablePolicyErrors = System.Net.Security.SslPolicyErrors.None,
                    },
                })
                .As<IConnectionFactory>()
                .SingleInstance();
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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            // See https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.0&tabs=visual-studio.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            // See https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.0&tabs=visual-studio.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProcessingTools.Web.Documents API V1");
            });

            // For most apps, calls to UseAuthentication, UseAuthorization, and UseCors must appear between the calls to UseRouting and UseEndpoints to be effective.
            // See https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-3.0&tabs=visual-studio#opt-in-to-runtime-compilation.
            app.UseRouting();

            app.UseStaticFiles();

            app.UseWebSockets();

            app.UseApplicationContext();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapAreaControllerRoute(
                   name: "AdminAreaRoute",
                   areaName: AreaNames.Admin,
                   pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "DataAreaRoute",
                   areaName: AreaNames.Data,
                   pattern: "Data/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "DocumentsAreaRoute",
                   areaName: AreaNames.Documents,
                   pattern: "Documents/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "FilesAreaRoute",
                   areaName: AreaNames.Files,
                   pattern: "Files/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "LayoutAreaRoute",
                   areaName: AreaNames.Layout,
                   pattern: "Layout/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "TestAreaRoute",
                   areaName: AreaNames.Test,
                   pattern: "Test/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "ToolsAreaRoute",
                   areaName: AreaNames.Tools,
                   pattern: "Tools/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "defaultRoute",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapFallbackToController(
                    action: ErrorController.HandleUnknownActionActionName,
                    controller: ErrorController.ControllerName);

                endpoints.MapRazorPages();

                endpoints.MapHub<ChatHub>("/r/chat");

                endpoints.MapHealthChecks("/health", HealthChecksExtensions.GetHealthCheckOptionsExcludingVersion(this.GetType().Assembly, env.IsDevelopment()));

                endpoints.MapHealthChecks("/version", HealthChecksExtensions.GetHealthCheckOptionsForVersion(this.GetType().Assembly, env.IsDevelopment())).RequireAuthorization(new AuthorizeAttribute { Roles = "admin" });
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

            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
            app.UseBrowserLink();

            this.Configure(app, env);

            app.ServeStaticFiles(env, "node_modules", "/node_modules");
            app.ServeStaticFiles(env, "node_modules", "/lib");

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

            app.UseExceptionHandler("/Error");
            app.UseStatusCodePagesWithRedirects("/Error/Code/{0}");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            this.Configure(app, env);

            app.ServeStaticFiles(env, "node_modules", "/node_modules");
            app.ServeStaticFiles(env, "node_modules", "/lib");

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

            app.UseExceptionHandler("/Error");
            app.UseStatusCodePagesWithRedirects("/Error/Code/{0}");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            this.Configure(app, env);

            app.UseCors("StrictCorsPolicy");
        }
    }
}
