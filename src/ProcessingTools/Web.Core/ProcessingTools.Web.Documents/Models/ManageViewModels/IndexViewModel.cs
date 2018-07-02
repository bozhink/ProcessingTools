// <copyright file="IndexViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Models.ManageViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Index view model.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the e-mail is confirmed.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the e-mail.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        public string StatusMessage { get; set; }
    }
}
