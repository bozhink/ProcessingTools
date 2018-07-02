// <copyright file="ResetPasswordViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Reset password view model.
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Gets or sets the e-mail.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirmation password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; }
    }
}
