// <copyright file="IDbContextInitializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Abstractions
{
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Contracts.Data;

    /// <summary>
    /// Generic DbContext initializer.
    /// </summary>
    /// <typeparam name="T">Type of the DbContext.</typeparam>
    public interface IDbContextInitializer<T> : IDatabaseInitializer
        where T : DbContext
    {
    }
}
