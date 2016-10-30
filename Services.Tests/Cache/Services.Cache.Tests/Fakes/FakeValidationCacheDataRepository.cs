namespace ProcessingTools.Services.Cache.Tests.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ProcessingTools.Cache.Data.Models;
    using ProcessingTools.Cache.Data.Repositories.Contracts;

    public class FakeValidationCacheDataRepository : IValidationCacheDataRepository
    {
        private IDictionary<string, HashSet<ValidationCacheEntity>> data;

        public FakeValidationCacheDataRepository()
        {
            this.data = new Dictionary<string, HashSet<ValidationCacheEntity>>();
        }

        public Task<object> Add(string context, ValidationCacheEntity entity)
        {
            return Task.Run<object>(() =>
            {
                if (!this.data.ContainsKey(context))
                {
                    this.data.Add(context, new HashSet<ValidationCacheEntity>());
                }

                int maxId = 0;

                try
                {
                    maxId = this.data[context].Max(i => i.Id);
                }
                catch
                {
                }

                entity.Id = maxId + 1;
                this.data[context].Add(entity);

                return entity;
            });
        }

        public IEnumerable<ValidationCacheEntity> All(string context)
        {
            if (!this.data.ContainsKey(context))
            {
                this.data.Add(context, new HashSet<ValidationCacheEntity>());
            }

            return this.data[context];
        }

        public IEnumerable<ValidationCacheEntity> All(string context, int skip, int take)
        {
            if (!this.data.ContainsKey(context))
            {
                this.data.Add(context, new HashSet<ValidationCacheEntity>());
            }

            return this.data[context].OrderBy(i => i.Id).Skip(skip).Take(take);
        }

        public Task<object> Delete(string context)
        {
            return Task.Run<object>(() =>
            {
                if (this.data.ContainsKey(context))
                {
                    this.data.Remove(context);
                }

                return true;
            });
        }

        public Task<object> Delete(string context, ValidationCacheEntity entity)
        {
            return Task.Run<object>(() =>
            {
                if (!this.data.ContainsKey(context))
                {
                    this.data.Add(context, new HashSet<ValidationCacheEntity>());
                }

                this.data[context].RemoveWhere(i => i.Id == entity.Id && i.LastUpdate == entity.LastUpdate && i.Status == entity.Status && i.Content == entity.Content);

                return true;
            });
        }

        public Task<object> Delete(string context, object id)
        {
            return Task.Run<object>(() =>
            {
                if (!this.data.ContainsKey(context))
                {
                    this.data.Add(context, new HashSet<ValidationCacheEntity>());
                }

                this.data[context].RemoveWhere(i => i.Id == (int)id);

                return true;
            });
        }

        public Task<ValidationCacheEntity> Get(string context, object id)
        {
            return Task.Run(() =>
            {
                if (!this.data.ContainsKey(context))
                {
                    this.data.Add(context, new HashSet<ValidationCacheEntity>());
                }

                return this.data[context].FirstOrDefault(i => i.Id == (int)id);
            });
        }

        public Task<object> Update(string context, ValidationCacheEntity entity)
        {
            return Task.Run<object>(() =>
            {
                if (!this.data.ContainsKey(context))
                {
                    this.data.Add(context, new HashSet<ValidationCacheEntity>());
                }

                this.data[context].Where(i => i.Id == entity.Id)
                    .ToList()
                    .ForEach(i =>
                    {
                        i.Content = entity.Content;
                        i.LastUpdate = entity.LastUpdate;
                        i.Status = entity.Status;
                    });

                return true;
            });
        }

        public Task<long> SaveChanges(string context)
        {
            return Task.FromResult(0L);
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