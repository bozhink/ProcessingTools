// <copyright file="IStringKeyValuePairsRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories
{
    /// <summary>
    /// String key-value pairs repository.
    /// </summary>
    /// <typeparam name="T">Type of the value.</typeparam>
    public interface IStringKeyValuePairsRepository<T> : IKeyValuePairsRepository<string, T>
    {
    }
}
