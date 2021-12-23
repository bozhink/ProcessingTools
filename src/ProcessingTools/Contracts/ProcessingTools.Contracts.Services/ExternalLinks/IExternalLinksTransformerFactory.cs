// <copyright file="IExternalLinksTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.ExternalLinks
{
    using ProcessingTools.Contracts.Services.Xml;

    /// <summary>
    /// External links transformers factory.
    /// </summary>
    public interface IExternalLinksTransformerFactory
    {
        /// <summary>
        /// Gets external links transformer.
        /// </summary>
        /// <returns>The transformer.</returns>
        IXmlTransformer GetExternalLinksTransformer();
    }
}
