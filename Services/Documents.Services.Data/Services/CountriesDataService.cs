namespace ProcessingTools.Documents.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Models;
    using Models.Contracts;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Documents.Data.Common.Repositories.Contracts;
    using ProcessingTools.Documents.Data.Models;

    public class CountriesDataService : ICountriesDataService
    {
        private IDocumentsRepository<Country> repository;

        private Func<ICountryServiceModel, Country> serviceModelToEntity = (model) => new Country
        {
            CreatedByUserId = model.CreatedByUserId,
            DateModified = model.DateModified,
            DateCreated = model.DateCreated,
            ModifiedByUserId = model.ModifiedByUserId,
            Name = model.Name
        };

        private Func<Country, ICountryServiceModel> entityToServiceModel = (entity) => new CountryServiceModel
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedByUserId = entity.CreatedByUserId,
            ModifiedByUserId = entity.ModifiedByUserId,
            DateCreated = entity.DateCreated,
            DateModified = entity.DateModified
        };

        public CountriesDataService(IDocumentsRepository<Country> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
        }

        public async Task Add(ICountryServiceModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            var entity = this.serviceModelToEntity.Invoke(model);

            await this.repository.Add(entity);
            await this.repository.SaveChanges();
        }

        public async Task<IQueryable<ICountryServiceModel>> All()
        {
            return (await this.repository.All())
                .Select(e => this.entityToServiceModel(e));
        }

        public Task Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            return this.repository.Delete(id);
        }

        public Task Delete(ICountryServiceModel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.repository.Delete(entity.Id);
        }

        public async Task<ICountryServiceModel> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            var entity = await this.repository.Get(id);

            return this.entityToServiceModel(entity);
        }

        public async Task<IQueryable<ICountryServiceModel>> Get(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > DefaultPagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            return (await this.repository.All())
                .OrderByDescending(i => i)
                .Skip(skip)
                .Take(take)
                .Select(this.entityToServiceModel)
                .AsQueryable();
        }

        public async Task Update(ICountryServiceModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            var entity = await this.repository.Get(model.Id);

            await this.repository.Update(entity);
            await this.repository.SaveChanges();
        }
    }
}