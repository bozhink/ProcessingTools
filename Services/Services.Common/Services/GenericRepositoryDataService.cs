namespace ProcessingTools.Services.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Contracts;
    using Factories;

    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class GenericRepositoryDataService<TDbModel, TServiceModel> : GenericRepositoryDataServiceFactory<TDbModel, TServiceModel>, IDataService<TServiceModel>
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

        protected override IEnumerable<TDbModel> MapServiceModelToDbModel(params TServiceModel[] models)
        {
            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = new HashSet<TDbModel>(models.Select(m => this.mapper.Map<TDbModel>(m)));
            if (entities == null)
            {
                throw new ApplicationException(nameof(entities));
            }

            return entities;
        }

        protected override IEnumerable<TServiceModel> MapDbModelToServiceModel(params TDbModel[] entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            var models = new HashSet<TServiceModel>(entities.Select(e => this.mapper.Map<TServiceModel>(e)));
            if (models == null)
            {
                throw new ApplicationException(nameof(models));
            }

            return models;
        }
    }
}