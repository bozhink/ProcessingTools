// <copyright file="SetPasswordBindingModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Web;

    /// <summary>
    /// Binding model for password creation.
    /// </summary>
    public class SetPasswordBindingModel
    {
        /// <summary>
        /// Gets or sets the value of the password.
        /// </summary>
        [Required]
        [StringLength(ValidationConstants.PasswordMaximalLength, ErrorMessageResourceName = nameof(Strings.PasswordErrorMessage), ErrorMessageResourceType = typeof(Strings), MinimumLength = ValidationConstants.PasswordMinimalLength)]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Strings.NewPassword), ResourceType = typeof(Strings))]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirmation value for the password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = nameof(Strings.ConfirmPassword), ResourceType = typeof(Strings))]
        [Compare(nameof(NewPassword), ErrorMessageResourceName = nameof(Strings.ConfirmPasswordErrorMessage), ErrorMessageResourceType = typeof(Strings))]
        public string ConfirmPassword { get; set; }
    }
}
