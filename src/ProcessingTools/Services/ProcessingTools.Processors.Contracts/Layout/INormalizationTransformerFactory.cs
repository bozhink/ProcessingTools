// <copyright file="INormalizationTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Layout
{
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Xml;

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
