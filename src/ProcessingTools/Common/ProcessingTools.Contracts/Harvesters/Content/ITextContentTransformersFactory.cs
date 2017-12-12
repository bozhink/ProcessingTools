// <copyright file="ITextContentTransformersFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Harvesters.Content
{
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// Text content transformers factory.
    /// </summary>
    public interface ITextContentTransformersFactory
    {
        /// <summary>
        /// Gets text content transformer.
        /// </summary>
        /// <returns>The transformer.</returns>
        IXmlTransformer GetTextContentTransformer();
    }
}
