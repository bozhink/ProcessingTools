namespace ProcessingTools.Data.Common.Redis.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Common.Redis.Abstractions;
    using ProcessingTools.Data.Common.Redis.Contracts;

    public class RedisKeyValuePairsRepository<T> : AbstractSavableRedisRepository, IRedisKeyValuePairsRepository<T>
    {
        public RedisKeyValuePairsRepository(IRedisClientProvider provider)
            : base(provider)
        {
        }

        public virtual Task<object> AddAsync(string key, T value)
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
                using (var client = this.ClientProvider.Create())
                {
                    if (client.ContainsKey(key))
                    {
                        throw new KeyExistsException();
                    }

                    return client.Add(key, value);
                }
            });
        }

        public virtual Task<T> GetAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Task.Run(() =>
            {
                using (var client = this.ClientProvider.Create())
                {
                    if (!client.ContainsKey(key))
                    {
                        throw new KeyNotFoundException();
                    }

                    return client.Get<T>(key);
                }
            });
        }

        public virtual Task<object> RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Task.Run<object>(() =>
            {
                using (var client = this.ClientProvider.Create())
                {
                    if (!client.ContainsKey(key))
                    {
                        return true;
                    }

                    return client.Remove(key);
                }
            });
        }

        public virtual Task<object> UpdateAsync(string key, T value)
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
                using (var client = this.ClientProvider.Create())
                {
                    if (!client.ContainsKey(key))
                    {
                        throw new KeyNotFoundException();
                    }

                    return client.Replace(key, value);
                }
            });
        }

        public virtual Task<object> UpsertAsync(string key, T value)
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
                using (var client = this.ClientProvider.Create())
                {
                    return client.Set(key, value);
                }
            });
        }
    }
}
