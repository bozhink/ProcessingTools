// <copyright file="ObjectHistoryDataService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.History
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using ProcessingTools.Common.Resources;
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
            if (objectId is null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return this.AddInternalAsync(objectId, source);
        }

        /// <inheritdoc/>
        public Task<IList<object>> GetAsync(object objectId, Type objectType)
        {
            if (objectId is null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            if (objectType is null)
            {
                throw new ArgumentNullException(nameof(objectType));
            }

            return this.GetInternalAsync(objectId, objectType);
        }

        /// <inheritdoc/>
        public Task<IList<object>> GetAsync(object objectId, Type objectType, int skip, int take)
        {
            if (objectId is null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            if (objectType is null)
            {
                throw new ArgumentNullException(nameof(objectType));
            }

            if (skip < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(skip), skip, ExceptionMessages.ValueShouldBeNonNegative);
            }

            if (take < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(take), take, ExceptionMessages.ValueShouldBePositive);
            }

            return this.GetInternalAsync(objectId, objectType, skip, take);
        }

        /// <inheritdoc/>
        public Task<IList<IObjectHistory>> GetHistoriesAsync(object objectId)
        {
            if (objectId is null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            return this.GetHistoriesInternalAsync(objectId);
        }

        /// <inheritdoc/>
        public Task<IList<IObjectHistory>> GetHistoriesAsync(object objectId, int skip, int take)
        {
            if (objectId is null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            if (skip < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(skip), skip, ExceptionMessages.ValueShouldBeNonNegative);
            }

            if (take < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(take), take, ExceptionMessages.ValueShouldBePositive);
            }

            return this.GetHistoriesInternalAsync(objectId, skip, take);
        }

        private static Func<IObjectHistory, object> MapToObject(Type objectType)
        {
            return h => JsonConvert.DeserializeObject(h.Data, objectType);
        }

        private static Func<IObjectHistory, ObjectHistory> MapToObjectHistory()
        {
            return h => new ObjectHistory
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

        private async Task<IList<IObjectHistory>> GetHistoriesInternalAsync(object objectId)
        {
            var data = await this.dataAccessObject.GetAsync(objectId).ConfigureAwait(false);
            if (data is null || !data.Any())
            {
                return Array.Empty<IObjectHistory>();
            }

            var items = data.Select(MapToObjectHistory()).ToArray();

            return items;
        }

        private async Task<IList<IObjectHistory>> GetHistoriesInternalAsync(object objectId, int skip, int take)
        {
            var data = await this.dataAccessObject.GetAsync(objectId, skip, take).ConfigureAwait(false);
            if (data is null || !data.Any())
            {
                return Array.Empty<IObjectHistory>();
            }

            var items = data.Select(MapToObjectHistory()).ToArray();

            return items;
        }

        private async Task<IList<object>> GetInternalAsync(object objectId, Type objectType)
        {
            var data = await this.dataAccessObject.GetAsync(objectId).ConfigureAwait(false);
            if (data is null || !data.Any())
            {
                return Array.Empty<object>();
            }

            var items = data.Select(MapToObject(objectType)).ToArray();

            return items;
        }

        private async Task<IList<object>> GetInternalAsync(object objectId, Type objectType, int skip, int take)
        {
            var data = await this.dataAccessObject.GetAsync(objectId, skip, take).ConfigureAwait(false);
            if (data is null || !data.Any())
            {
                return Array.Empty<object>();
            }

            var items = data.Select(MapToObject(objectType)).ToArray();

            return items;
        }
    }
}
