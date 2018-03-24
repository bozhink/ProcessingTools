// <copyright file="IndexViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Manage
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// Index view model
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether password is present.
        /// </summary>
        public bool HasPassword { get; set; }

        /// <summary>
        /// Gets or sets login information.
        /// </summary>
        public IList<UserLoginInfo> Logins { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether two factor authorization is applied.
        /// </summary>
        public bool TwoFactor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether browser has to be remebered.
        /// </summary>
        public bool BrowserRemembered { get; set; }
    }
}
