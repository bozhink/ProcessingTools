﻿// <copyright file="ApplicationOAuthProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;

    /// <summary>
    /// Default implementation of <see cref="IOAuthAuthorizationServerProvider"/>
    /// </summary>
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string publicClientId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationOAuthProvider"/> class.
        /// </summary>
        /// <param name="publicClientId">Public ClientID</param>
        public ApplicationOAuthProvider(string publicClientId)
        {
            this.publicClientId = publicClientId ?? throw new ArgumentNullException(nameof(publicClientId));
        }

        /// <summary>
        /// Creates <see cref="AuthenticationProperties"/> for a specified user.
        /// </summary>
        /// <param name="userName">The user name</param>
        /// <returns><see cref="AuthenticationProperties"/> object</returns>
        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };

            return new AuthenticationProperties(data);
        }

        /// <inheritdoc/>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            var user = await userManager.FindAsync(context.UserName, context.Password).ConfigureAwait(false);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oauthIdentity = await user.GenerateUserIdentityAsync(
                userManager,
                OAuthDefaults.AuthenticationType)
                .ConfigureAwait(false);

            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(
                userManager,
                CookieAuthenticationDefaults.AuthenticationType)
                .ConfigureAwait(false);

            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oauthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        /// <inheritdoc/>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        /// <inheritdoc/>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        /// <inheritdoc/>
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == this.publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}
