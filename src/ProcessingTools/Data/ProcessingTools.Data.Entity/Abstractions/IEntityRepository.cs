// <copyright file="IEntityRepository.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Abstractions
{
    using ProcessingTools.Data.Contracts;

    public interface IEntityRepository<T> : IRepository<T>
        where T : class
    {
    }
}
