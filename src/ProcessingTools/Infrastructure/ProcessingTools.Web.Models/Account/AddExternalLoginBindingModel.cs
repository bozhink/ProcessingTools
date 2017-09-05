// <copyright file="AddExternalLoginBindingModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Binding model for creation of a new external login.
    /// </summary>
    public class AddExternalLoginBindingModel
    {
        /// <summary>
        /// Gets or sets the external access token.
        /// </summary>
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }
}
