// <copyright file="INormalizationTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Layout
{
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Normalization transformer factory.
    /// </summary>
    public interface INormalizationTransformerFactory
    {
        /// <summary>
        /// Get normalization transformer factory.
        /// </summary>
        /// <param name="schemaType">Schema of the output document.</param>
        /// <returns><see cref="IXmlTransformer"/></returns>
        IXmlTransformer Create(SchemaType schemaType);
    }
}
