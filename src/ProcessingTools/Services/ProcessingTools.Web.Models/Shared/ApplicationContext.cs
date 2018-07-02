// <copyright file="ApplicationContext.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Shared
{
    using System;
    using System.Security.Claims;
    using ProcessingTools.Contracts;

    /// <summary>
    /// Application context.
    /// </summary>
    public class ApplicationContext : IApplicationContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationContext"/> class.
        /// </summary>
        /// <param name="user"><see cref="ClaimsPrincipal"/> object with the user information.</param>
        public ApplicationContext(ClaimsPrincipal user)
        {
            if (user != null)
            {
                string userID = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                string userName = user.FindFirst(ClaimTypes.Name)?.Value;
                string userEmail = user.FindFirst(ClaimTypes.Email)?.Value;

                this.UserContext = new UserContext(userID, userName, userEmail);
            }
            else
            {
                this.UserContext = null;
            }

            this.DateTimeProvider = () => DateTime.UtcNow;
            this.GuidProvider = () => Guid.NewGuid();
        }

        /// <inheritdoc/>
        public IUserContext UserContext { get; }

        /// <inheritdoc/>
        public Func<DateTime> DateTimeProvider { get; }

        /// <inheritdoc/>
        public Func<Guid> GuidProvider { get; }
    }
}
