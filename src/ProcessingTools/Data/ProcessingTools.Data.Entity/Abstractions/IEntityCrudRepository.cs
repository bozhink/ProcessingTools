// <copyright file="IEntityCrudRepository.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Abstractions
{
    using ProcessingTools.Data.Contracts;

    public interface IEntityCrudRepository<T> : ICrudRepository<T>, IEntityRepository<T>
        where T : class
    {
    }
}
