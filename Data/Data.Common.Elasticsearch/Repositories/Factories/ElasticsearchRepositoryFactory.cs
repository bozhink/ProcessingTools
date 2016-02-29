namespace ProcessingTools.Data.Common.Elasticsearch.Repositories.Factories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Elasticsearch.Contracts;

    using Nest;

    using ProcessingTools.Data.Common.Models.Contracts;

    /// <summary>
    /// Elasticsearch repository factory.
    /// </summary>
    /// <typeparam name="TEntity">Type of the model object.</typeparam>
    /// <remarks>The term ‘context’ used bellow corresponds to the ‘index’ in Elasticsearch terminology.</remarks>
    public abstract class ElasticsearchRepositoryFactory<TEntity> : IElasticsearchGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        private const int MaximalNumberOfItemsToReturn = 10000;

        private IElasticClientProvider provider;
        private IElasticClient client;

        public ElasticsearchRepositoryFactory(IElasticClientProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            this.provider = provider;
            this.client = this.provider.Create();
        }

        private IElasticClient Client => this.client;

        public async Task Add(string context, TEntity entity)
        {
            await this.CreateIndexIfItDoesNotExist(context);
            var response = await this.Client.IndexAsync(entity, idx => idx.Index(context));
        }

        public async Task<IQueryable<TEntity>> All(string context)
        {
            var response = await this.Client.SearchAsync<TEntity>(e => e.From(0).Size(MaximalNumberOfItemsToReturn));
            return response.Documents.AsQueryable();
        }

        public async Task Delete(string context)
        {
            var response = await this.Client.DeleteIndexAsync(context);
        }

        public async Task Delete(string context, TEntity entity)
        {
            var response = await this.Client.DeleteAsync(new DeleteRequest<TEntity>(entity));
        }

        public async Task Delete(string context, int id)
        {
            var entity = await this.Get(context, id);
            await this.Delete(context, entity);
        }

        public async Task<TEntity> Get(string context, int id)
        {
            var response = await this.Client.GetAsync<TEntity>(id, idx => idx.Index(context));
            return response.Source;
        }

        public async Task<int> SaveChanges(string context)
        {
            var response = await this.Client.FlushAsync(context);
            return response.IsValid ? 0 : 1;
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

        private async Task CreateIndexIfItDoesNotExist(string indexName)
        {
            var indexExistsResponse = await this.Client.IndexExistsAsync(indexName);
            if (!indexExistsResponse.Exists)
            {
                var response = await this.Client.CreateIndexAsync(
                    indexName,
                    c => c.Settings(s => s
                        .NumberOfReplicas(1)
                        .NumberOfShards(10)
                        .Setting("merge.policy.merge_factor", "10")
                        .Setting("search.slowlog.threshold.fetch.warn", "1s")));
            }
        }
    }
}
