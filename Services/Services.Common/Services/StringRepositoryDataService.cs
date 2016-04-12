namespace ProcessingTools.Services.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Contracts;
    using Factories;

    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class StringRepositoryDataService : GenericRepositoryDataServiceFactory<string, string>, IDataService<string>
    {
        public StringRepositoryDataService(IGenericRepository<string> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<string, IEnumerable<string>>> MapDbModelToServiceModel => e => new string[] 
        {
            e
        };

        protected override Expression<Func<string, IEnumerable<string>>> MapServiceModelToDbModel => e => new string[]
        {
            e
        };

        protected override Expression<Func<string, object>> SortExpression => e => e;
    }
}