// <copyright file="IZooBankTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Bio.ZooBank
{
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// ZooBank transformer factory.
    /// </summary>
    public interface IZooBankTransformerFactory
    {
        /// <summary>
        /// Get ZooBank registration transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/></returns>
        IXmlTransformer GetZooBankRegistrationTransformer();
    }
}
