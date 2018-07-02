// <copyright file="RegisterViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Web;

    /// <summary>
    /// Register view model
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = nameof(Strings.Email), ResourceType = typeof(Strings))]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [StringLength(ValidationConstants.PasswordMaximalLength, ErrorMessageResourceName = nameof(Strings.PasswordErrorMessage), ErrorMessageResourceType = typeof(Strings), MinimumLength = ValidationConstants.PasswordMinimalLength)]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Strings.Password), ResourceType = typeof(Strings))]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password confirmation value.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = nameof(Strings.ConfirmPassword), ResourceType = typeof(Strings))]
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(Strings.ConfirmPasswordErrorMessage), ErrorMessageResourceType = typeof(Strings))]
        public string ConfirmPassword { get; set; }
    }
}
