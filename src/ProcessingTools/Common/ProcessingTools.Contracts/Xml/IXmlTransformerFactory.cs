// <copyright file="IXmlTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Xml
{
    /// <summary>
    /// <see cref="IXmlTransformer"/> factory.
    /// </summary>
    public interface IXmlTransformerFactory
    {
        /// <summary>
        /// Creates <see cref="IXmlTransformer"/> from source script.
        /// </summary>
        /// <param name="source">Source script to build the transformer.</param>
        /// <returns>Instance of <see cref="IXmlTransformer"/>.</returns>
        IXmlTransformer CreateXmlTransformerFromSourceScript(string source);
    }
}
