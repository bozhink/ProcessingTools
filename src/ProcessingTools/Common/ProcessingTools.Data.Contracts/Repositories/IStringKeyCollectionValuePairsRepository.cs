// <copyright file="IStringKeyCollectionValuePairsRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface IStringKeyCollectionValuePairsRepository<T> : IKeyCollectionValuePairsRepository<string, T>
    {
    }
}
