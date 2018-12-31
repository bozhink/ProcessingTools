﻿// <copyright file="TaxonRanksRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Bio.Taxonomy.TaxonRanks
{
    using System.Collections.Generic;

    /// <summary>
    /// Taxon ranks request model.
    /// </summary>
    public class TaxonRanksRequestModel
    {
        /// <summary>
        /// Gets or sets the taxon-rank items.
        /// </summary>
        public IList<TaxonRankRequestModel> Items { get; set; } = new List<TaxonRankRequestModel>();
    }
}
