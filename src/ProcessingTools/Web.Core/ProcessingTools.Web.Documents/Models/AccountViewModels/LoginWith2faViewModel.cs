// <copyright file="LoginWith2faViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Login with 2FA view model.
    /// </summary>
    public class LoginWith2faViewModel
    {
        /// <summary>
        /// Gets or sets the two-factor code.
        /// </summary>
        [Required]
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Authenticator code")]
        public string TwoFactorCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether machine have to be remembered.
        /// </summary>
        [Display(Name = "Remember this machine")]
        public bool RememberMachine { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether login have to be remembered.
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
