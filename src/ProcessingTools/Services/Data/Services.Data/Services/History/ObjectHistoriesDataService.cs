namespace ProcessingTools.Services.Data.Services.History
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Data.Repositories.History;
    using ProcessingTools.Contracts.Models.History;
    using ProcessingTools.Contracts.Services.Data.History;
    using ProcessingTools.Services.Models.Data.History;

    public class ObjectHistoriesDataService : IObjectHistoriesDataService
    {
        private readonly IObjectHistoriesRepository repository;
        private readonly IEnvironment environment;

        public ObjectHistoriesDataService(IObjectHistoriesRepository repository, IEnvironment environment)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public async Task<object> AddAsync(object objectId, object source)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var settings = new JsonSerializerSettings
            {
                MaxDepth = 5,
                Formatting = Formatting.None
            };

            var objectType = source.GetType();
            var assemblyName = objectType.Assembly.GetName();
            string data = JsonConvert.SerializeObject(source, settings);
            var model = new ObjectHistory
            {
                Data = data,
                ObjectId = objectId.ToString(),
                ObjectType = objectType.FullName,
                AssemblyName = assemblyName.Name,
                AssemblyVersion = assemblyName.Version.ToString(),
                Id = this.environment.GuidProvider.Invoke().ToString(),
                CreatedBy = this.environment.User?.Id,
                CreatedOn = this.environment.DateTimeProvider.Invoke()
            };

            var result = await this.repository.AddAsync(model).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        public async Task<object[]> GetAsync(object objectId, Type objectType)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            if (objectType == null)
            {
                throw new ArgumentNullException(nameof(objectType));
            }

            string id = objectId.ToString();
            string typeName = objectType.FullName;

            var data = await this.repository.FindAsync(h => h.ObjectId == id && h.ObjectType == typeName).ConfigureAwait(false);

            var items = data
                .OrderBy(h => h.CreatedOn)
                .Select(h => JsonConvert.DeserializeObject(h.Data, objectType))
                .ToArray();

            return items;
        }

        public async Task<object[]> GetAsync(object objectId, Type objectType, int skip, int take)
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

            string id = objectId.ToString();
            string typeName = objectType.FullName;

            var data = await this.repository.FindAsync(h => h.ObjectId == id && h.ObjectType == typeName).ConfigureAwait(false);

            var items = data
                .OrderBy(h => h.CreatedOn)
                .Skip(skip)
                .Take(take)
                .Select(h => JsonConvert.DeserializeObject(h.Data, objectType))
                .ToArray();

            return items;
        }

        public async Task<IObjectHistory[]> GetHistoriesAsync(object objectId)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            string id = objectId.ToString();
            var data = await this.repository.FindAsync(h => h.ObjectId == id).ConfigureAwait(false);

            var items = data.OrderBy(h => h.CreatedBy)
                .Select(h => new ObjectHistory
                {
                    Id = h.Id,
                    Data = h.Data,
                    ObjectId = h.ObjectId,
                    ObjectType = h.ObjectType,
                    AssemblyName = h.AssemblyName,
                    AssemblyVersion = h.AssemblyVersion,
                    CreatedBy = h.CreatedBy,
                    CreatedOn = h.CreatedOn
                })
                .ToArray();

            return items;
        }

        public async Task<IObjectHistory[]> GetHistoriesAsync(object objectId, int skip, int take)
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

            var items = data.OrderBy(h => h.CreatedOn)
                .Skip(skip)
                .Take(take)
                .Select(h => new ObjectHistory
                {
                    Id = h.Id,
                    Data = h.Data,
                    ObjectId = h.ObjectId,
                    ObjectType = h.ObjectType,
                    AssemblyName = h.AssemblyName,
                    AssemblyVersion = h.AssemblyVersion,
                    CreatedBy = h.CreatedBy,
                    CreatedOn = h.CreatedOn
                })
                .ToArray();

            return items;
        }
    }
}
