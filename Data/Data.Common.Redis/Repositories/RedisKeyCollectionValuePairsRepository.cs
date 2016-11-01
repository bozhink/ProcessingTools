namespace ProcessingTools.Data.Common.Redis.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Repositories;
    using ServiceStack.Redis;
    using ServiceStack.Text;

    public class RedisKeyCollectionValuePairsRepository<T> : IRedisKeyCollectionValuePairsRepository<T>
    {
        private readonly IRedisClientProvider provider;
        private readonly IStringSerializer serializer;

        public RedisKeyCollectionValuePairsRepository(IRedisClientProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
            this.serializer = new JsonStringSerializer();
        }

        private Func<string, T> Deserialize => s => this.serializer.DeserializeFromString<T>(s);

        private Func<T, string> Serialize => e => this.serializer.SerializeToString(e);

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
                using (var client = this.provider.Create())
                {
                    var list = client.Lists[key];
                    this.AddValueToList(list, value);

                    return true;
                }
            });
        }

        public virtual IEnumerable<T> GetAll(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            using (var client = this.provider.Create())
            {
                var list = client.Lists[key];
                return list.Select(this.Deserialize);
            }
        }

        public virtual Task<object> Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Task.Run<object>(() =>
            {
                using (var client = this.provider.Create())
                {
                    if (!client.ContainsKey(key))
                    {
                        return true;
                    }

                    return client.Remove(key);
                }
            });
        }

        public virtual Task<object> Remove(string key, T value)
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
                    var list = client.Lists[key];
                    var result = this.RemoveValueFromList(list, value);

                    return result;
                }
            });
        }

        public virtual Task<long> SaveChanges() => Task.Run(() =>
        {
            using (var client = this.provider.Create())
            {
                client.SaveAsync();
                return 0L;
            }
        });

        private void AddValueToList(IRedisList list, T value) => list.Add(this.Serialize(value));

        private bool RemoveValueFromList(IRedisList list, T value) => list.Remove(this.Serialize(value));
    }
}
