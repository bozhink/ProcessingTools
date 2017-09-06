// <copyright file="ManageLoginsViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Manage
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;

    /// <summary>
    /// Manage logins view model
    /// </summary>
    public class ManageLoginsViewModel
    {
        /// <summary>
        /// Gets or sets information about current logins.
        /// </summary>
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        /// <summary>
        /// Gets or sets information about other logins.
        /// </summary>
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
}
