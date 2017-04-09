namespace ProcessingTools.Data.Common.Redis.Abstractions.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts;
    using ProcessingTools.Contracts.Data.Repositories;
    using ServiceStack.Redis;

    public abstract class AbstractSavableRedisRepository : IKeyListableRepository<string>
    {
        private readonly IRedisClientProvider clientProvider;

        public AbstractSavableRedisRepository(IRedisClientProvider clientProvider)
        {
            if (clientProvider == null)
            {
                throw new ArgumentNullException(nameof(clientProvider));
            }

            this.clientProvider = clientProvider;
        }

        public IEnumerable<string> Keys
        {
            get
            {
                using (var client = this.ClientProvider.Create())
                {
                    return client.GetAllKeys();
                }
            }
        }

        protected IRedisClientProvider ClientProvider => this.clientProvider;

        public virtual Task<long> SaveChanges() => Task.Run(() =>
        {
            using (var client = this.ClientProvider.Create())
            {
                try
                {
                    client.SaveAsync();
                }
                catch (RedisResponseException)
                {
                    return 1L;
                }

                return 0L;
            }
        });
    }
}
