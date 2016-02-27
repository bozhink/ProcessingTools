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

        public Task Add(string context, ValidationCacheEntity entity)
        {
            return Task.Run(() =>
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
            });
        }

        public Task<IQueryable<ValidationCacheEntity>> All(string context)
        {
            return Task.Run(() =>
            {
                if (!this.data.ContainsKey(context))
                {
                    this.data.Add(context, new HashSet<ValidationCacheEntity>());
                }

                return this.data[context].AsQueryable();
            });
        }

        public Task Delete(string context)
        {
            return Task.Run(() =>
            {
                if (this.data.ContainsKey(context))
                {
                    this.data.Remove(context);
                }
            });
        }

        public Task Delete(string context, ValidationCacheEntity entity)
        {
            return Task.Run(() =>
            {
                if (!this.data.ContainsKey(context))
                {
                    this.data.Add(context, new HashSet<ValidationCacheEntity>());
                }

                this.data[context].RemoveWhere(i => i.Id == entity.Id && i.LastUpdate == entity.LastUpdate && i.Status == entity.Status && i.Content == entity.Content);
            });
        }

        public Task Delete(string context, int id)
        {
            return Task.Run(() =>
            {
                if (!this.data.ContainsKey(context))
                {
                    this.data.Add(context, new HashSet<ValidationCacheEntity>());
                }

                this.data[context].RemoveWhere(i => i.Id == id);
            });
        }

        public Task<ValidationCacheEntity> Get(string context, int id)
        {
            return Task.Run(() =>
            {
                if (!this.data.ContainsKey(context))
                {
                    this.data.Add(context, new HashSet<ValidationCacheEntity>());
                }

                return this.data[context].FirstOrDefault(i => i.Id == id);
            });
        }

        public Task Update(string context, ValidationCacheEntity entity)
        {
            return Task.Run(() =>
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
            });
        }
    }
}