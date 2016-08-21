namespace ProcessingTools.Data.Common.File.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Data.Common.File.Contracts;

    public abstract class FileGenericRepository<TContext, ITaxonRankEntity> : FileCrudRepository<TContext, ITaxonRankEntity>, IFileGenericRepository<ITaxonRankEntity>
        where TContext : IFileDbContext<ITaxonRankEntity>
        where ITaxonRankEntity : class
    {
        public FileGenericRepository(IFileDbContextProvider<TContext, ITaxonRankEntity> contextProvider)
            : base(contextProvider)
        {
        }

        public virtual Task<long> Count() => Task.FromResult(this.Context.DataSet.LongCount());

        public virtual Task<long> Count(Expression<Func<ITaxonRankEntity, bool>> filter) => Task.FromResult(this.Context.DataSet.LongCount(filter));
    }
}
