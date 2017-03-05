namespace ProcessingTools.History.Data.Entity.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Repositories;
    using Models;
    using ProcessingTools.Data.Common.Entity.Repositories;
    using ProcessingTools.History.Data.Common.Contracts.Models;

    public class EntityHistoryRepository : GenericRepository<IHistoryDbContext, HistoryItem>, IEntityHistoryRepository
    {
        public EntityHistoryRepository(IHistoryDbContext context)
            : base(context)
        {
        }

        public Task<object> Add(IHistoryItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var model = new HistoryItem
            {
                Data = entity.Data,
                DateModified = entity.DateModified,
                ObjectId = entity.ObjectId,
                ObjectType = entity.ObjectType,
                UserId = entity.UserId
            };

            base.Add(model);

            return Task.FromResult<object>(model.Id);
        }

        public new Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            base.Delete(id: id);

            return Task.FromResult(id);
        }

        public Task<IEnumerable<IHistoryItem>> Find(Expression<Func<IHistoryItem, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = this.DbSet.AsQueryable<IHistoryItem>().Where(filter);
            var result = query.AsEnumerable();

            return Task.FromResult(result);
        }

        public async Task<long> SaveChanges() => await this.Context.SaveChangesAsync();
    }
}
