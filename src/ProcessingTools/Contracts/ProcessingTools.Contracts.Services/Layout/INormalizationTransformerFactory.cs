// <copyright file="INormalizationTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Common.Enumerations;
using ProcessingTools.Contracts.Services.Xml;

namespace ProcessingTools.Contracts.Services.Layout
{
    /// <summary>
    /// Normalization transformer factory.
    /// </summary>
    public interface INormalizationTransformerFactory
    {
        /// <summary>
        /// Get normalization transformer factory.
        /// </summary>
        /// <param name="schemaType">Schema of the output document.</param>
        /// <returns><see cref="IXmlTransformer"/>.</returns>
        IXmlTransformer Create(SchemaType schemaType);
    }
}
