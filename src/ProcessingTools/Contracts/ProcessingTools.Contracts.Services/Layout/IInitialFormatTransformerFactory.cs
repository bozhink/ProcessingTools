// <copyright file="IInitialFormatTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Layout
{
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Services.Xml;

    /// <summary>
    /// Initial format transformer factory.
    /// </summary>
    public interface IInitialFormatTransformerFactory
    {
        /// <summary>
        /// Get initial format transformer.
        /// </summary>
        /// <param name="schemaType">Schema of the output document.</param>
        /// <returns><see cref="IXmlTransformer"/>.</returns>
        IXmlTransformer Create(SchemaType schemaType);
    }
}
