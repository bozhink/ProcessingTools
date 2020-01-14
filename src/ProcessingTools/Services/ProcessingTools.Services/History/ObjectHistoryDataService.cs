// <copyright file="ObjectHistoryDataService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.History
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using ProcessingTools.Contracts.DataAccess.History;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.History;
    using ProcessingTools.Contracts.Services.History;
    using ProcessingTools.Services.Models.Data.History;

    /// <summary>
    /// Object history data service.
    /// </summary>
    public class ObjectHistoryDataService : IObjectHistoryDataService
    {
        private readonly IApplicationContext applicationContext;
        private readonly IObjectHistoryDataAccessObject dataAccessObject;

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
        public Task<object> AddAsync(object objectId, object source)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return this.AddInternalAsync(objectId, source);
        }

        /// <inheritdoc/>
        public Task<object[]> GetAsync(object objectId, Type objectType)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            if (objectType == null)
            {
                throw new ArgumentNullException(nameof(objectType));
            }

            return this.GetInternalAsync(objectId, objectType);
        }

        /// <inheritdoc/>
        public Task<object[]> GetAsync(object objectId, Type objectType, int skip, int take)
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

            return this.GetInternalAsync(objectId, objectType, skip, take);
        }

        /// <inheritdoc/>
        public Task<IObjectHistory[]> GetHistoriesAsync(object objectId)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            return this.GetHistoriesInternalAsync(objectId);
        }

        /// <inheritdoc/>
        public Task<IObjectHistory[]> GetHistoriesAsync(object objectId, int skip, int take)
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

            return this.GetHistoriesInternalAsync(objectId, skip, take);
        }

        private async Task<object> AddInternalAsync(object objectId, object source)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
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
                CreatedOn = this.applicationContext.DateTimeProvider.Invoke(),
            };

            var result = await this.dataAccessObject.AddAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        private async Task<IObjectHistory[]> GetHistoriesInternalAsync(object objectId)
        {
            var data = await this.dataAccessObject.GetAsync(objectId).ConfigureAwait(false);
            if (data == null || !data.Any())
            {
                return Array.Empty<IObjectHistory>();
            }

            var items = data.Select(this.ReMapObjectHistory()).ToArray();

            return items;
        }

        private async Task<IObjectHistory[]> GetHistoriesInternalAsync(object objectId, int skip, int take)
        {
            var data = await this.dataAccessObject.GetAsync(objectId, skip, take).ConfigureAwait(false);
            if (data == null || !data.Any())
            {
                return Array.Empty<IObjectHistory>();
            }

            var items = data.Select(this.ReMapObjectHistory()).ToArray();

            return items;
        }

        private async Task<object[]> GetInternalAsync(object objectId, Type objectType)
        {
            var data = await this.dataAccessObject.GetAsync(objectId).ConfigureAwait(false);
            if (data == null || !data.Any())
            {
                return Array.Empty<object>();
            }

            var items = data.Select(this.MapObjectHistoryToObject(objectType)).ToArray();

            return items;
        }

        private async Task<object[]> GetInternalAsync(object objectId, Type objectType, int skip, int take)
        {
            var data = await this.dataAccessObject.GetAsync(objectId, skip, take).ConfigureAwait(false);
            if (data == null || !data.Any())
            {
                return Array.Empty<object>();
            }

            var items = data.Select(this.MapObjectHistoryToObject(objectType)).ToArray();

            return items;
        }

        private Func<IObjectHistory, object> MapObjectHistoryToObject(Type objectType) => h => JsonConvert.DeserializeObject(h.Data, objectType);

        private Func<IObjectHistory, ObjectHistory> ReMapObjectHistory() => h => new ObjectHistory
        {
            Id = h.Id,
            Data = h.Data,
            ObjectId = h.ObjectId,
            ObjectType = h.ObjectType,
            AssemblyName = h.AssemblyName,
            AssemblyVersion = h.AssemblyVersion,
            CreatedBy = h.CreatedBy,
            CreatedOn = h.CreatedOn,
        };
    }
}
