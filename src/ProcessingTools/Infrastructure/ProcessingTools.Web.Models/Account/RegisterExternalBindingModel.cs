// <copyright file="RegisterExternalBindingModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Binding model for registration of external login.
    /// </summary>
    public class RegisterExternalBindingModel
    {
        /// <summary>
        /// Gets or sets email.
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
