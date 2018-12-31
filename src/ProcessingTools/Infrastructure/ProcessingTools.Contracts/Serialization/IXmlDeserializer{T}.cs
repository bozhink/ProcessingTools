﻿// <copyright file="IXmlDeserializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Serialization
{
    /// <summary>
    /// Generic XML deserializer.
    /// </summary>
    /// <typeparam name="T">Type of output result.</typeparam>
    public interface IXmlDeserializer<T> : IDeserializer<T>
    {
    }
}
