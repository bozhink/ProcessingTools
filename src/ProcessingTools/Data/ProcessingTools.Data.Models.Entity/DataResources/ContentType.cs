// <copyright file="ContentType.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.DataResources
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.DataResources;
    using ProcessingTools.Contracts.Models.Resources;

    /// <summary>
    /// Content-type entity.
    /// </summary>
    public class ContentType : IContentType
    {
        /// <summary>
        /// Gets or sets the ID of the content-type entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the content-type.
        /// </summary>
        [Required]
        [MaxLength(ValidationConstants.ContentTypeNameMaximalLength)]
        public string Name { get; set; }
    }
}
