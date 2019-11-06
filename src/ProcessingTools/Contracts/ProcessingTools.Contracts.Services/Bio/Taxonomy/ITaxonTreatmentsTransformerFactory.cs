﻿// <copyright file="ITaxonTreatmentsTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Services.Xml;

    /// <summary>
    /// Taxon treatments transformer factory.
    /// </summary>
    public interface ITaxonTreatmentsTransformerFactory
    {
        /// <summary>
        /// Get taxon treatment format transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/>.</returns>
        IXmlTransformer GetTaxonTreatmentFormatTransformer();

        /// <summary>
        /// Get taxon treatment extract materials transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/>.</returns>
        IXmlTransformer GetTaxonTreatmentExtractMaterialsTransformer();
    }
}