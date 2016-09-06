namespace ProcessingTools.Data.Common.File.Repositories
{
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Data.Common.Expressions;
    using ProcessingTools.Data.Common.Expressions.Contracts;
    using ProcessingTools.Data.Common.File.Contracts;

    public abstract class FileCrudRepository<TContext, ITaxonRankEntity> : FileSearchableRepository<TContext, ITaxonRankEntity>, IFileCrudRepository<ITaxonRankEntity>
        where TContext : IFileDbContext<ITaxonRankEntity>
        where ITaxonRankEntity : class
    {
        public FileCrudRepository(IFileDbContextProvider<TContext, ITaxonRankEntity> contextProvider)
            : base(contextProvider)
        {
        }

        public virtual Task<object> Add(ITaxonRankEntity entity)
        {
            DummyValidator.ValidateEntity(entity);
            return this.Context.Add(entity);
        }

        public virtual Task<object> Delete(object id)
        {
            DummyValidator.ValidateId(id);
            return this.Context.Delete(id);
        }

        public abstract Task<long> SaveChanges();

        public virtual Task<object> Update(ITaxonRankEntity entity)
        {
            DummyValidator.ValidateEntity(entity);
            return this.Context.Update(entity);
        }

        public virtual async Task<object> Update(object id, IUpdateExpression<ITaxonRankEntity> update)
        {
            DummyValidator.ValidateId(id);
            DummyValidator.ValidateUpdate(update);

            var entity = await this.Get(id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            // TODO : Updater
            var updater = new Updater<ITaxonRankEntity>(update);
            await updater.Invoke(entity);

            return await this.Context.Update(entity);
        }
    }
}
