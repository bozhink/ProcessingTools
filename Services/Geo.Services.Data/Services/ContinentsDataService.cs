namespace ProcessingTools.Geo.Services.Data.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Extensions;
    using ProcessingTools.Geo.Data.Models;
    using ProcessingTools.Geo.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class ContinentsDataService : DataServiceFactory<Continent, ContinentServiceModel>, IContinentsDataService
    {
        public ContinentsDataService(IGeoDataRepositoryProvider<Continent> repositoryProvider)
            : base(repositoryProvider)
        {
        }

        protected override Expression<Func<Continent, ContinentServiceModel>> MapDataToServiceModel => e => new ContinentServiceModel
        {
            Id = e.Id,

            Name = e.Name,

            Synonyms = e.Synonyms.Select(s => new ContinentSynonymServiceModel
            {
                Id = s.Id,
                Name = s.Name
            }).ToList(),

            Countries = e.Countries.Select(c => new CountryServiceModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList()
        };

        protected override Expression<Func<ContinentServiceModel, Continent>> MapServiceToDataModel => m => new Continent
        {
            Id = m.Id,

            Name = m.Name,

            Synonyms = m.Synonyms.Select(s => new ContinentSynonym
            {
                Id = s.Id,
                Name = s.Name
            }).ToList(),

            Countries = m.Countries.Select(c => new Country
            {
                Id = c.Id,
                Name = c.Name
            }).ToList()
        };

        public async override Task<IQueryable<ContinentServiceModel>> All()
        {
            return (await base.All()).OrderBy(c => c.Name);
        }

        public async Task<object> AddSynonym(int continentId, ContinentSynonymServiceModel synonym)
        {
            if (synonym == null)
            {
                throw new ArgumentNullException(nameof(synonym));
            }

            var repository = this.repositoryProvider.Create();

            var entity = await repository.Get(id: continentId);

            entity.Synonyms.Add(new ContinentSynonym
            {
                Id = synonym.Id,
                Name = synonym.Name
            });

            await repository.Update(entity: entity);

            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
        }

        public async Task<object> RemoveSynonym(int continentId, ContinentSynonymServiceModel synonym)
        {
            var repository = this.repositoryProvider.Create();

            var entity = await repository.Get(id: continentId);

            var synonymToBeRemoved = entity.Synonyms.FirstOrDefault(s => s.Name == synonym.Name);
            entity.Synonyms.Remove(synonymToBeRemoved);

            await repository.Update(entity: entity);

            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
        }
    }
}
