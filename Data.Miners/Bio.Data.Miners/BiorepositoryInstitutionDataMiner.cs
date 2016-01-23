namespace ProcessingTools.Bio.Data.Miners
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Biorepositories.Services.Data.Contracts;
    using Contracts;
    using Models;
    using Models.Contracts;
    using ProcessingTools.Common.Constants;

    public class BiorepositoryInstitutionDataMiner : IBiorepositoryInstitutionDataMiner
    {
        private const int NumberOfItemsToTake = DefaultPagingConstants.MaximalItemsPerPageAllowed;

        private IBiorepositoriesDataService service;

        public BiorepositoryInstitutionDataMiner(IBiorepositoriesDataService service)
        {
            this.service = service;
        }

        public Task<IQueryable<IBiorepositoryInstitution>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException("content");
            }

            return Task.Run(() =>
            {
                var matches = new List<IBiorepositoryInstitution>();

                for (int i = 0; true; i += NumberOfItemsToTake)
                {
                    var items = this.service.GetBiorepositoryInstitutions(i, NumberOfItemsToTake).ToList();

                    if (items.Count < 1)
                    {
                        break;
                    }

                    matches.AddRange(items.Where(x => content.Contains(x.Name))
                        .Select(x => new BiorepositoryInstitution
                        {
                            Name = x.Name,
                            Url = x.Url
                        }));
                }

                var result = new HashSet<IBiorepositoryInstitution>(matches);
                return result.AsQueryable();
            });
        }
    }
}