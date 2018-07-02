// <copyright file="RemoveLoginBindingModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Binding model for login removal.
    /// </summary>
    public class RemoveLoginBindingModel
    {
        /// <summary>
        /// Gets or sets login provider.
        /// </summary>
        [Required]
        [Display(Name = nameof(Strings.LoginProvider), ResourceType = typeof(Strings))]
        public string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets provider key.
        /// </summary>
        [Required]
        [Display(Name = nameof(Strings.ProviderKey), ResourceType = typeof(Strings))]
        public string ProviderKey { get; set; }
    }
}
