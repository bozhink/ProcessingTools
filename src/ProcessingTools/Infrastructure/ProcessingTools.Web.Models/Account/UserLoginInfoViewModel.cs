// <copyright file="UserLoginInfoViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Account
{
    /// <summary>
    /// User login info
    /// </summary>
    public class UserLoginInfoViewModel
    {
        /// <summary>
        /// Gets or sets login provider.
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets provider key.
        /// </summary>
        public string ProviderKey { get; set; }
    }
}
