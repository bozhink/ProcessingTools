namespace ProcessingTools.History.Services.Data.Services
{
    using System;
    using System.Collections;
    using System.Threading.Tasks;
    using Contracts.Services;
    using Models;
    using Newtonsoft.Json;
    using ProcessingTools.History.Data.Common.Contracts.Repositories;

    public class HistoryDataService : IHistoryDataService
    {
        private readonly IHistoryRepository repository;

        public HistoryDataService(IHistoryRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        // TODO
        public Task<object> AddItemToHistory(object userId, object objectId, object item)
        {
            throw new NotImplementedException();
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

            var result = await this.repository.Add(model);
            await this.repository.SaveChanges();

            return result;
        }

        public Task<IEnumerable> Get(object objectId, Type objectType, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable> GetAll(object objectId, Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
