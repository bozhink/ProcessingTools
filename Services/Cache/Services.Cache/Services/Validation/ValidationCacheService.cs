namespace ProcessingTools.Services.Cache.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts.Validation;
    using Models.Validation;
    using ProcessingTools.Cache.Data.Common.Contracts.Repositories;

    public class ValidationCacheService : IValidationCacheService
    {
        private readonly IValidationCacheDataRepository repository;
        private readonly IMapper mapper;

        public ValidationCacheService(IValidationCacheDataRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IValidationCacheServiceModel, ValidationCacheServiceModel>();
                c.CreateMap<ValidationCacheServiceModel, IValidationCacheServiceModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        public Task<object> Add(string key, IValidationCacheServiceModel value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return this.repository.Add(key, value);
        }

        public IEnumerable<IValidationCacheServiceModel> GetAll(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return this.repository.GetAll(key)
                .Select(v => this.mapper.Map<ValidationCacheServiceModel>(v));
        }

        public Task<object> Remove(string key, IValidationCacheServiceModel value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return this.repository.Remove(key, value);
        }
    }
}
