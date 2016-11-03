namespace ProcessingTools.Services.Cache.Validation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts.Validation;
    using Models.Validation;
    using ProcessingTools.Cache.Data.Common.Contracts.Models;
    using ProcessingTools.Cache.Data.Common.Contracts.Repositories;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions.Linq;

    public class ValidationCacheService : IValidationCacheService
    {
        private readonly IValidationCacheDataRepository repository;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IMapper mapper;

        public ValidationCacheService(IValidationCacheDataRepository repository, IDateTimeProvider dateTimeProvider)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException(nameof(dateTimeProvider));
            }

            this.repository = repository;
            this.dateTimeProvider = dateTimeProvider;

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IValidationCacheServiceModel, ValidationCacheServiceModel>();
                c.CreateMap<ValidationCacheServiceModel, IValidationCacheServiceModel>();

                c.CreateMap<IValidationCacheEntity, ValidationCacheServiceModel>();
                c.CreateMap<ValidationCacheServiceModel, IValidationCacheEntity>();
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

            var entity = this.mapper.Map<ValidationCacheServiceModel>(value);
            entity.LastUpdate = this.dateTimeProvider.Now;

            return this.repository.Add(key, entity);
        }

        public async Task<IValidationCacheServiceModel> Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var query = this.repository.GetAll(key);
            if (query == null)
            {
                return null;
            }

            var entity = await query.OrderByDescending(e => e.LastUpdate)
                .Select(v => this.mapper.Map<ValidationCacheServiceModel>(v))
                .FirstOrDefaultAsync();

            return entity;
        }
    }
}
