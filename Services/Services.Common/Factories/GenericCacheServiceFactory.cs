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

    public abstract class GenericCacheServiceFactory<TKey, TId, TDbModel, TServiceModel> : ICacheService<TKey, TId, TServiceModel>
        where TDbModel : IGenericEntity<TId>
        where TServiceModel : IGenericServiceModel<TId>
    {
        private readonly IMapper mapper;

        private IGenericRepository<TKey, TId, TDbModel> repository;

        public GenericCacheServiceFactory(IGenericRepository<TKey, TId, TDbModel> repository)
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

        public async Task Add(TKey key, TServiceModel entity)
        {
            var item = this.mapper.Map<TDbModel>(entity);
            await this.repository.Add(key, item);
        }

        public async Task<IQueryable<TServiceModel>> All(TKey key)
        {
            return (await this.repository.All(key))
                .Select(i => this.mapper.Map<TServiceModel>(i));
        }

        public async Task Delete(TKey key)
        {
            await this.repository.Delete(key);
        }

        public async Task Delete(TKey key, TServiceModel entity)
        {
            await this.repository.Delete(key, entity.Id);
        }

        public async Task Delete(TKey key, TId id)
        {
            await this.repository.Delete(key, id);
        }

        public async Task<TServiceModel> Get(TKey key, TId id)
        {
            var entity = await this.repository.Get(key, id);
            return this.mapper.Map<TServiceModel>(entity);
        }

        public async Task Update(TKey key, TServiceModel entity)
        {
            await this.repository.Update(key, this.mapper.Map<TDbModel>(entity));
        }
    }
}