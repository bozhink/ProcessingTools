namespace ProcessingTools.Bio.Environments.Data.Repositories
{
    using System;
    using ProcessingTools.Bio.Environments.Data.Contracts;
    using ProcessingTools.Bio.Environments.Data.Repositories.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class BioEnvironmentsRepositoryProvider<T> : IBioEnvironmentsRepositoryProvider<T>
        where T : class
    {
        private IBioEnvironmentsDbContextProvider contextProvider;

        public BioEnvironmentsRepositoryProvider(IBioEnvironmentsDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public IGenericRepository<T> Create()
        {
            return new BioEnvironmentsRepository<T>(this.contextProvider);
        }
    }
}