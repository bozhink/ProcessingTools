// <copyright file="ApplicationRole.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using ProcessingTools.Common.Constants;

    /// <summary>
    /// Application role.
    /// </summary>
    public class ApplicationRole : IdentityRole<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRole"/> class.
        /// </summary>
        public ApplicationRole()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        public override string Id { get; set; }
    }
}
