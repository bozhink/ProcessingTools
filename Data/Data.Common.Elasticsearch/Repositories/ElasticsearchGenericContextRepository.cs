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
    public class ElasticsearchGenericContextRepository<TEntity> : IElasticsearchGenericContextRepository<TEntity>
        where TEntity : class, IEntity
    {
        private IElasticClientProvider provider;

        public ElasticsearchGenericContextRepository(IElasticClientProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
            this.Client = this.provider.Create();
        }

        private IElasticClient Client { get; set; }

        public virtual async Task Add(string context, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await this.CreateIndexIfItDoesNotExist(context);
            var response = await this.Client.IndexAsync(entity, idx => idx.Index(context));
        }

        public virtual async Task<IQueryable<TEntity>> All(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            var countResponse = this.Client.Count<TEntity>(c => c.Index(context));
            var response = await this.Client.SearchAsync<TEntity>(e => e.From(0).Size((int)countResponse.Count));
            return response.Documents.AsQueryable();
        }

        public virtual async Task<IQueryable<TEntity>> All(string context, int skip, int take)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (skip < 0)
            {
                throw new ArgumentException("Skip should be non-negative.", nameof(skip));
            }

            if (take < 1)
            {
                throw new ArgumentException("Take should be greater than zero.", nameof(take));
            }

            var response = await this.Client.SearchAsync<TEntity>(e => e.From(skip).Size(take));
            return response.Documents.AsQueryable();
        }

        public virtual async Task Delete(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = await this.Client.DeleteIndexAsync(context);
        }

        public virtual async Task Delete(string context, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var response = await this.Client.DeleteAsync(new DeleteRequest<TEntity>(entity));
        }

        public virtual async Task Delete(string context, int id)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            var entity = await this.Get(context, id);
            await this.Delete(context, entity);
        }

        public virtual async Task<TEntity> Get(string context, int id)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = await this.Client.GetAsync<TEntity>(id, idx => idx.Index(context));
            return response.Source;
        }

        public virtual async Task<int> SaveChanges(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = await this.Client.FlushAsync(context);
            return response.IsValid ? 0 : 1;
        }

        public virtual async Task Update(string context, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var documentPath = new DocumentPath<TEntity>(new Id(entity.Id));
            var response = await this.Client.UpdateAsync<TEntity, TEntity>(
                documentPath,
                u => u.Doc(entity).DocAsUpsert(true));
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
