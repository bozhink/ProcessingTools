namespace ProcessingTools.Cache.Data.Redis.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Cache.Data.Redis.Contracts.Repositories;
    using ProcessingTools.Cache.Data.Redis.Models;
    using ProcessingTools.Models.Contracts.Cache;
    using ProcessingTools.Data.Common.Redis.Contracts;
    using ProcessingTools.Data.Common.Redis.Repositories;

    public class RedisValidationCacheDataRepository : RedisKeyCollectionValuePairsRepository<IValidationCacheModel>, IRedisValidationCacheDataRepository
    {
        public RedisValidationCacheDataRepository(IRedisClientProvider provider)
            : base(provider)
        {
        }

        private Func<IValidationCacheModel, ValidationCacheEntity> MapToEntity => e => new ValidationCacheEntity
        {
            Content = e.Content,
            LastUpdate = e.LastUpdate,
            Status = e.Status
        };

        public override Task<object> AddAsync(string key, IValidationCacheModel value)
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

            return base.AddAsync(key, entity);
        }

        public override Task<object> RemoveAsync(string key, IValidationCacheModel value)
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

            return base.RemoveAsync(key, entity);
        }
    }
}
