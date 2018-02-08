﻿namespace ProcessingTools.Data.Common.Entity.Abstractions
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public abstract class GenericDbContextInitializer<TContext> : IDbContextInitializer<TContext>
        where TContext : DbContext
    {
        private readonly IDbContextFactory<TContext> contextFactory;

        protected GenericDbContextInitializer(IDbContextFactory<TContext> contextFactory)
        {
            this.contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public async Task<object> InitializeAsync()
        {
            using (var context = this.contextFactory.Create())
            {
                if (context.Database.CreateIfNotExists())
                {
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
            }

            this.SetInitializer();

            return true;
        }

        protected abstract void SetInitializer();
    }
}
