namespace ProcessingTools.MediaType.Data.Repositories
{
    using System;
    using Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

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

        public IGenericRepository<T> Create()
        {
            return new MediaTypesRepository<T>(this.contextProvider);
        }
    }
}