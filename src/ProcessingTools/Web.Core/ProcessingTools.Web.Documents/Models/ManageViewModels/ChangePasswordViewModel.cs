// <copyright file="ChangePasswordViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Models.ManageViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Change password view model.
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirmation of the new password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        public string StatusMessage { get; set; }
    }
}
