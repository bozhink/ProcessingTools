// <copyright file="DbContextInitializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Abstractions
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Generic DbContext initializer.
    /// </summary>
    /// <typeparam name="T">Type of the DbContext.</typeparam>
    public abstract class DbContextInitializer<T> : IDbContextInitializer<T>
        where T : DbContext
    {
        private readonly T context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextInitializer{T}"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DbContext"/> to be initialized.</param>
        protected DbContextInitializer(T context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<object> InitializeAsync()
        {
            // TODO: EnsureCreatedAsync => MigrateAsync
            await this.context.Database.EnsureCreatedAsync().ConfigureAwait(false);

            this.SetInitializer();

            return true;
        }

        /// <summary>
        /// Executes post-migration custom initialization.
        /// </summary>
        protected abstract void SetInitializer();
    }
}
