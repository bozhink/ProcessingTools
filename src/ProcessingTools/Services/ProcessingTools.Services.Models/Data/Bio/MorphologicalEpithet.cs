﻿// <copyright file="MorphologicalEpithet.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Bio
{
    using ProcessingTools.Services.Models.Contracts.Bio;

    /// <summary>
    /// Morphological epithet service model.
    /// </summary>
    public class MorphologicalEpithet : IMorphologicalEpithet
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the morphological epithet.
        /// </summary>
        public string Name { get; set; }
    }
}