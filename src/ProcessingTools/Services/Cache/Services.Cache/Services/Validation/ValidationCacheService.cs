namespace ProcessingTools.Services.Cache.Services.Validation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Cache.Repositories;
    using ProcessingTools.Models.Contracts.Cache;
    using ProcessingTools.Services.Cache.Contracts.Models.Validation;
    using ProcessingTools.Services.Cache.Contracts.Services.Validation;
    using ProcessingTools.Services.Cache.Models.Validation;

    public class ValidationCacheService : IValidationCacheService
    {
        private readonly IValidationCacheDataRepository repository;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IMapper mapper;

        public ValidationCacheService(IValidationCacheDataRepository repository, IDateTimeProvider dateTimeProvider)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));

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

            return this.repository.AddAsync(key, entity);
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
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return entity;
        }
    }
}
