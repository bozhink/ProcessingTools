// <copyright file="ITextContentTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Contracts.Content
{
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// Text content transformers factory.
    /// </summary>
    public interface ITextContentTransformerFactory
    {
        /// <summary>
        /// Gets text content transformer.
        /// </summary>
        /// <returns>The transformer.</returns>
        IXmlTransformer GetTextContentTransformer();
    }
}
