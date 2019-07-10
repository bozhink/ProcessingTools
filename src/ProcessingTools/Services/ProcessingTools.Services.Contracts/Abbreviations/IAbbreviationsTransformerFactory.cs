// <copyright file="IAbbreviationsTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Xml;

namespace ProcessingTools.Contracts.Services.Abbreviations
{
    /// <summary>
    /// Abbreviations transformers factory.
    /// </summary>
    public interface IAbbreviationsTransformerFactory
    {
        /// <summary>
        /// Gets abbreviations transformer.
        /// </summary>
        /// <returns>The transformer.</returns>
        IXmlTransformer GetAbbreviationsTransformer();
    }
}
