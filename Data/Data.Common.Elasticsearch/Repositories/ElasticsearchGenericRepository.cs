namespace ProcessingTools.Data.Common.Elasticsearch.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Elasticsearch.Contracts;

    using Nest;

    using ProcessingTools.Data.Common.Models.Contracts;

    public class ElasticsearchGenericRepository<TEntity> : IElasticsearchGenericRepository<TEntity>
    {
        private IElasticContextProvider contextProvider;
        private IElasticClientProvider clientProvider;

        public ElasticsearchGenericRepository(IElasticContextProvider contextProvider, IElasticClientProvider clientProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException("contextProvider");
            }

            if (clientProvider == null)
            {
                throw new ArgumentNullException("clientProvider");
            }

            this.contextProvider = contextProvider;
            this.Context = this.contextProvider.Context;

            this.clientProvider = clientProvider;
            this.Client = this.clientProvider.Client;
        }

        private Indices Context { get; set; }

        private IElasticClient Client { get; set; }

        public Task Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> All()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> All(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Get(object id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task Update(TEntity entity)
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
