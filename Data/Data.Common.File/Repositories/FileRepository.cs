namespace ProcessingTools.Data.Common.File.Repositories
{
    using System;
    using Contracts;
    using ProcessingTools.Data.Common.File.Contracts;

    public class FileRepository<TContext, TEntity> : IFileRepository<TEntity>
        where TContext : IFileDbContext<TEntity>
    {
        public FileRepository(IFileDbContextProvider<TContext, TEntity> contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.Context = contextProvider.Create();
        }

        protected TContext Context { get; private set; }
    }
}
