// <copyright file="ISearchableRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface ISearchableRepository<T> : IRepository<T>
    {
        IQueryable<T> Query { get; }

        Task<T> GetByIdAsync(object id);

        Task<T> FindFirstAsync(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter);
    }
}
