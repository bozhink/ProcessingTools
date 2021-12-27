﻿// <copyright file="TaxonRankSearchRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Api.Models
{
    /// <summary>
    /// Search request model.
    /// </summary>
    public class TaxonRankSearchRequestModel
    {
        /// <summary>
        /// Gets or sets the search string.
        /// </summary>
        public string SearchString { get; set; }
    }
}