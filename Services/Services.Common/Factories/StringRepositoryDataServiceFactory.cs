namespace ProcessingTools.Services.Common.Factories
{
    using System;
    using System.Collections.Generic;

    using Contracts;

    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class StringRepositoryDataServiceFactory : RepositoryDataServiceAbstractFactory<string, string>, IDataService<string>
    {
        public StringRepositoryDataServiceFactory(IGenericRepository<string> repository)
            : base(repository)
        {
        }

        protected override IEnumerable<string> MapDbModelToServiceModel(params string[] entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            return new HashSet<string>(entities);
        }

        protected override IEnumerable<string> MapServiceModelToDbModel(params string[] models)
        {
            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            return new HashSet<string>(models);
        }
    }
}