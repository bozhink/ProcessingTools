namespace ProcessingTools.Documents.Services.Data
{
    using System;
    using System.Collections.Generic;

    using Contracts;
    using Models;
    using Models.Contracts;

    using ProcessingTools.Documents.Data.Common.Repositories.Contracts;
    using ProcessingTools.Documents.Data.Models;
    using ProcessingTools.Services.Common.Factories;

    public class CountriesDataService : RepositoryDataServiceAbstractFactory<Country, ICountryServiceModel>, ICountriesDataService
    {
        public CountriesDataService(IDocumentsRepository<Country> repository)
            : base(repository)
        {
        }

        protected override IEnumerable<Country> MapServiceModelToDbModel(params ICountryServiceModel[] models)
        {
            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var result = new HashSet<Country>();
            foreach (var model in models)
            {
                var entity = new Country
                {
                    CreatedByUserId = model.CreatedByUserId,
                    DateModified = model.DateModified,
                    DateCreated = model.DateCreated,
                    ModifiedByUserId = model.ModifiedByUserId,
                    Name = model.Name
                };

                result.Add(entity);
            }

            return result;
        }

        protected override IEnumerable<ICountryServiceModel> MapDbModelToServiceModel(params Country[] entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            var result = new HashSet<ICountryServiceModel>();
            foreach (var entity in entities)
            {
                var model = new CountryServiceModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CreatedByUserId = entity.CreatedByUserId,
                    ModifiedByUserId = entity.ModifiedByUserId,
                    DateCreated = entity.DateCreated,
                    DateModified = entity.DateModified
                };

                result.Add(model);
            }

            return result;
        }
    }
}