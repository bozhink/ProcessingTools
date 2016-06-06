namespace ProcessingTools.Services.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using AutoMapper;
    using Contracts;
    using Factories;

    using ProcessingTools.Data.Common.Models.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class GenericRepositoryDataService<TDbModel, TServiceModel> : MultiDataServiceWithRepositoryFactory<TDbModel, TServiceModel>, IDataService<TServiceModel>
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

        protected override Expression<Func<TDbModel, IEnumerable<TServiceModel>>> MapDbModelToServiceModel => e => new TServiceModel[]
        {
            this.mapper.Map<TServiceModel>(e)
        };

        protected override Expression<Func<TServiceModel, IEnumerable<TDbModel>>> MapServiceModelToDbModel => m => new TDbModel[]
        {
            this.mapper.Map<TDbModel>(m)
        };

        protected override Expression<Func<TDbModel, object>> SortExpression => e => e.Id;
    }
}
