// <copyright file="RemoveLoginViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Models.ManageViewModels
{
    /// <summary>
    /// Remove login view model.
    /// </summary>
    public class RemoveLoginViewModel
    {
        /// <summary>
        /// Gets or sets the login provider.
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the provider key.
        /// </summary>
        public string ProviderKey { get; set; }
    }
}
