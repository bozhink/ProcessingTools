// <copyright file="ExternalLoginConfirmationViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// External login confirmation view model
    /// </summary>
    public class ExternalLoginConfirmationViewModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [Display(Name = nameof(Strings.Email), ResourceType = typeof(Strings))]
        public string Email { get; set; }
    }
}
