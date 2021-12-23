// <copyright file="Synonym.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;

    /// <summary>
    /// Synonym.
    /// </summary>
    public abstract class Synonym : BaseModel, ISynonym
    {
        /// <summary>
        /// Gets or sets the ID of synonym.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of the synonym.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfGeoName)]
        [MaxLength(ValidationConstants.MaximalLengthOfGeoName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the language code of the synonym.
        /// </summary>
        public int? LanguageCode { get; set; }
    }
}
