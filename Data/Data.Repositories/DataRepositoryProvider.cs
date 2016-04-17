namespace ProcessingTools.Data.Repositories
{
    using System;
    using Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class DataRepositoryProvider<T> : IDataRepositoryProvider<T>
        where T : class
    {
        private readonly IDataDbContextProvider contextProvider;

        public DataRepositoryProvider(IDataDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public IGenericRepository<T> Create()
        {
            return new DataRepository<T>(this.contextProvider);
        }
    }
}