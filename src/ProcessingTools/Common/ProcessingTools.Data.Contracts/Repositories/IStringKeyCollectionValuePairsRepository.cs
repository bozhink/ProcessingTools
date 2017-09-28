// <copyright file="IStringKeyCollectionValuePairsRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories
{
    public interface IStringKeyCollectionValuePairsRepository<T> : IKeyCollectionValuePairsRepository<string, T>
    {
    }
}
