// <copyright file="IDataContractJsonDeserializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Serialization
{
    /// <summary>
    /// Generic DataContract JSON deserializer.
    /// </summary>
    /// <typeparam name="T">Type of output result.</typeparam>
    public interface IDataContractJsonDeserializer<T> : IDeserializer<T>
    {
    }
}
