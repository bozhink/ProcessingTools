﻿// <copyright file="ISpecialTransformersFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Processors.Special
{
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// Special Transformers Factory
    /// </summary>
    public interface ISpecialTransformersFactory
    {
        /// <summary>
        /// Returns Gavin Laurens Transformer.
        /// </summary>
        /// <returns>Gavin Laurens XmlTransformer</returns>
        IXmlTransformer GetGavinLaurensTransformer();
    }
}
