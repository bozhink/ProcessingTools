namespace ProcessingTools.Mediatypes.Data.Entity.Providers
{
    using System;
    using Contracts;

    public class MediatypesDbContextProvider : IMediatypesDbContextProvider
    {
        private readonly IMediatypesDbContextFactory contextFactory;

        public MediatypesDbContextProvider(IMediatypesDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
        }

        public MediatypesDbContext Create()
        {
            return this.contextFactory.Create();
        }
    }
}
