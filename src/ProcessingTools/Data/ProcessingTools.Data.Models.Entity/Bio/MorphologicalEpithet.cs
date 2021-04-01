// <copyright file="MorphologicalEpithet.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Bio
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Bio;

    /// <summary>
    /// Morphological epithet entity.
    /// </summary>
    public class MorphologicalEpithet
    {
        /// <summary>
        /// Gets or sets the ID of the morphological epithet entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the morphological epithet.
        /// </summary>
        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfMorphologicalEpithetName)]
        public string Name { get; set; }
    }
}
