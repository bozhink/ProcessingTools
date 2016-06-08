namespace ProcessingTools.Services.Common.Factories
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Data.Common.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;
    using ProcessingTools.Extensions;

    public abstract class DataServiceBaseFactory<TDbModel, TServiceModel> : IDataServiceBase<TServiceModel>
        where TDbModel : class
        where TServiceModel : class
    {
        protected readonly IGenericRepositoryProvider<IGenericRepository<TDbModel>, TDbModel> repositoryProvider;

        public DataServiceBaseFactory(IGenericRepositoryProvider<IGenericRepository<TDbModel>, TDbModel> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;

            this.MapDataToServiceModelFunc = this.MapDataToServiceModel.Compile();
            this.MapServiceToDataModelFunc = this.MapServiceToDataModel.Compile();
        }

        private Func<TDbModel, TServiceModel> MapDataToServiceModelFunc { get; set; }

        private Func<TServiceModel, TDbModel> MapServiceToDataModelFunc { get; set; }

        protected abstract Expression<Func<TDbModel, TServiceModel>> MapDataToServiceModel { get; }

        protected abstract Expression<Func<TServiceModel, TDbModel>> MapServiceToDataModel { get; }

        public virtual async Task<object> Add(TServiceModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = this.MapServiceToDataModelFunc.Invoke(model);

            var repository = this.repositoryProvider.Create();

            await repository.Add(entity: entity);
            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
        }

        public virtual async Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var repository = this.repositoryProvider.Create();

            await repository.Delete(id: id);
            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
        }

        public virtual async Task<TServiceModel> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var repository = this.repositoryProvider.Create();

            var entity = await repository.Get(id: id);
            var result = this.MapDataToServiceModelFunc.Invoke(entity);

            repository.TryDispose();

            return result;
        }

        public virtual async Task<object> Update(TServiceModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = this.MapServiceToDataModelFunc.Invoke(model);

            var repository = this.repositoryProvider.Create();

            await repository.Update(entity: entity);
            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
        }
    }
}
