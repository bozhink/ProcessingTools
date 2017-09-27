// <copyright file="IRepositoryFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface IRepositoryFactory<out TRepository>
        where TRepository : IRepository
    {
        TRepository Create();
    }
}
