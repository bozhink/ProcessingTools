namespace ProcessingTools.Bio.Environments.Data.Repositories
{
    using System;
    using Contracts;
    using ProcessingTools.Bio.Environments.Data.Contracts;

    public class BioEnvironmentsDbContextProvider : IBioEnvironmentsDbContextProvider
    {
        private readonly IBioEnvironmentsDbContextFactory contextFactory;

        public BioEnvironmentsDbContextProvider(IBioEnvironmentsDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
        }

        public BioEnvironmentsDbContext Create()
        {
            return this.contextFactory.Create();
        }
    }
}