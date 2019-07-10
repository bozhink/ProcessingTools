// <copyright file="IBioTaxonomyTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Services.Xml;

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
