namespace ProcessingTools.Data.Common.Redis.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Exceptions;

    public class RedisKeyValuePairsRepository<T> : IRedisKeyValuePairsRepository<T>
    {
        private readonly IRedisClientProvider provider;

        public RedisKeyValuePairsRepository(IRedisClientProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
        }

        public Task<object> Add(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Task.Run<object>(() =>
            {
                using (var client = this.provider.Create())
                {
                    if (client.ContainsKey(key))
                    {
                        throw new KeyExistsException();
                    }

                    return client.Add(key, value);
                }
            });
        }

        public Task<T> Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Task.Run(() =>
            {
                using (var client = this.provider.Create())
                {
                    if (!client.ContainsKey(key))
                    {
                        throw new KeyNotFoundException();
                    }

                    return client.Get<T>(key);
                }
            });
        }

        public Task<object> Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Task.Run<object>(() =>
            {
                using (var client = this.provider.Create())
                {
                    return client.Remove(key);
                }
            });
        }

        public Task<long> SaveChanges() => Task.Run(() =>
        {
            using (var client = this.provider.Create())
            {
                client.SaveAsync();
                return 0L;
            }
        });

        public Task<object> Update(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Task.Run<object>(() =>
            {
                using (var client = this.provider.Create())
                {
                    if (!client.ContainsKey(key))
                    {
                        throw new KeyNotFoundException();
                    }

                    return client.Replace(key, value);
                }
            });
        }

        public Task<object> Upsert(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Task.Run<object>(() =>
            {
                using (var client = this.provider.Create())
                {
                    return client.Set(key, value);
                }
            });
        }
    }
}
