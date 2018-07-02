// <copyright file="AddPhoneNumberViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Manage
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Add phone number view model
    /// </summary>
    public class AddPhoneNumberViewModel
    {
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [Required]
        [Phone]
        [Display(Name = nameof(Strings.PhoneNumber), ResourceType = typeof(Strings))]
        public string Number { get; set; }
    }
}
