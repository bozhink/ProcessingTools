// <copyright file="IStringKeyCollectionValuePairsRepository.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    /// <summary>
    /// String-key-collection-value-pairs repository.
    /// </summary>
    /// <typeparam name="T">Type of the list model.</typeparam>
    public interface IStringKeyCollectionValuePairsRepository<T> : IKeyCollectionValuePairsRepository<string, T>
    {
    }
}
