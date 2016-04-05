namespace ProcessingTools.Bio.Data.Miners
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Biorepositories.Services.Data.Contracts;
    using ProcessingTools.Common.Constants;

    public class BiorepositoryInstitutionDataMiner : IBiorepositoryInstitutionDataMiner
    {
        private const int NumberOfItemsToTake = DefaultPagingConstants.MaximalItemsPerPageAllowed;

        private IBiorepositoriesDataService service;

        public BiorepositoryInstitutionDataMiner(IBiorepositoriesDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        public Task<IQueryable<BiorepositoryInstitution>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            return Task.Run(() =>
            {
                var matches = new List<BiorepositoryInstitution>();

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

                var result = new HashSet<BiorepositoryInstitution>(matches);
                return result.AsQueryable();
            });
        }
    }
}