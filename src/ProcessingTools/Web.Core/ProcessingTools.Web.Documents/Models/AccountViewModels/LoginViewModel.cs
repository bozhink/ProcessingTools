// <copyright file="LoginViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Login view model.
    /// </summary>
    public class LoginViewModel
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
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether login credentials have to be remembered.
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
