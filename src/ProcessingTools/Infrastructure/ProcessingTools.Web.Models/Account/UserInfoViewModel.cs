// <copyright file="UserInfoViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Account
{
    /// <summary>
    /// User info
    /// </summary>
    public class UserInfoViewModel
    {
        /// <summary>
        /// Gets or sets email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user is registered.
        /// </summary>
        public bool HasRegistered { get; set; }

        /// <summary>
        /// Gets or sets login provider.
        /// </summary>
        public string LoginProvider { get; set; }
    }
}
