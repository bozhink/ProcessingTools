// <copyright file="UserContext.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Models
{
    /// <summary>
    /// User Context
    /// </summary>
    public class UserContext
    {
        public UserContext(string userId, string userName, string userEmail)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.UserEmail = userEmail;
        }

        /// <summary>
        /// Gets the User Id.
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// Gets the User Name.
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Gets the User e-mail.
        /// </summary>
        public string UserEmail { get; }
    }
}
