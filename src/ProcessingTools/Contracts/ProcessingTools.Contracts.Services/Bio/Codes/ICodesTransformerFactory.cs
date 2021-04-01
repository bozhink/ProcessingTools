// <copyright file="ICodesTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Codes
{
    using ProcessingTools.Contracts.Services.Xml;

    /// <summary>
    /// Codes transformer factory.
    /// </summary>
    public interface ICodesTransformerFactory
    {
        /// <summary>
        /// Get codes remove non-code nodes transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/>.</returns>
        IXmlTransformer GetCodesRemoveNonCodeNodesTransformer();
    }
}
