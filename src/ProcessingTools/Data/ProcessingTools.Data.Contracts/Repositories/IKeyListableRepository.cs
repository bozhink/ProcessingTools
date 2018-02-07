// <copyright file="IKeyListableRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Collections.Generic;

    /// <summary>
    /// Key listable repository.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    public interface IKeyListableRepository<out TKey> : IRepository
    {
        /// <summary>
        /// Gets the keys.
        /// </summary>
        IEnumerable<TKey> Keys { get; }
    }
}
