// <copyright file="MorphologicalEpithetResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Bio.MorphologicalEpithets
{
    using ProcessingTools.Contracts.Models.Bio;

    /// <summary>
    /// Represents response model for the morphological epithets API.
    /// </summary>
    public class MorphologicalEpithetResponseModel : IMorphologicalEpithet
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the morphological epithet object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the morphological epithet object.
        /// </summary>
        public string Name { get; set; }
    }
}
