namespace ProcessingTools.Cache.Data.Redis.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Repositories;
    using Models;
    using ProcessingTools.Cache.Data.Common.Contracts.Models;
    using ProcessingTools.Data.Common.Redis.Contracts;
    using ProcessingTools.Data.Common.Redis.Repositories;

    public class RedisValidationCacheDataRepository : RedisKeyCollectionValuePairsRepository<IValidationCacheEntity>, IRedisValidationCacheDataRepository
    {
        public RedisValidationCacheDataRepository(IRedisClientProvider provider)
            : base(provider)
        {
        }

        private Func<IValidationCacheEntity, ValidationCacheEntity> MapToEntity => e => new ValidationCacheEntity
        {
            Content = e.Content,
            LastUpdate = e.LastUpdate,
            Status = e.Status
        };

        public override Task<object> Add(string key, IValidationCacheEntity value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var entity = this.MapToEntity(value);

            return base.Add(key, entity);
        }

        public override Task<object> Remove(string key, IValidationCacheEntity value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var entity = this.MapToEntity(value);

            return base.Remove(key, entity);
        }
    }
}
