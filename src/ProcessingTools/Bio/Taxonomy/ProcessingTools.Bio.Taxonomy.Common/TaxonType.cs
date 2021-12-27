﻿// <copyright file="TaxonType.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Common
{
    /// <summary>
    /// Taxon type.
    /// </summary>
    public enum TaxonType
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Lower
        /// </summary>
        Lower = 1,

        /// <summary>
        /// Higher
        /// </summary>
        Higher = 2,

        /// <summary>
        /// Any
        /// </summary>
        Any = 3,
    }
}