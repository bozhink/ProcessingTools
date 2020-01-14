// <copyright file="IZooBankTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.ZooBank
{
    using ProcessingTools.Contracts.Services.Xml;

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
