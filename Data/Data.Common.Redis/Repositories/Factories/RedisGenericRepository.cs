namespace ProcessingTools.Data.Common.Redis.Repositories.Factories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Models.Contracts;

    public class RedisGenericRepository<TEntity> : IRedisGenericRepository<TEntity>
        where TEntity : IRedisEntity
    {
        public Task Add(string context, TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> All(string context)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string context)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string context, TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string context, int id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Get(string context, int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(string context, TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
