namespace ProcessingTools.History.Services.Data.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Contracts.Repositories.History;
    using ProcessingTools.History.Services.Data.Contracts.Services;
    using ProcessingTools.History.Services.Data.Models;
    using ProcessingTools.Models.Contracts.History;

    public class HistoryDataService : IHistoryDataService
    {
        private readonly IHistoryRepository repository;
        private readonly IDateTimeProvider dateTimeProvider;

        public HistoryDataService(IHistoryRepository repository, IDateTimeProvider dateTimeProvider)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public Task<object> AddItemToHistory(object userId, object objectId, object item)
        {
            return this.AddItemToHistory(userId, objectId, this.dateTimeProvider.Now, item);
        }

        public async Task<object> AddItemToHistory(object userId, object objectId, DateTime dateModified, object item)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            string data = JsonConvert.SerializeObject(
                item,
                new JsonSerializerSettings
                {
                    MaxDepth = 5,
                    Formatting = Formatting.None
                });

            var model = new HistoryItem
            {
                Data = data,
                DateModified = dateModified,
                ObjectId = objectId.ToString(),
                ObjectType = item.GetType().FullName,
                UserId = userId.ToString()
            };

            var result = await this.repository.AddAsync(model).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        public async Task<IEnumerable> Get(object objectId, Type objectType, int skip, int take)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            if (objectType == null)
            {
                throw new ArgumentNullException(nameof(objectType));
            }

            if (skip < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(skip), skip, "Value should be non-negative");
            }

            if (take < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(take), take, "Value should be positive");
            }

            var data = await this.GetQueryAsync(objectId, objectType).ConfigureAwait(false);

            var items = data.Select(h => JsonConvert.DeserializeObject(h.Data, objectType))
                .Skip(skip)
                .Take(take)
                .ToArray();

            return items;
        }

        public async Task<IEnumerable<IHistoryItem>> Get(object objectId, int skip, int take)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            if (skip < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(skip), skip, "Value should be non-negative");
            }

            if (take < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(take), take, "Value should be positive");
            }

            string id = objectId.ToString();
            var data = await this.repository.FindAsync(h => h.ObjectId == id).ConfigureAwait(false);

            var items = data.OrderBy(h => h.DateModified)
                .Skip(skip)
                .Take(take)
                .Select(h => new HistoryItem
                {
                    Id = h.Id,
                    Data = h.Data,
                    ObjectId = h.ObjectId,
                    ObjectType = h.ObjectType,
                    DateModified = h.DateModified,
                    UserId = h.UserId
                })
                .ToArray();

            return items;
        }

        public async Task<IEnumerable> GetAll(object objectId, Type objectType)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            if (objectType == null)
            {
                throw new ArgumentNullException(nameof(objectType));
            }

            var data = await this.GetQueryAsync(objectId, objectType).ConfigureAwait(false);

            var items = data.Select(h => JsonConvert.DeserializeObject(h.Data, objectType)).ToArray();

            return items;
        }

        public async Task<IEnumerable<IHistoryItem>> GetAll(object objectId)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            string id = objectId.ToString();
            var data = await this.repository.FindAsync(h => h.ObjectId == id).ConfigureAwait(false);

            var items = data.OrderBy(h => h.DateModified)
                .Select(h => new HistoryItem
                {
                    Id = h.Id,
                    Data = h.Data,
                    ObjectId = h.ObjectId,
                    ObjectType = h.ObjectType,
                    DateModified = h.DateModified,
                    UserId = h.UserId
                })
                .ToArray();

            return items;
        }

        private async Task<IEnumerable<IHistoryItem>> GetQueryAsync(object objectId, Type objectType)
        {
            string id = objectId.ToString();
            string typeName = objectType.FullName;

            var query = await this.repository.FindAsync(h => h.ObjectId == id && h.ObjectType == typeName).ConfigureAwait(false);
            return query.OrderBy(h => h.DateModified);
        }
    }
}
