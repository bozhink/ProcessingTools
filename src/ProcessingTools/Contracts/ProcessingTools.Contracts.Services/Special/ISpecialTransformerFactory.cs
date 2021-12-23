// <copyright file="ISpecialTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Special
{
    using ProcessingTools.Contracts.Services.Xml;

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
