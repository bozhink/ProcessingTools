// <copyright file="ObjectHistoryDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.History
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Contracts.History;
    using ProcessingTools.Models.Contracts.History;
    using ProcessingTools.Services.Contracts.History;
    using ProcessingTools.Services.Models.Data.History;

    /// <summary>
    /// Object history data service.
    /// </summary>
    public class ObjectHistoryDataService : IObjectHistoryDataService
    {
        private readonly IObjectHistoryDataAccessObject dataAccessObject;
        private readonly IApplicationContext applicationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectHistoryDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Instance of <see cref="IObjectHistoryDataAccessObject"/>.</param>
        /// <param name="applicationContext">The application context.</param>
        public ObjectHistoryDataService(IObjectHistoryDataAccessObject dataAccessObject, IApplicationContext applicationContext)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        /// <inheritdoc/>
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
                CreatedBy = this.applicationContext.UserContext?.UserId,
                CreatedOn = this.applicationContext.DateTimeProvider.Invoke()
            };

            var result = await this.dataAccessObject.AddAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
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

            var data = await this.dataAccessObject.GetAsync(objectId).ConfigureAwait(false);
            if (data == null || !data.Any())
            {
                return new object[] { };
            }

            var items = data.Select(this.MapObjectHistoryToObject(objectType)).ToArray();

            return items;
        }

        /// <inheritdoc/>
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

            var data = await this.dataAccessObject.GetAsync(objectId, skip, take).ConfigureAwait(false);
            if (data == null || !data.Any())
            {
                return new object[] { };
            }

            var items = data.Select(this.MapObjectHistoryToObject(objectType)).ToArray();

            return items;
        }

        /// <inheritdoc/>
        public async Task<IObjectHistory[]> GetHistoriesAsync(object objectId)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            var data = await this.dataAccessObject.GetAsync(objectId).ConfigureAwait(false);
            if (data == null || !data.Any())
            {
                return new IObjectHistory[] { };
            }

            var items = data.Select(this.ReMapObjectHistory()).ToArray();

            return items;
        }

        /// <inheritdoc/>
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

            var data = await this.dataAccessObject.GetAsync(objectId, skip, take).ConfigureAwait(false);
            if (data == null || !data.Any())
            {
                return new IObjectHistory[] { };
            }

            var items = data.Select(this.ReMapObjectHistory()).ToArray();

            return items;
        }

        private Func<IObjectHistory, ObjectHistory> ReMapObjectHistory() => h => new ObjectHistory
        {
            Id = h.Id,
            Data = h.Data,
            ObjectId = h.ObjectId,
            ObjectType = h.ObjectType,
            AssemblyName = h.AssemblyName,
            AssemblyVersion = h.AssemblyVersion,
            CreatedBy = h.CreatedBy,
            CreatedOn = h.CreatedOn
        };

        private Func<IObjectHistory, object> MapObjectHistoryToObject(Type objectType) => h => JsonConvert.DeserializeObject(h.Data, objectType);
    }
}
