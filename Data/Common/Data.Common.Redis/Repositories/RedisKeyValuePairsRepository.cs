namespace ProcessingTools.Data.Common.Redis.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Abstracts.Repositories;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Exceptions;

    public class RedisKeyValuePairsRepository<T> : AbstractSavableRedisRepository, IRedisKeyValuePairsRepository<T>
    {
        public RedisKeyValuePairsRepository(IRedisClientProvider provider)
            : base(provider)
        {
        }

        public virtual Task<object> Add(string key, T value)
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

        public virtual Task<T> Get(string key)
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

        public virtual Task<object> Remove(string key)
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

        public virtual Task<object> Update(string key, T value)
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

        public virtual Task<object> Upsert(string key, T value)
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
