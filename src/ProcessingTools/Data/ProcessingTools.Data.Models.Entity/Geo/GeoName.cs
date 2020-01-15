// <copyright file="GeoName.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Geo name entity.
    /// </summary>
    public class GeoName : BaseModel, INamedIntegerIdentified
    {
        /// <summary>
        /// Gets or sets the ID of the geo name entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of the geo name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfGeoName)]
        [MaxLength(ValidationConstants.MaximalLengthOfGeoName)]
        public string Name { get; set; }
    }
}
