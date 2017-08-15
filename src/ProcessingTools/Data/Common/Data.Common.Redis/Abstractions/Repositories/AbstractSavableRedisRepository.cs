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

        protected AbstractSavableRedisRepository(IRedisClientProvider clientProvider)
        {
            this.clientProvider = clientProvider ?? throw new ArgumentNullException(nameof(clientProvider));
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

        public virtual object SaveChanges()
        {
            using (var client = this.ClientProvider.Create())
            {
                try
                {
                    client.SaveAsync();
                }
                catch (RedisResponseException)
                {
                    return 1;
                }

                return 0;
            }
        }

        public virtual Task<object> SaveChangesAsync() => Task.Run(() => this.SaveChanges());
    }
}
