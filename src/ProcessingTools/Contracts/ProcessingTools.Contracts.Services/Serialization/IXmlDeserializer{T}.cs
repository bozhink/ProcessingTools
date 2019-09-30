// <copyright file="IXmlDeserializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Serialization
{
    /// <summary>
    /// Generic XML deserializer.
    /// </summary>
    /// <typeparam name="T">Type of output result.</typeparam>
    public interface IXmlDeserializer<out T> : IDeserializer<T>
    {
    }
}
