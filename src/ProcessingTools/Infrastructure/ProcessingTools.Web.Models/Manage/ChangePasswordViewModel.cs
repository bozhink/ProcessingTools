﻿// <copyright file="ChangePasswordViewModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Manage
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Web;

    /// <summary>
    /// Change password view model
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Gets or sets the value of the old password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Strings.OldPassword), ResourceType = typeof(Strings))]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets  the new password.
        /// </summary>
        [Required]
        [StringLength(ValidationConstants.PasswordMaximalLength, ErrorMessageResourceName = nameof(Strings.NewPasswordErrorMessage), ErrorMessageResourceType = typeof(Strings), MinimumLength = ValidationConstants.PasswordMinimalLength)]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Strings.NewPassword), ResourceType = typeof(Strings))]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirmation value for the new password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = nameof(Strings.ConfirmPassword), ResourceType = typeof(Strings))]
        [Compare(nameof(NewPassword), ErrorMessageResourceName = nameof(Strings.ConfirmPasswordErrorMessage), ErrorMessageResourceType = typeof(Strings))]
        public string ConfirmPassword { get; set; }
    }
}
