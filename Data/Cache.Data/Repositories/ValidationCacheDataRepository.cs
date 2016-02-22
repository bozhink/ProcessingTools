namespace ProcessingTools.Cache.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models.Contracts;

    public class ValidationCacheDataRepository : IValidationCacheDataRepository
    {
        public Task Add(string context, IValidationCacheEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<IValidationCacheEntity>> All(string context)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string context)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string context, IValidationCacheEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string context, int id)
        {
            throw new NotImplementedException();
        }

        public Task<IValidationCacheEntity> Get(string context, int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(string context, IValidationCacheEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
