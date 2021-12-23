// <copyright file="ITextContentTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Content
{
    using ProcessingTools.Contracts.Services.Xml;

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
