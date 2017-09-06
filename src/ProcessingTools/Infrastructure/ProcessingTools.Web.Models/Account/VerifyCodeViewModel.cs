// <copyright file="VerifyCodeViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Verify code view model
    /// </summary>
    public class VerifyCodeViewModel
    {
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        [Required]
        public string Provider { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [Required]
        [Display(Name = nameof(Strings.Code), ResourceType = typeof(Strings))]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the return-url.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to remember the browser.
        /// </summary>
        [Display(Name = nameof(Strings.RememberBrowser), ResourceType = typeof(Strings))]
        public bool RememberBrowser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to remember this login.
        /// </summary>
        [Display(Name = nameof(Strings.RememberMe), ResourceType = typeof(Strings))]
        public bool RememberMe { get; set; }
    }
}
