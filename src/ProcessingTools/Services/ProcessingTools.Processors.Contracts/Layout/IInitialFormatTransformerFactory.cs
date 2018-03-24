// <copyright file="IInitialFormatTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Layout
{
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Initial format transformer factory.
    /// </summary>
    public interface IInitialFormatTransformerFactory
    {
        /// <summary>
        /// Get initial format transformer.
        /// </summary>
        /// <param name="schemaType">Schema of the output document.</param>
        /// <returns><see cref="IXmlTransformer"/></returns>
        IXmlTransformer Create(SchemaType schemaType);
    }
}
