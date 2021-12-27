﻿// <copyright file="TaxonRankRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Api.Models
{
    /// <summary>
    /// Taxon rank request model.
    /// </summary>
    public class TaxonRankRequestModel
    {
        /// <summary>
        /// Gets or sets the taxon name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the taxon rank.
        /// </summary>
        public string Rank { get; set; }
    }
}