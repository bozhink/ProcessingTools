// <copyright file="ICodesTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Bio.Codes
{
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// Codes transformer factory.
    /// </summary>
    public interface ICodesTransformerFactory
    {
        /// <summary>
        /// Get codes remove non-code nodes transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/></returns>
        IXmlTransformer GetCodesRemoveNonCodeNodesTransformer();
    }
}
