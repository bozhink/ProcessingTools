// <copyright file="ICrudRepositoryProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    /// <summary>
    /// CRUD repository provider.
    /// </summary>
    /// <typeparam name="T">Type of the model.</typeparam>
    public interface ICrudRepositoryProvider<T> : IRepositoryProvider<ICrudRepository<T>>
    {
    }
}
