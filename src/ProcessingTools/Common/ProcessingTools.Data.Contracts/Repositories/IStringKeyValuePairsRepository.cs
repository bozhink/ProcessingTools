// <copyright file="IStringKeyValuePairsRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface IStringKeyValuePairsRepository<T> : IKeyValuePairsRepository<string, T>
    {
    }
}
