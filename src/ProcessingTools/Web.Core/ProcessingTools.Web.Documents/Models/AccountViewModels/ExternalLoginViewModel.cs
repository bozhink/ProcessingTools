// <copyright file="ExternalLoginViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// External login view model.
    /// </summary>
    public class ExternalLoginViewModel
    {
        /// <summary>
        /// Gets or sets the e-mail.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
