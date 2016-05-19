namespace ProcessingTools.Data.Common.Redis.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Extensions;
    using ProcessingTools.Data.Common.Models.Contracts;
    using ProcessingTools.Data.Common.Redis.Contracts;

    public class RedisGenericRepository<TEntity> : IRedisGenericRepository<TEntity>
        where TEntity : IEntity
    {
        private IRedisClientProvider provider;

        public RedisGenericRepository(IRedisClientProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
        }

        public virtual Task Add(string context, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Task.Run(() =>
            {
                using (var client = this.provider.Create())
                {
                    var list = client.Lists[context];

                    int id = list.GetMaximalId() + 1;
                    entity.Id = id;

                    list.AddEntity(entity);
                }
            });
        }

        public virtual Task<IQueryable<TEntity>> All(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.Run(() =>
            {
                using (var client = this.provider.Create())
                {
                    var list = client.Lists[context];
                    return list.Select(i => i.Deserialize<TEntity>()).AsQueryable();
                }
            });
        }

        public virtual Task<IQueryable<TEntity>> All(string context, int skip, int take)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (skip < 0)
            {
                throw new ArgumentException("Skip should be non-negative.", nameof(skip));
            }

            if (take < 1)
            {
                throw new ArgumentException("Take should be greater than zero.", nameof(take));
            }

            return Task.Run(() =>
            {
                using (var client = this.provider.Create())
                {
                    var list = client.Lists[context];
                    return list.OrderBy(i => i)
                        .Skip(skip)
                        .Take(take)
                        .Select(i => i.Deserialize<TEntity>())
                        .AsQueryable();
                }
            });
        }

        public virtual Task Delete(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.Run(() =>
            {
                using (var client = this.provider.Create())
                {
                    client.Remove(context);
                }
            });
        }

        public virtual Task Delete(string context, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Task.Run(() =>
            {
                using (var client = this.provider.Create())
                {
                    var list = client.Lists[context];
                    list.RemoveEntity(entity);
                }
            });
        }

        public virtual Task Delete(string context, int id)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.Run(() =>
            {
                using (var client = this.provider.Create())
                {
                    var list = client.Lists[context];
                    list.RemoveEntity<TEntity>(id);
                }
            });
        }

        public virtual Task<TEntity> Get(string context, int id)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.Run(() =>
            {
                using (var client = this.provider.Create())
                {
                    var list = client.Lists[context];
                    return list.Select(i => i.Deserialize<TEntity>())
                        .FirstOrDefault(i => i.Id == id);
                }
            });
        }

        public virtual Task Update(string context, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Task.Run(() =>
            {
                using (var client = this.provider.Create())
                {
                    var list = client.Lists[context];
                    list.RemoveEntity<TEntity>(entity.Id);
                    list.AddEntity(entity);
                }
            });
        }

        public virtual Task<int> SaveChanges(string context)
        {
            return Task.FromResult(0);
        }
    }
}
