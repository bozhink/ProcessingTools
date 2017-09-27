// <copyright file="IKeyListableRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Collections.Generic;

    public interface IKeyListableRepository<out TKey> : IRepository
    {
        IEnumerable<TKey> Keys { get; }
    }
}
