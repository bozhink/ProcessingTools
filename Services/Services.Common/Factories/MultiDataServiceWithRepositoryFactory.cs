namespace ProcessingTools.Services.Common.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Extensions;

    public abstract class MultiDataServiceWithRepositoryFactory<TDbModel, TServiceModel> : RepositoryMultiDataServiceFactory<TDbModel, TServiceModel>, IMultiEntryDataService<TServiceModel>, IDisposable
    {
        private readonly ISearchableCountableCrudRepository<TDbModel> repository;

        public MultiDataServiceWithRepositoryFactory(ISearchableCountableCrudRepository<TDbModel> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        protected override abstract Expression<Func<TDbModel, IEnumerable<TServiceModel>>> MapDbModelToServiceModel { get; }

        protected override abstract Expression<Func<TServiceModel, IEnumerable<TDbModel>>> MapServiceModelToDbModel { get; }

        protected override abstract Expression<Func<TDbModel, object>> SortExpression { get; }

        public virtual Task<object> Add(params TServiceModel[] models) => base.Add(repository: this.repository, models: models);

        public virtual Task<IQueryable<TServiceModel>> All() => base.All(repository: this.repository);

        public virtual Task<IQueryable<TServiceModel>> Query(int skip, int take) => base.Query(repository: this.repository, skip: skip, take: take);

        public virtual Task<object> Delete(params object[] ids) => base.Delete(repository: this.repository, ids: ids);

        public virtual Task<object> Delete(params TServiceModel[] models) => base.Delete(repository: this.repository, models: models);

        public virtual Task<IQueryable<TServiceModel>> Get(params object[] ids) => base.Get(repository: this.repository, ids: ids);

        public virtual Task<object> Update(params TServiceModel[] models) => base.Update(repository: this.repository, models: models);

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
    }
}