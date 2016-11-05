namespace ProcessingTools.MediaType.Data.Entity.Providers
{
    using System;
    using Contracts;

    public class MediaTypesDbContextProvider : IMediaTypesDbContextProvider
    {
        private readonly IMediatypesDbContextFactory contextFactory;

        public MediaTypesDbContextProvider(IMediatypesDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
        }

        public MediaTypesDbContext Create()
        {
            return this.contextFactory.Create();
        }
    }
}
