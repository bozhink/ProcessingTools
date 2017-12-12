// <copyright file="IAbbreviationsTransformersFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Harvesters.Abbreviations
{
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// Abbreviations transformers factory.
    /// </summary>
    public interface IAbbreviationsTransformersFactory
    {
        /// <summary>
        /// Gets abbreviations transformer.
        /// </summary>
        /// <returns>The transformer.</returns>
        IXmlTransformer GetAbbreviationsTransformer();
    }
}
