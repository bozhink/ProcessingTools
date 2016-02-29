namespace ProcessingTools.Data.Common.Redis.Repositories.Factories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Extensions;
    using ProcessingTools.Data.Common.Models.Contracts;
    using Redis.Contracts;

    public abstract class RedisRepositoryFactory<TEntity> : IRedisGenericRepository<TEntity>
        where TEntity : IEntity
    {
        private IRedisClientProvider provider;

        public RedisRepositoryFactory(IRedisClientProvider provider)
        {
            this.provider = provider;
        }

        public Task Add(string context, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
            }

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
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

        public Task<IQueryable<TEntity>> All(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
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

        public Task<IQueryable<TEntity>> All(string context, int skip, int take)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
            }

            if (skip < 0)
            {
                throw new ArgumentException("Skip should be non-negative.", "skip");
            }

            if (take < 1)
            {
                throw new ArgumentException("Take should be greater than zero.", "take");
            }

            return Task.Run(() =>
            {
                using (var client = this.provider.Create())
                {
                    var list = client.Lists[context];
                    return list.OrderBy(i => i).Skip(skip).Take(take).Select(i => i.Deserialize<TEntity>()).AsQueryable();
                }
            });
        }

        public Task Delete(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
            }

            return Task.Run(() =>
            {
                using (var client = this.provider.Create())
                {
                    client.Remove(context);
                }
            });
        }

        public Task Delete(string context, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
            }

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
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

        public Task Delete(string context, int id)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
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

        public Task<TEntity> Get(string context, int id)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
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

        public Task Update(string context, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException("context");
            }

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
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

        public Task<int> SaveChanges(string context)
        {
            return Task.Run(() =>
            {
                return 0;
            });
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // There is nothing to be disposed.
            }
        }
    }
}
