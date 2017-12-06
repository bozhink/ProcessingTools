// <copyright file="IDatabaseProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data
{
    /// <summary>
    /// Database provider.
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
