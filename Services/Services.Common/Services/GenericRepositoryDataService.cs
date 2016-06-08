namespace ProcessingTools.Services.Common
{
    using System;
    using System.Linq.Expressions;

    using AutoMapper;
    using Contracts;
    using Factories;

    using ProcessingTools.Data.Common.Models.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class GenericRepositoryDataService<TDbModel, TServiceModel> : SimpleDataServiceWithRepositoryFactory<TDbModel, TServiceModel>, IMultiEntryDataService<TServiceModel>
        where TDbModel : class, IEntity
        where TServiceModel : class
    {
        private readonly IMapper mapper;

        public GenericRepositoryDataService(IGenericRepository<TDbModel> repository)
            : base(repository)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<TDbModel, TServiceModel>();
                c.CreateMap<TServiceModel, TDbModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override Expression<Func<TDbModel, TServiceModel>> MapDbModelToServiceModel => e => this.mapper.Map<TServiceModel>(e);

        protected override Expression<Func<TServiceModel, TDbModel>> MapServiceModelToDbModel => m => this.mapper.Map<TDbModel>(m);

        protected override Expression<Func<TDbModel, object>> SortExpression => e => e.Id;
    }
}
