// <copyright file="ForgotPasswordViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Forgot password view model
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = nameof(Strings.Email), ResourceType = typeof(Strings))]
        public string Email { get; set; }
    }
}
