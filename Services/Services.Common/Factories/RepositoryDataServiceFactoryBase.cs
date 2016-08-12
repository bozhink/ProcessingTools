namespace ProcessingTools.Services.Common.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Data.Common.Repositories.Contracts;

    public abstract class RepositoryDataServiceFactoryBase<TDbModel, TServiceModel> : IRepositoryDataService<TDbModel, TServiceModel>
    {
        public virtual async Task<object> Add(IGenericRepository<TDbModel> repository, params TServiceModel[] models)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (models == null || models.Length < 1)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = this.MapServiceModelsToEntities(models);

            var tasks = entities.Select(e => repository.Add(e))
                .ToArray();

            await Task.WhenAll(tasks);

            return await repository.SaveChanges();
        }

        public abstract Task<IQueryable<TServiceModel>> All(IGenericRepository<TDbModel> repository);

        public virtual async Task<object> Delete(IGenericRepository<TDbModel> repository, params object[] ids)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (ids == null || ids.Length < 1)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var tasks = ids.Select(id => repository.Delete(id))
                .ToArray();

            await Task.WhenAll(tasks);

            return await repository.SaveChanges();
        }

        public virtual async Task<object> Delete(IGenericRepository<TDbModel> repository, params TServiceModel[] models)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (models == null || models.Length < 1)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = this.MapServiceModelsToEntities(models);

            var tasks = entities.Select(e => repository.Delete(e))
                .ToArray();

            await Task.WhenAll(tasks);

            return await repository.SaveChanges();
        }

        public abstract Task<IQueryable<TServiceModel>> Get(IGenericRepository<TDbModel> repository, params object[] ids);

        public abstract Task<IQueryable<TServiceModel>> Query(IGenericRepository<TDbModel> repository, int skip, int take);

        public virtual async Task<object> Update(IGenericRepository<TDbModel> repository, params TServiceModel[] models)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (models == null || models.Length < 1)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = this.MapServiceModelsToEntities(models);

            var tasks = entities.Select(e => repository.Update(e))
                .ToArray();

            await Task.WhenAll(tasks);

            return await repository.SaveChanges();
        }

        protected abstract IEnumerable<TDbModel> MapServiceModelsToEntities(TServiceModel[] models);
    }
}
