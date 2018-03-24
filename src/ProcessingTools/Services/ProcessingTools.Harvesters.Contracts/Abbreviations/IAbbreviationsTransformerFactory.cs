// <copyright file="IAbbreviationsTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Contracts.Abbreviations
{
    using ProcessingTools.Contracts.Xml;

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
