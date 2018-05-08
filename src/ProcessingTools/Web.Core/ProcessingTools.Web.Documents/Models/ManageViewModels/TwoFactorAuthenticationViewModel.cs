// <copyright file="TwoFactorAuthenticationViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Models.ManageViewModels
{
    /// <summary>
    /// Two Factor Authentication view model.
    /// </summary>
    public class TwoFactorAuthenticationViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether authenticator is present.
        /// </summary>
        public bool HasAuthenticator { get; set; }

        /// <summary>
        /// Gets or sets the number of available recovery codes.
        /// </summary>
        public int RecoveryCodesLeft { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 2FA is enabled.
        /// </summary>
        public bool Is2faEnabled { get; set; }
    }
}
