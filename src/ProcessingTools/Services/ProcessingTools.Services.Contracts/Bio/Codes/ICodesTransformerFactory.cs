// <copyright file="ICodesTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Xml;

namespace ProcessingTools.Contracts.Services.Bio.Codes
{
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
