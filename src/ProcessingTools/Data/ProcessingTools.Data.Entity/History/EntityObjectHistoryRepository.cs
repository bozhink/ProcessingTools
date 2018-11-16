namespace ProcessingTools.Data.Entity.History
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Contracts.Data.Expressions;
    using ProcessingTools.Data.Entity.Abstractions;
    using ProcessingTools.Data.Models.Entity.History;
    using ProcessingTools.Models.Contracts.History;

    public class EntityObjectHistoryRepository : EfRepository<HistoryDbContext, ObjectHistory>, IEntityObjectHistoriesRepository
    {
        public EntityObjectHistoryRepository(HistoryDbContext context)
            : base(context)
        {
        }

        public IQueryable<IObjectHistory> Query => this.DbSet.AsQueryable<IObjectHistory>();

        public Task<object> AddAsync(IObjectHistory entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var model = new ObjectHistory
            {
                Data = entity.Data,
                AssemblyName = entity.AssemblyName,
                AssemblyVersion = entity.AssemblyVersion,
                ObjectId = entity.ObjectId,
                ObjectType = entity.ObjectType,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                Id = entity.Id
            };

            this.Add(model);

            return Task.FromResult<object>(model.Id);
        }

        public Task<long> CountAsync() => this.DbSet.LongCountAsync();

        public Task<long> CountAsync(Expression<Func<IObjectHistory, bool>> filter) => this.DbSet.AsQueryable<IObjectHistory>().LongCountAsync(filter);

        public Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Delete(id: id);

            return Task.FromResult(id);
        }

        public Task<IObjectHistory[]> FindAsync(Expression<Func<IObjectHistory, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = this.DbSet.AsQueryable<IObjectHistory>().Where(filter);
            return query.ToArrayAsync();
        }

        public Task<IObjectHistory> FindFirstAsync(Expression<Func<IObjectHistory, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = this.DbSet.AsQueryable<IObjectHistory>().Where(filter);
            return query.FirstOrDefaultAsync();
        }

        public Task<IObjectHistory> GetByIdAsync(object id)
        {
            var entity = this.DbSet.Find(id);
            return Task.FromResult<IObjectHistory>(entity);
        }

        public Task<object> UpdateAsync(IObjectHistory entity)
        {
            throw new NotSupportedException();
        }

        public Task<object> UpdateAsync(object id, IUpdateExpression<IObjectHistory> updateExpression)
        {
            throw new NotSupportedException();
        }
    }
}
