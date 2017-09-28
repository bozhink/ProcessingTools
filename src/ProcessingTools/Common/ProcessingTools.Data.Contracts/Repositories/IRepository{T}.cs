// <copyright file="IRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories
{
    /// <summary>
    /// Generic repository.
    /// </summary>
    /// <typeparam name="T">Type of database context.</typeparam>
    public interface IRepository<T> : IRepository
    {
    }
}
