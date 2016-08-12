namespace ProcessingTools.Data.Common.Elasticsearch.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;
    using Elasticsearch.Contracts;

    using Nest;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Common.Models.Contracts;

    public class ElasticsearchGenericRepository<TEntity> : IElasticsearchGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IElasticClientProvider clientProvider;
        private readonly IElasticContextProvider contextProvider;

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

        protected IElasticClient Client { get; set; }

        protected IndexName Context { get; set; }

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

        public virtual Task<long> Count()
        {
            // TODO: Not implemented
            throw new NotImplementedException();
        }

        public virtual Task<long> Count(Expression<Func<TEntity, bool>> filter)
        {
            // TODO: Not implemented
            throw new NotImplementedException();
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

        public virtual Task<IQueryable<TEntity>> Find(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> sort,
            ProcessingTools.Common.Types.SortOrder sortOrder = ProcessingTools.Common.Types.SortOrder.Ascending,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect)
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
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > PagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            // TODO: Not implemented
            throw new NotImplementedException();
        }

        public virtual Task<IQueryable<T>> Find<T>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, T>> projection,
            Expression<Func<TEntity, object>> sort,
            ProcessingTools.Common.Types.SortOrder sortOrder = ProcessingTools.Common.Types.SortOrder.Ascending,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > PagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            // TODO: Not implemented
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> FindFirst(
            Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            // TODO: Not implemented
            throw new NotImplementedException();
        }

        public virtual Task<T> FindFirst<T>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, T>> projection)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            // TODO: Not implemented
            throw new NotImplementedException();
        }

        public virtual async Task<long> SaveChanges()
        {
            var response = await this.Client.FlushAsync(this.Context);
            return response.IsValid ? 0L : 1L;
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
