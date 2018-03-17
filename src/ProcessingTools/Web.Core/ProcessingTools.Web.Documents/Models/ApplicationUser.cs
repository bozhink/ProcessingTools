// <copyright file="ApplicationUser.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using ProcessingTools.Constants;

    /// <summary>
    /// Application user.
    /// </summary>
    public class ApplicationUser : IdentityUser<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUser"/> class.
        /// </summary>
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        public override string Id { get; set; }
    }
}
