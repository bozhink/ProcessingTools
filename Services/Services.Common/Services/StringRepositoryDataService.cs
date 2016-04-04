namespace ProcessingTools.Services.Common
{
    using System;
    using System.Collections.Generic;

    using Contracts;
    using Factories;

    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class StringRepositoryDataService : RepositoryDataServiceAbstractFactory<string, string>, IDataService<string>
    {
        public StringRepositoryDataService(IGenericRepository<string> repository)
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