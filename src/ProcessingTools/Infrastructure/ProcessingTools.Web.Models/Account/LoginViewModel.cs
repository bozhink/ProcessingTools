// <copyright file="LoginViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Login view model
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [Display(Name = nameof(Strings.Email), ResourceType = typeof(Strings))]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Strings.Password), ResourceType = typeof(Strings))]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to remember this login.
        /// </summary>
        [Display(Name = nameof(Strings.RememberMe), ResourceType = typeof(Strings))]
        public bool RememberMe { get; set; }
    }
}
