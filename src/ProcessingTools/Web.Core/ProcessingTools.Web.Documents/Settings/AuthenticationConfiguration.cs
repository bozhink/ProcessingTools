// <copyright file="AuthenticationConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using System;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ProcessingTools.Common.Constants;

    /// <summary>
    /// Authentication configuration.
    /// </summary>
    public static class AuthenticationConfiguration
    {
        /// <summary>
        /// Configure authentication.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        /// <param name="configuration">Application configuration.</param>
        /// <returns>Configured service collection.</returns>
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
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
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            var authenticationBuilder = services.AddAuthentication();

            if (configuration != null)
            {
                if (!string.IsNullOrWhiteSpace(configuration[ConfigurationConstants.AuthenticationFacebookAppId]) && !string.IsNullOrWhiteSpace(configuration[ConfigurationConstants.AuthenticationFacebookAppSecret]))
                {
                    authenticationBuilder.AddFacebook(facebookOptions =>
                    {
                        facebookOptions.AppId = configuration[ConfigurationConstants.AuthenticationFacebookAppId];
                        facebookOptions.AppSecret = configuration[ConfigurationConstants.AuthenticationFacebookAppSecret];
                    });
                }

                if (!string.IsNullOrWhiteSpace(configuration[ConfigurationConstants.AuthenticationTwitterConsumerKey]) && !string.IsNullOrWhiteSpace(configuration[ConfigurationConstants.AuthenticationTwitterConsumerSecret]))
                {
                    authenticationBuilder.AddTwitter(twitterOptions =>
                    {
                        twitterOptions.ConsumerKey = configuration[ConfigurationConstants.AuthenticationTwitterConsumerKey];
                        twitterOptions.ConsumerSecret = configuration[ConfigurationConstants.AuthenticationTwitterConsumerSecret];
                    });
                }

                if (!string.IsNullOrWhiteSpace(configuration[ConfigurationConstants.AuthenticationGoogleClientId]) && !string.IsNullOrWhiteSpace(configuration[ConfigurationConstants.AuthenticationGoogleClientSecret]))
                {
                    authenticationBuilder.AddGoogle(googleOptions =>
                    {
                        googleOptions.ClientId = configuration[ConfigurationConstants.AuthenticationGoogleClientId];
                        googleOptions.ClientSecret = configuration[ConfigurationConstants.AuthenticationGoogleClientSecret];
                    });
                }

                if (!string.IsNullOrWhiteSpace(configuration[ConfigurationConstants.AuthenticationMicrosoftApplicationId]) && !string.IsNullOrWhiteSpace(configuration[ConfigurationConstants.AuthenticationMicrosoftPassword]))
                {
                    authenticationBuilder.AddMicrosoftAccount(microsoftAccountOptions =>
                    {
                        microsoftAccountOptions.ClientId = configuration[ConfigurationConstants.AuthenticationMicrosoftApplicationId];
                        microsoftAccountOptions.ClientSecret = configuration[ConfigurationConstants.AuthenticationMicrosoftPassword];
                    });
                }
            }

            return services;
        }

        /// <summary>
        /// Configure authentication.
        /// </summary>
        /// <param name="app">Application builder to be configured.</param>
        /// <returns>Configures application builder.</returns>
        public static IApplicationBuilder ConfigureAuthentication(this IApplicationBuilder app)
        {
            app.UseCookiePolicy();
            app.UseAuthentication();
            return app;
        }
    }
}
