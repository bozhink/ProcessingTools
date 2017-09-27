// <copyright file="IDatabaseInitializer.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data
{
    using System.Threading.Tasks;

    /// <summary>
    /// Database initializer.
    /// </summary>
    public interface IDatabaseInitializer
    {
        /// <summary>
        /// Initializes database.
        /// </summary>
        /// <returns>Task</returns>
        Task<object> InitializeAsync();
    }
}
