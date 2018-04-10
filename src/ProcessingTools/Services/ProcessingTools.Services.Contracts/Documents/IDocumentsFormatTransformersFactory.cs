// <copyright file="IDocumentsFormatTransformersFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Documents
{
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// Documents format transformers factory.
    /// </summary>
    public interface IDocumentsFormatTransformersFactory
    {
        /// <summary>
        /// Gets format XML to HTML transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/> instance.</returns>
        IXmlTransformer GetFormatXmlToHtmlTransformer();

        /// <summary>
        /// Gets format HTML to XML transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/> instance.</returns>
        IXmlTransformer GetFormatHtmlToXmlTransformer();
    }
}
