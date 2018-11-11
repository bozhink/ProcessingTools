// <copyright file="MorphologicalEpithetRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Bio.MorphologicalEpithets
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Bio;
    using ProcessingTools.Models.Contracts.Bio;

    /// <summary>
    /// Represents request model for the morphological epithets API.
    /// </summary>
    public class MorphologicalEpithetRequestModel : IMorphologicalEpithet
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the morphological epithet object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the morphological epithet object.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfMorphologicalEpithetName)]
        public string Name { get; set; }
    }
}
