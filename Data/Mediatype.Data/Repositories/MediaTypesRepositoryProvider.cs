namespace ProcessingTools.MediaType.Data.Repositories
{
    using System;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.MediaType.Data.Contracts;
    using ProcessingTools.MediaType.Data.Repositories.Contracts;

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