// <copyright file="IIterableRepository.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// Iterable repository.
    /// </summary>
    /// <typeparam name="T">Type of model.</typeparam>
    public interface IIterableRepository<T> : IRepository<T>
    {
        /// <summary>
        /// Gets entities.
        /// </summary>
        IEnumerable<T> Entities { get; }
    }
}
