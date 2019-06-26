// <copyright file="UserContext.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Shared
{
    using ProcessingTools.Contracts;

    /// <summary>
    /// User context.
    /// </summary>
    public class UserContext : IUserContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserContext"/> class.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <param name="userName">User name.</param>
        /// <param name="userEmail">User e-mail.</param>
        public UserContext(string userId, string userName, string userEmail)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.UserEmail = userEmail;
        }

        /// <inheritdoc/>
        public string UserId { get; }

        /// <inheritdoc/>
        public string UserName { get; }

        /// <inheritdoc/>
        public string UserEmail { get; }
    }
}
