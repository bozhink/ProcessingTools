// <copyright file="UserContext.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Shared
{
    /// <summary>
    /// User Context
    /// </summary>
    public class UserContext
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

        /// <summary>
        /// Gets the user ID.
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// Gets the user name.
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Gets the user e-mail.
        /// </summary>
        public string UserEmail { get; }
    }
}
