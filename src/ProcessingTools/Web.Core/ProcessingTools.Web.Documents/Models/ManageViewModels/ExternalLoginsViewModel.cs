// <copyright file="ExternalLoginsViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Models.ManageViewModels
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// External logins view model.
    /// </summary>
    public class ExternalLoginsViewModel
    {
        /// <summary>
        /// Gets or sets the current logins.
        /// </summary>
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        /// <summary>
        /// Gets or sets the other logins.
        /// </summary>
        public IList<AuthenticationScheme> OtherLogins { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the remove button.
        /// </summary>
        public bool ShowRemoveButton { get; set; }

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        public string StatusMessage { get; set; }
    }
}
