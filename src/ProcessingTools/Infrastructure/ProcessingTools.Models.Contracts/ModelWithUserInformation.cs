// <copyright file="ModelWithUserInformation.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants;

    /// <summary>
    /// Model with user information.
    /// </summary>
    public abstract class ModelWithUserInformation : ICreated, IModified
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelWithUserInformation"/> class.
        /// </summary>
        protected ModelWithUserInformation()
        {
            this.ModifiedOn = DateTime.UtcNow;
            this.CreatedOn = this.ModifiedOn;
        }

        /// <inheritdoc/>
        [Display(Name = "Date Created")]
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        [Display(Name = "Date Modified")]
        public DateTime ModifiedOn { get; set; }

        /// <inheritdoc/>
        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        [Display(Name = "Created By User")]
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        [Display(Name = "Modified By User")]
        public string ModifiedBy { get; set; }
    }
}
