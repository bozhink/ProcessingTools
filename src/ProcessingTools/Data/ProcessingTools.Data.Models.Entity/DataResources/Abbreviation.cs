// <copyright file="Abbreviation.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.DataResources
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Common.Constants.Data.DataResources;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Resources;

    /// <summary>
    /// Abbreviation entity.
    /// </summary>
    public class Abbreviation : EntityWithSources, IAbbreviation
    {
        /// <summary>
        /// Gets or sets the ID of the abbreviation entity.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the abbreviation.
        /// </summary>
        [Required]
        [MaxLength(ValidationConstants.AbbreviationNameMaximalLength)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the definition of the abbreviation.
        /// </summary>
        [Required]
        [MaxLength(ValidationConstants.AbbreviationDefinitionMaximalLength)]
        public string Definition { get; set; }

        /// <summary>
        /// Gets or sets the ID of the content-type entity.
        /// </summary>
        public virtual int? ContentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the content-type entity.
        /// </summary>
        public virtual ContentType ContentType { get; set; }

        /// <inheritdoc/>
        [NotMapped]
        string IContentTyped.ContentType => this.ContentType.Name;
    }
}
