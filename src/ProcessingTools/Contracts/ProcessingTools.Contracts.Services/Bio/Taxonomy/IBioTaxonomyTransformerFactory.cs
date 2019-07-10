// <copyright file="IBioTaxonomyTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Xml;

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    /// <summary>
    /// Bio taxonomy transformer factory.
    /// </summary>
    public interface IBioTaxonomyTransformerFactory
    {
        /// <summary>
        /// Get remove taxon name parts transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/>.</returns>
        IXmlTransformer GetRemoveTaxonNamePartsTransformer();

        /// <summary>
        /// Get parse treatment meta with internal information transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/>.</returns>
        IXmlTransformer GetParseTreatmentMetaWithInternalInformationTransformer();
    }
}
