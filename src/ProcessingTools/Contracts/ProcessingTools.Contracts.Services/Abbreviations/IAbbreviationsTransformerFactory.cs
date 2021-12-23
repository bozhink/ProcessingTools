// <copyright file="IAbbreviationsTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Abbreviations
{
    using ProcessingTools.Contracts.Services.Xml;

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
