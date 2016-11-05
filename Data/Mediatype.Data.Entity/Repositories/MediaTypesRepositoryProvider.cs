namespace ProcessingTools.MediaType.Data.Entity.Repositories
{
    using System;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;

    public class MediaTypesRepositoryProvider<T> : IMediaTypesRepositoryProvider<T>
        where T : class
    {
        private readonly IMediaTypesDbContextProvider contextProvider;

        public MediaTypesRepositoryProvider(IMediaTypesDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public ISearchableCountableCrudRepository<T> Create()
        {
            return new MediaTypesRepository<T>(this.contextProvider);
        }
    }
}
