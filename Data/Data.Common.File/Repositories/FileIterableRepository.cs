namespace ProcessingTools.Data.Common.File.Repositories
{
    using System;
    using System.Collections.Generic;

    using Contracts;

    using ProcessingTools.Data.Common.File.Contracts;

    public class FileIterableRepository<TContext, T> : IFileIterableRepository<T>
        where TContext : IFileDbContext<T>
    {
        public FileIterableRepository(IFileDbContextProvider<TContext, T> contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.Context = contextProvider.Create();
        }

        public virtual IEnumerable<T> Entities => this.Context.DataSet;

        protected virtual TContext Context { get; private set; }
    }
}
