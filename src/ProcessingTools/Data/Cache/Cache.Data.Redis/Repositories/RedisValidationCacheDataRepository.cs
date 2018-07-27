namespace ProcessingTools.Cache.Data.Redis.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Common.Redis.Contracts;
    using ProcessingTools.Data.Common.Redis.Repositories;
    using ProcessingTools.Data.Contracts.Cache;
    using ProcessingTools.Data.Models.Cache.Redis;
    using ProcessingTools.Models.Contracts.Cache;

    public class RedisValidationCacheDataRepository : RedisKeyCollectionValuePairsRepository<IValidationCacheModel>, IValidationCacheDataRepository
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
