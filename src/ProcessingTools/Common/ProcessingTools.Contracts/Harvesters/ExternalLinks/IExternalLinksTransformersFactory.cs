// <copyright file="IExternalLinksTransformersFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Harvesters.ExternalLinks
{
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// External links transformers factory.
    /// </summary>
    public interface IExternalLinksTransformersFactory
    {
        /// <summary>
        /// Gets external links transformer.
        /// </summary>
        /// <returns>The transformer.</returns>
        IXmlTransformer GetExternalLinksTransformer();
    }
}
