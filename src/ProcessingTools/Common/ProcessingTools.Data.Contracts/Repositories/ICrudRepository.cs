// <copyright file="ICrudRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface ICrudRepository<T> : ISearchableRepository<T>
    {
        Task<long> CountAsync();

        Task<long> CountAsync(Expression<Func<T, bool>> filter);

        Task<object> AddAsync(T entity);

        Task<object> DeleteAsync(object id);

        Task<object> UpdateAsync(T entity);

        Task<object> UpdateAsync(object id, IUpdateExpression<T> updateExpression);
    }
}
