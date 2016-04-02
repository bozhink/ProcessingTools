namespace ProcessingTools.Services.Common.Factories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Contracts;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Common.Repositories.Contracts;
    using ProcessingTools.Extensions;

    public class RepositoryDataServiceFactory<TDbModel, TServiceModel> : IDataService<TServiceModel>, IDisposable
    {
        private readonly IMapper mapper;
        private readonly IGenericRepository<TDbModel> repository;

        public RepositoryDataServiceFactory(IGenericRepository<TDbModel> repository)
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

        public virtual async Task Add(TServiceModel model)
        {
            var entity = this.MapServiceModelToDbModel(model);
            await this.repository.Add(entity)
                .ContinueWith(_ => this.repository.SaveChanges().Wait());
        }

        public virtual async Task<IQueryable<TServiceModel>> All()
        {
            return (await this.repository.All())
                .Select(e => this.MapDbModelToServiceModel(e));
        }

        public virtual async Task Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await this.repository.Delete(id)
                .ContinueWith(_ => this.repository.SaveChanges().Wait());
        }

        public virtual async Task Delete(TServiceModel model)
        {
            var entity = this.MapServiceModelToDbModel(model);
            await this.repository.Delete(entity)
                .ContinueWith(_ => this.repository.SaveChanges().Wait());
        }

        public virtual async Task<TServiceModel> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await this.repository.Get(id);
            return this.MapDbModelToServiceModel(entity);
        }

        public virtual async Task<IQueryable<TServiceModel>> Get(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > DefaultPagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            return (await this.repository.All(skip, take))
                .Select(e => this.MapDbModelToServiceModel(e));
        }

        public virtual async Task Update(TServiceModel model)
        {
            var entity = this.MapServiceModelToDbModel(model);
            await this.repository.Update(entity)
                .ContinueWith(_ => this.repository.SaveChanges().Wait());
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.repository.TryDispose();
            }
        }

        private TDbModel MapServiceModelToDbModel(TServiceModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = this.mapper.Map<TDbModel>(model);
            if (entity == null)
            {
                throw new ApplicationException(nameof(entity));
            }

            return entity;
        }

        private TServiceModel MapDbModelToServiceModel(TDbModel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var model = this.mapper.Map<TServiceModel>(entity);
            if (model == null)
            {
                throw new ApplicationException(nameof(model));
            }

            return model;
        }
    }
}