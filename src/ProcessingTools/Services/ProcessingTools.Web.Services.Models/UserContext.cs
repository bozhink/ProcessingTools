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
        /// <summary>
        /// Gets or sets the User Id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the User Name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the User e-mail.
        /// </summary>
        public string UserEmail { get; set; }
    }
}
