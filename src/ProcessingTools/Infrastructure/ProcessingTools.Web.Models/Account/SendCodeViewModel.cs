// <copyright file="SendCodeViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Account
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// Send code view model
    /// </summary>
    public class SendCodeViewModel
    {
        /// <summary>
        /// Gets or sets selected provider.
        /// </summary>
        public string SelectedProvider { get; set; }

        /// <summary>
        /// Gets or sets providers.
        /// </summary>
        public ICollection<SelectListItem> Providers { get; set; }

        /// <summary>
        /// Gets or sets the return-url.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to remember this login.
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
