// <copyright file="ValidationCacheService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Cache
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Contracts.Repositories.Cache;
    using ProcessingTools.Models.Contracts.Cache;
    using ProcessingTools.Services.Contracts.Cache;
    using ProcessingTools.Services.Models.Cache;
    using ProcessingTools.Contracts.Services;

    /// <summary>
    /// Validation cache service.
    /// </summary>
    public class ValidationCacheService : IValidationCacheService
    {
        private readonly IValidationCacheDataRepository repository;
        private readonly IEnvironment environment;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationCacheService"/> class.
        /// </summary>
        /// <param name="repository">Data repository.</param>
        /// <param name="environment">Environment service.</param>
        public ValidationCacheService(IValidationCacheDataRepository repository, IEnvironment environment)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.environment = environment ?? throw new ArgumentNullException(nameof(environment));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IValidationCacheModel, ValidationCacheServiceModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public Task<object> AddAsync(string key, IValidationCacheModel value)
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
            entity.LastUpdate = this.environment.DateTimeProvider.Invoke();

            return this.repository.AddAsync(key, entity);
        }

        /// <inheritdoc/>
        public Task<IValidationCacheModel> GetAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var data = this.repository.GetAll(key);
            if (data == null)
            {
                return null;
            }

            var entity = data.OrderByDescending(e => e.LastUpdate).FirstOrDefault();

            return Task.FromResult(entity);
        }
    }
}
