namespace ProcessingTools.Data.Common.Elasticsearch.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;
    using Elasticsearch.Contracts;

    using Nest;

    using ProcessingTools.Data.Common.Models.Contracts;

    public class ElasticsearchGenericRepository<TEntity> : IElasticsearchGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IElasticContextProvider contextProvider;
        private readonly IElasticClientProvider clientProvider;

        public ElasticsearchGenericRepository(IElasticContextProvider contextProvider, IElasticClientProvider clientProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            if (clientProvider == null)
            {
                throw new ArgumentNullException(nameof(clientProvider));
            }

            this.contextProvider = contextProvider;
            this.Context = this.contextProvider.Create();

            this.clientProvider = clientProvider;
            this.Client = this.clientProvider.Create();
        }

        protected IndexName Context { get; set; }

        protected IElasticClient Client { get; set; }

        public virtual async Task<object> Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await this.CreateIndexIfItDoesNotExist(this.Context);
            var response = await this.Client.IndexAsync(entity, idx => idx.Index(this.Context));
            return response;
        }

        public virtual async Task<IQueryable<TEntity>> All()
        {
            var countResponse = this.Client.Count<TEntity>(c => c.Index(this.Context));
            var response = await this.Client.SearchAsync<TEntity>(e => e.From(0).Size((int)countResponse.Count));
            return response.Documents.AsQueryable();
        }

        // TODO
        public virtual async Task<IQueryable<TEntity>> All(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var countResponse = this.Client.Count<TEntity>(c => c.Index(this.Context));
            var response = await this.Client.SearchAsync<TEntity>(e => e.From(0).Size((int)countResponse.Count));
            return response.Documents.AsQueryable();
        }

        // TODO
        public virtual async Task<IQueryable<TEntity>> All(Expression<Func<TEntity, object>> sort, int skip, int take)
        {
            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            if (skip < 0)
            {
                throw new ArgumentException(string.Empty, nameof(skip));
            }

            if (take < 1)
            {
                throw new ArgumentException(string.Empty, nameof(take));
            }

            var response = await this.Client.SearchAsync<TEntity>(e => e.From(skip).Size(take));
            return response.Documents.AsQueryable();
        }

        // TODO
        public virtual async Task<IQueryable<TEntity>> All(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> sort, int skip, int take)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            if (skip < 0)
            {
                throw new ArgumentException(string.Empty, nameof(skip));
            }

            if (take < 1)
            {
                throw new ArgumentException(string.Empty, nameof(take));
            }

            var response = await this.Client.SearchAsync<TEntity>(e => e.From(skip).Size(take));
            return response.Documents.AsQueryable();
        }

        public virtual async Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await this.Get(id);
            return await this.Delete(entity);
        }

        public virtual async Task<object> Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var response = await this.Client.DeleteAsync(new DeleteRequest<TEntity>(entity));
            return response;
        }

        public virtual async Task<TEntity> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var documentPath = new DocumentPath<TEntity>(new Id(id.ToString()));
            var response = await this.Client.GetAsync<TEntity>(documentPath, idx => idx.Index(this.Context));
            return response.Source;
        }

        public virtual async Task<int> SaveChanges()
        {
            var response = await this.Client.FlushAsync(this.Context);
            return response.IsValid ? 0 : 1;
        }

        public virtual async Task<object> Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var documentPath = new DocumentPath<TEntity>(new Id(entity.Id));
            var response = await this.Client.UpdateAsync<TEntity, TEntity>(
                documentPath,
                u => u.Doc(entity).DocAsUpsert(true));

            return response;
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
