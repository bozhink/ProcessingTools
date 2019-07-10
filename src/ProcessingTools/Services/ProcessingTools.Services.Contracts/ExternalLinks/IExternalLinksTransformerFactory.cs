// <copyright file="IExternalLinksTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Xml;

namespace ProcessingTools.Contracts.Services.ExternalLinks
{
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
