namespace ProcessingTools.Mediatypes.Data.Entity.Repositories
{
    using System;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;

    public class MediatypesRepositoryProvider<T> : IMediatypesRepositoryProvider<T>
        where T : class
    {
        private readonly IMediatypesDbContextProvider contextProvider;

        public MediatypesRepositoryProvider(IMediatypesDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public ISearchableCountableCrudRepository<T> Create()
        {
            return new MediatypesRepository<T>(this.contextProvider);
        }
    }
}
