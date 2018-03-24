// <copyright file="ExternalLoginViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Account
{
    /// <summary>
    /// External login provider
    /// </summary>
    public class ExternalLoginViewModel
    {
        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }
    }
}
