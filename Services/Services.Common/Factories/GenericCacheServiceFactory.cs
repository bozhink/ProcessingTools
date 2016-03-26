namespace ProcessingTools.Services.Common.Factories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Data.Common.Models.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public abstract class GenericCacheServiceFactory<TContext, TId, TDbModel, TServiceModel> : ICacheService<TContext, TId, TServiceModel>
        where TDbModel : IGenericEntity<TId>
        where TServiceModel : IGenericServiceModel<TId>
    {
        private readonly IMapper mapper;

        private IGenericContextRepository<TContext, TId, TDbModel> repository;

        public GenericCacheServiceFactory(IGenericContextRepository<TContext, TId, TDbModel> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<TDbModel, TServiceModel>();
                c.CreateMap<TServiceModel, TDbModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        public Task Add(TContext context, TServiceModel model)
        {
            var entity = this.mapper.Map<TDbModel>(model);
            return this.repository.Add(context, entity);
        }

        public async Task<IQueryable<TServiceModel>> All(TContext context)
        {
            return (await this.repository.All(context))
                .Select(i => this.mapper.Map<TServiceModel>(i));
        }

        public Task Delete(TContext context)
        {
            return this.repository.Delete(context);
        }

        public Task Delete(TContext context, TServiceModel model)
        {
            return this.repository.Delete(context, model.Id);
        }

        public Task Delete(TContext context, TId id)
        {
            return this.repository.Delete(context, id);
        }

        public async Task<TServiceModel> Get(TContext context, TId id)
        {
            var entity = await this.repository.Get(context, id);
            return this.mapper.Map<TServiceModel>(entity);
        }

        public Task Update(TContext context, TServiceModel model)
        {
            return this.repository.Update(context, this.mapper.Map<TDbModel>(model));
        }
    }
}