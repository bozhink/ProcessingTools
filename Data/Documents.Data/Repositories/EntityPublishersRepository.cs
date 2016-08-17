namespace ProcessingTools.Documents.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Documents.Data.Common.Models.Contracts;

    public class EntityPublishersRepository : IEntityPublishersRepository
    {
        public Task<object> Add(IPublisherEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<object> AddAddress(object entityId, IAddressEntity address)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<IPublisherEntity>> All()
        {
            throw new NotImplementedException();
        }

        public Task<long> Count()
        {
            throw new NotImplementedException();
        }

        public Task<long> Count(Expression<Func<IPublisherEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<object> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task<object> Delete(IPublisherEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IPublisherEntity> Get(object id)
        {
            throw new NotImplementedException();
        }

        public Task<object> RemoveAddress(object entityId, object addressId)
        {
            throw new NotImplementedException();
        }

        public Task<long> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<object> Update(IPublisherEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
