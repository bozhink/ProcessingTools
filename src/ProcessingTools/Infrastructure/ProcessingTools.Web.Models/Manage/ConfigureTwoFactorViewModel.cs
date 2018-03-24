// <copyright file="ConfigureTwoFactorViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Manage
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// Configure two factor view model
    /// </summary>
    public class ConfigureTwoFactorViewModel
    {
        /// <summary>
        /// Gets or sets the selected provider.
        /// </summary>
        public string SelectedProvider { get; set; }

        /// <summary>
        /// Gets or sets providers.
        /// </summary>
        public ICollection<SelectListItem> Providers { get; set; }
    }
}
