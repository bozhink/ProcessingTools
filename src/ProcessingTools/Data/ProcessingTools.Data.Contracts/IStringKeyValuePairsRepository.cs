// <copyright file="IStringKeyValuePairsRepository.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    /// <summary>
    /// String key-value pairs repository.
    /// </summary>
    /// <typeparam name="T">Type of the value.</typeparam>
    public interface IStringKeyValuePairsRepository<T> : IKeyValuePairsRepository<string, T>
    {
    }
}
