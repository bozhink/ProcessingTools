// <copyright file="ISpecialTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Xml;

namespace ProcessingTools.Contracts.Services.Special
{
    /// <summary>
    /// Special transformers factory.
    /// </summary>
    public interface ISpecialTransformerFactory
    {
        /// <summary>
        /// Returns Gavin Laurens Transformer.
        /// </summary>
        /// <returns>Gavin-Laurens XmlTransformer.</returns>
        IXmlTransformer GetGavinLaurensTransformer();
    }
}
