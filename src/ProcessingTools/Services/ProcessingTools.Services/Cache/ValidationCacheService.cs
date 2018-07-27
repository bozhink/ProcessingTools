// <copyright file="ValidationCacheService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Cache
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Contracts.Cache;
    using ProcessingTools.Models.Contracts.Cache;
    using ProcessingTools.Services.Contracts.Cache;
    using ProcessingTools.Services.Models.Cache;

    /// <summary>
    /// Validation cache service.
    /// </summary>
    public class ValidationCacheService : IValidationCacheService
    {
        private readonly IValidationCacheDataAccessObject dataAccessObject;
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationCacheService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        /// <param name="applicationContext">The application context.</param>
        public ValidationCacheService(IValidationCacheDataAccessObject dataAccessObject, IApplicationContext applicationContext)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IValidationCacheModel, ValidationCacheServiceModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<object> AddAsync(string key, IValidationCacheModel value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var entity = this.mapper.Map<IValidationCacheModel, ValidationCacheServiceModel>(value);
            entity.LastUpdate = this.applicationContext.DateTimeProvider.Invoke();

            var result = await this.dataAccessObject.AddAsync(key, entity).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<IValidationCacheModel> GetAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var item = await this.dataAccessObject.GetLastForKeyAsync(key).ConfigureAwait(false);
            return item;
        }
    }
}
