namespace ProcessingTools.Data.Common.Elasticsearch.Repositories
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
    public class ElasticsearchGenericRepository<TEntity> : IElasticsearchGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        private IElasticClientProvider provider;
        private IElasticClient client;

        public ElasticsearchGenericRepository(IElasticClientProvider provider)
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
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
            }

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            await this.CreateIndexIfItDoesNotExist(context);
            var response = await this.Client.IndexAsync(entity, idx => idx.Index(context));
        }

        public async Task<IQueryable<TEntity>> All(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
            }

            var response = await this.Client.SearchAsync<TEntity>(e => e.From(0));
            return response.Documents.AsQueryable();
        }

        public async Task<IQueryable<TEntity>> All(string context, int skip, int take)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
            }

            if (skip < 0)
            {
                throw new ArgumentException("Skip should be non-negative.", "skip");
            }

            if (take < 1)
            {
                throw new ArgumentException("Take should be greater than zero.", "take");
            }

            var response = await this.Client.SearchAsync<TEntity>(e => e.From(skip).Size(take));
            return response.Documents.AsQueryable();
        }

        public async Task Delete(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
            }

            var response = await this.Client.DeleteIndexAsync(context);
        }

        public async Task Delete(string context, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
            }

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var response = await this.Client.DeleteAsync(new DeleteRequest<TEntity>(entity));
        }

        public async Task Delete(string context, int id)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
            }

            var entity = await this.Get(context, id);
            await this.Delete(context, entity);
        }

        public async Task<TEntity> Get(string context, int id)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
            }

            var response = await this.Client.GetAsync<TEntity>(id, idx => idx.Index(context));
            return response.Source;
        }

        public async Task<int> SaveChanges(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
            }

            var response = await this.Client.FlushAsync(context);
            return response.IsValid ? 0 : 1;
        }

        public Task Update(string context, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
            }

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            // TODO
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
