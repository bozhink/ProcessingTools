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
        where TEntity : class, IEntity
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
            this.Context = this.contextProvider.Create();

            this.clientProvider = clientProvider;
            this.Client = this.clientProvider.Create();
        }

        private IndexName Context { get; set; }

        private IElasticClient Client { get; set; }

        public async Task Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            await this.CreateIndexIfItDoesNotExist(this.Context);
            var response = await this.Client.IndexAsync(entity, idx => idx.Index(this.Context));
        }

        public async Task<IQueryable<TEntity>> All()
        {
            var countResponse = this.Client.Count<TEntity>(c => c.Index(this.Context));
            var response = await this.Client.SearchAsync<TEntity>(e => e.From(0).Size((int)countResponse.Count));
            return response.Documents.AsQueryable();
        }

        public async Task<IQueryable<TEntity>> All(int skip, int take)
        {
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

        public async Task Delete(object id)
        {
            var entity = await this.Get(id);
            await this.Delete(entity);
        }

        public async Task Delete(TEntity entity)
        {
            var response = await this.Client.DeleteAsync(new DeleteRequest<TEntity>(entity));
        }

        public async Task<TEntity> Get(object id)
        {
            var documentPath = new DocumentPath<TEntity>(new Id(id.ToString()));
            var response = await this.Client.GetAsync<TEntity>(documentPath, idx => idx.Index(this.Context));
            return response.Source;
        }

        public async Task<int> SaveChanges()
        {
            var response = await this.Client.FlushAsync(this.Context);
            return response.IsValid ? 0 : 1;
        }

        public async Task Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var documentPath = new DocumentPath<TEntity>(new Id(entity.Id));
            var response = await this.Client.UpdateAsync<TEntity, TEntity>(
                documentPath,
                u => u.Doc(entity).DocAsUpsert(true));
        }

        private async Task CreateIndexIfItDoesNotExist(IndexName indexName)
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
