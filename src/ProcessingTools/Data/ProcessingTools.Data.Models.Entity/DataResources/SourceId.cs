// <copyright file="SourceId.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.DataResources
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Common.Constants.Data.DataResources;
    using ProcessingTools.Contracts.Models.Resources;

    /// <summary>
    /// Source ID entity.
    /// </summary>
    public class SourceId : ISourceIdEntity
    {
        /// <summary>
        /// Gets or sets the ID of the source ID.
        /// </summary>
        [Key]
        [Required]
        [MaxLength(ValidationConstants.SourceIdMaximalLength)]
        public string Id { get; set; }

        /// <inheritdoc/>
        [NotMapped]
        string ISourceIdEntity.SourceId => this.Id;
    }
}
