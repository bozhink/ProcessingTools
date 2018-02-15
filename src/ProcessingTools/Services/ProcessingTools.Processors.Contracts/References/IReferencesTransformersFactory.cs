// <copyright file="IReferencesTransformersFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.References
{
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// References transformers factory.
    /// </summary>
    public interface IReferencesTransformersFactory
    {
        /// <summary>
        /// Get references tag template transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/></returns>
        IXmlTransformer GetReferencesTagTemplateTransformer();

        /// <summary>
        /// Get references get references transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/></returns>
        IXmlTransformer GetReferencesGetReferencesTransformer();
    }
}
