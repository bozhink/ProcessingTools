// <copyright file="ManageInfoViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Account
{
    using System.Collections.Generic;

    /// <summary>
    /// Manage info
    /// </summary>
    public class ManageInfoViewModel
    {
        /// <summary>
        /// Gets or sets local login provider.
        /// </summary>
        public string LocalLoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets user login information.
        /// </summary>
        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        /// <summary>
        /// Gets or sets external login providers.
        /// </summary>
        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }
}
