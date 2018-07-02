// <copyright file="VerifyPhoneNumberViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Manage
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Verify phone number view model
    /// </summary>
    public class VerifyPhoneNumberViewModel
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [Required]
        [Display(Name = nameof(Strings.Code), ResourceType = typeof(Strings))]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [Required]
        [Phone]
        [Display(Name = nameof(Strings.PhoneNumber), ResourceType = typeof(Strings))]
        public string PhoneNumber { get; set; }
    }
}
