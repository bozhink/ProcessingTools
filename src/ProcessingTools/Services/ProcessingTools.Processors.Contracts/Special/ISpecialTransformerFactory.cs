// <copyright file="ISpecialTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Special
{
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// Special Transformers Factory
    /// </summary>
    public interface ISpecialTransformerFactory
    {
        /// <summary>
        /// Returns Gavin Laurens Transformer.
        /// </summary>
        /// <returns>Gavin-Laurens XmlTransformer</returns>
        IXmlTransformer GetGavinLaurensTransformer();
    }
}
