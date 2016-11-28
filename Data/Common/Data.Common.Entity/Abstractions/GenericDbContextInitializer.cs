namespace ProcessingTools.Data.Common.Entity.Abstractions
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;
    using Contracts;

    public abstract class GenericDbContextInitializer<TContext> : IDbContextInitializer<TContext>
        where TContext : DbContext
    {
        private readonly IDbContextFactory<TContext> contextFactory;

        public GenericDbContextInitializer(IDbContextFactory<TContext> contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
        }

        public async Task<object> Initialize()
        {
            using (var context = this.contextFactory.Create())
            {
                if (context.Database.CreateIfNotExists())
                {
                    await context.SaveChangesAsync();
                }
            }

            this.SetInitializer();

            return true;
        }

        protected abstract void SetInitializer();
    }
}
