// <copyright file="ITextContentTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Xml;

namespace ProcessingTools.Contracts.Services.Content
{
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
