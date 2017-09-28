// <copyright file="ICrudRepositoryProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories
{
    public interface ICrudRepositoryProvider<T> : IRepositoryProvider<ICrudRepository<T>>
    {
    }
}
