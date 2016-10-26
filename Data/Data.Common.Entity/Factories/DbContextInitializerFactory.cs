namespace ProcessingTools.Data.Common.Entity.Factories
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using Contracts;

    public abstract class DbContextInitializerFactory<TContext> : IDbContextInitializer<TContext>
        where TContext : DbContext
    {
        private readonly IDbContextProvider<TContext> contextProvider;

        public DbContextInitializerFactory(IDbContextProvider<TContext> contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public async Task<object> Initialize()
        {
            this.SetInitializer();

            using (var context = this.contextProvider.Create())
            {
                context.Database.CreateIfNotExists();
                context.Database.Initialize(true);
                await context.SaveChangesAsync();
            }

            return true;
        }

        protected abstract void SetInitializer();
    }
}