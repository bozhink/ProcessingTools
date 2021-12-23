// <copyright file="GeoEpithet.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Geo epithet entity.
    /// </summary>
    public class GeoEpithet : BaseModel, INamedIntegerIdentified
    {
        /// <summary>
        /// Gets or sets the ID of the geo epithet entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of the geo epithet.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfGeoEpithetName)]
        [MaxLength(ValidationConstants.MaximalLengthOfGeoEpithetName)]
        public string Name { get; set; }
    }
}
