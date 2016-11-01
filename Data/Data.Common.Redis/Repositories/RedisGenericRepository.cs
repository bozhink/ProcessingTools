namespace ProcessingTools.Data.Common.Redis.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions.Linq;
    using ServiceStack.Redis;
    using ServiceStack.Text;

    public class RedisGenericRepository<TEntity> : IRedisGenericRepository<TEntity>
        where TEntity : IEntity
    {
        private readonly IRedisClientProvider provider;
        private readonly JsonStringSerializer serializer;

        public RedisGenericRepository(IRedisClientProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
            this.serializer = new JsonStringSerializer();
        }

        private Func<string, TEntity> Deserialize => s => this.serializer.DeserializeFromString<TEntity>(s);

        private Func<TEntity, string> Serialize => e => this.serializer.SerializeToString(e);

        public virtual async Task<object> Add(string context, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            using (var client = this.provider.Create())
            {
                var list = client.Lists[context];

                int id = this.GetMaximalId(list) + 1;
                entity.Id = id;

                await this.AddEntity(list, entity);
            }

            return entity;
        }

        public virtual IEnumerable<TEntity> All(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            using (var client = this.provider.Create())
            {
                var list = client.Lists[context];
                return list.Select(this.Deserialize);
            }
        }

        public virtual IEnumerable<TEntity> All(string context, int skip, int take)
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

            using (var client = this.provider.Create())
            {
                var list = client.Lists[context];
                return list.Skip(skip)
                    .Take(take)
                    .Select(this.Deserialize);
            }
        }

        public virtual Task<object> Delete(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.Run<object>(() =>
            {
                bool result = false;
                using (var client = this.provider.Create())
                {
                    result = client.Remove(context);
                }

                return result;
            });
        }

        public virtual async Task<object> Delete(string context, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            bool result = false;
            using (var client = this.provider.Create())
            {
                var list = client.Lists[context];
                result = await this.RemoveEntity(list, entity);
            }

            return result;
        }

        public virtual async Task<object> Delete(string context, object id)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            bool result = false;
            using (var client = this.provider.Create())
            {
                var list = client.Lists[context];
                result = await this.RemoveEntity(list, (int)id);
            }

            return result;
        }

        public virtual async Task<TEntity> Get(string context, object id)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            TEntity result;
            using (var client = this.provider.Create())
            {
                var list = client.Lists[context];
                result = await list.Select(this.Deserialize)
                    .FirstOrDefaultAsync(i => i.Id == (int)id);
            }

            return result;
        }

        public virtual Task<long> SaveChanges(string context) => Task.FromResult(0L);

        public virtual async Task<object> Update(string context, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            using (var client = this.provider.Create())
            {
                var list = client.Lists[context];
                await this.RemoveEntity(list, entity.Id);
                await this.AddEntity(list, entity);
            }

            return entity;
        }

        private Task AddEntity(IRedisList list, TEntity entity) => Task.Run(() => list.Add(this.Serialize(entity)));

        private int GetMaximalId(IRedisList list)
        {
            int maxId;

            if (list.Count > 0)
            {
                maxId = list.Max(i => this.Deserialize(i).Id);
            }
            else
            {
                maxId = 0;
            }

            return maxId;
        }

        private Task<bool> RemoveEntity(IRedisList list, TEntity entity) => Task.Run(() => list.Remove(this.Serialize(entity)));

        private async Task<bool> RemoveEntity(IRedisList list, int id)
        {
            var entityToBeRemoved = await list.Select(this.Deserialize)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (entityToBeRemoved == null)
            {
                throw new ApplicationException("Entity not found.");
            }

            return list.Remove(this.Serialize(entityToBeRemoved));
        }
    }
}
