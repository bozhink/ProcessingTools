namespace ProcessingTools.Services.Common
{
    using System;
    using System.Linq.Expressions;

    using Contracts;
    using Factories;

    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class StringRepositoryDataService : SimpleDataServiceWithRepositoryFactory<string, string>, IDataService<string>
    {
        public StringRepositoryDataService(IGenericRepository<string> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<string, string>> MapDbModelToServiceModel => e => e;

        protected override Expression<Func<string, string>> MapServiceModelToDbModel => e => e;

        protected override Expression<Func<string, object>> SortExpression => e => e;
    }
}