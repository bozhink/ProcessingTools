// <copyright file="IZooBankTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Xml;

namespace ProcessingTools.Contracts.Services.Bio.ZooBank
{
    /// <summary>
    /// ZooBank transformer factory.
    /// </summary>
    public interface IZooBankTransformerFactory
    {
        /// <summary>
        /// Get ZooBank registration transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/>.</returns>
        IXmlTransformer GetZooBankRegistrationTransformer();
    }
}
