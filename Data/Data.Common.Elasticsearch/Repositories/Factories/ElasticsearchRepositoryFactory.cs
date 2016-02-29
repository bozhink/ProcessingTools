namespace ProcessingTools.Data.Common.Elasticsearch.Repositories.Factories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;

    using ProcessingTools.Data.Common.Models.Contracts;

    // TODO
    public abstract class ElasticsearchRepositoryFactory<TEntity> : IElasticsearchGenericRepository<TEntity>
        where TEntity : IEntity
    {
        public Task Add(string context, TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> All(string context)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string context)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string context, TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string context, int id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Get(string context, int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges(string context)
        {
            throw new NotImplementedException();
        }

        public Task Update(string context, TEntity entity)
        {
            throw new NotImplementedException();
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
                // There is nothing to be disposed.
            }
        }
    }
}
