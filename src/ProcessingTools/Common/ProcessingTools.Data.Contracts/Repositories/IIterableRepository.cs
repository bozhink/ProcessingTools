// <copyright file="IIterableRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Collections.Generic;

    public interface IIterableRepository<T> : IRepository<T>
    {
        IEnumerable<T> Entities { get; }
    }
}
