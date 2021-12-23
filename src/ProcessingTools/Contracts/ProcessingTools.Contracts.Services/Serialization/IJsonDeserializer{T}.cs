// <copyright file="IJsonDeserializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Serialization
{
    /// <summary>
    /// Generic DataContract JSON deserializer.
    /// </summary>
    /// <typeparam name="T">Type of output result.</typeparam>
    public interface IJsonDeserializer<out T> : IDeserializer<T>
    {
    }
}
