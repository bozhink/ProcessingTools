// <copyright file="IDatabaseProvider{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    /// <summary>
    /// Generic database provider.
    /// </summary>
    /// <typeparam name="T">Type of database context.</typeparam>
    public interface IDatabaseProvider<out T>
    {
        /// <summary>
        /// Creates database context.
        /// </summary>
        /// <returns>Created database context.</returns>
        T Create();
    }
}
