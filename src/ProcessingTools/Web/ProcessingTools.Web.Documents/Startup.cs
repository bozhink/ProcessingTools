// <copyright file="Startup.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents
{
    using System;
    using System.Text.Json;
    using Autofac;
    using AutoMapper;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Web.Documents.Controllers;
    using ProcessingTools.Web.Documents.Data;
    using ProcessingTools.Web.Documents.Extensions;
    using ProcessingTools.Web.Documents.Formatters;
    using ProcessingTools.Web.Documents.Hubs;
    using ProcessingTools.Web.Documents.Models;
    using ProcessingTools.Web.Documents.Settings;

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
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Collection of services.</param>
        /// <returns>Service provider.</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSignalR();

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

            // Configure authorization
            // See https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims
            // See https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ElevatedRights", policy =>
                {
                    policy.RequireRole("Administrator", "PowerUser", "BackupAdministrator");
                });
            });

            // Configure MVC Core
            services.AddMvcCore()
              .AddApiExplorer()
              .AddAuthorization()
              .AddFormatterMappings()
              .AddXmlDataContractSerializerFormatters()
              .AddXmlSerializerFormatters()
              .AddViews()
              .AddRazorViewEngine()
              .AddCacheTagHelper()
              .AddDataAnnotations()
              .AddCors();

            // Configure MVC
            services
                .AddControllersWithViews(options =>
                {
                    options.RespectBrowserAcceptHeader = true;
                    options.MaxModelValidationErrors = 50;
                    options.InputFormatters.Insert(0, new RawRequestBodyFormatter());
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

            services.AddRazorPages();

            // Configure HTTPS
            // See https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.1&tabs=visual-studio
            //// services.AddHsts(options =>
            //// {
            ////     options.Preload = true;
            ////     options.IncludeSubDomains = true;
            ////     options.MaxAge = TimeSpan.FromDays(30);
            //// });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                ////options.HttpsPort = 24173;
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            // Configure CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("*");
                });
            });

            // Configure Razor
            services.Configure<RazorViewEngineOptions>(options =>
            {
                ////options.AreaViewLocationFormats.Clear();
                ////options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
            });

            // Configure AutoMapper.
            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddMaps(typeof(ProcessingTools.Configuration.AutoMapper.AssemblySetup).Assembly);
            });

            services.AddSingleton<IMapper>(mapperConfiguration.CreateMapper());

            // Configure container
            var container = services.BuildContainer(this.Configuration);

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
            app.UseStaticFiles();

            if (env.IsDevelopment() || env.IsStaging())
            {
                app.ServeStaticFiles(env, "node_modules", "/node_modules");
                app.ServeStaticFiles(env, "node_modules", "/lib");
            }

            app.UseCors("CorsPolicy");
            app.UseWebSockets();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseApplicationContext();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "AdminAreaRoute",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "DataAreaRoute",
                   areaName: "Data",
                   pattern: "Data/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "DocumentsAreaRoute",
                   areaName: "Documents",
                   pattern: "Documents/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "FilesAreaRoute",
                   areaName: "Files",
                   pattern: "Files/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "LayoutAreaRoute",
                   areaName: "Layout",
                   pattern: "Layout/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "TestAreaRoute",
                   areaName: "Test",
                   pattern: "Test/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "ToolsAreaRoute",
                   areaName: "Tools",
                   pattern: "Tools/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "defaultRoute",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapFallbackToController(
                    action: ErrorController.HandleUnknownActionActionName,
                    controller: ErrorController.ControllerName);

                endpoints.MapRazorPages();

                endpoints.MapHub<ChatHub>("/r/chat");
            });
        }
    }
}
