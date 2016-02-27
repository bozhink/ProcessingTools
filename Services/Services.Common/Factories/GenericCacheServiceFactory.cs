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

        private IGenericRepository<TContext, TId, TDbModel> repository;

        public GenericCacheServiceFactory(IGenericRepository<TContext, TId, TDbModel> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<TDbModel, TServiceModel>();
                c.CreateMap<TServiceModel, TDbModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        public async Task Add(TContext context, TServiceModel entity)
        {
            var item = this.mapper.Map<TDbModel>(entity);
            await this.repository.Add(context, item);
        }

        public async Task<IQueryable<TServiceModel>> All(TContext context)
        {
            return (await this.repository.All(context))
                .Select(i => this.mapper.Map<TServiceModel>(i));
        }

        public async Task Delete(TContext context)
        {
            await this.repository.Delete(context);
        }

        public async Task Delete(TContext context, TServiceModel entity)
        {
            await this.repository.Delete(context, entity.Id);
        }

        public async Task Delete(TContext context, TId id)
        {
            await this.repository.Delete(context, id);
        }

        public async Task<TServiceModel> Get(TContext context, TId id)
        {
            var entity = await this.repository.Get(context, id);
            return this.mapper.Map<TServiceModel>(entity);
        }

        public async Task Update(TContext context, TServiceModel entity)
        {
            await this.repository.Update(context, this.mapper.Map<TDbModel>(entity));
        }
    }
}