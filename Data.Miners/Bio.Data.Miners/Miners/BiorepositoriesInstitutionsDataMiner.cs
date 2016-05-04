namespace ProcessingTools.Bio.Data.Miners
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Biorepositories.Services.Data.Contracts;
    using ProcessingTools.Bio.Biorepositories.Services.Data.Models;
    using ProcessingTools.Common.Constants;

    public class BiorepositoriesInstitutionsDataMiner : IBiorepositoriesInstitutionsDataMiner
    {
        private const int NumberOfItemsToTake = DefaultPagingConstants.MaximalItemsPerPageAllowed;

        private IBiorepositoriesDataService service;

        public BiorepositoriesInstitutionsDataMiner(IBiorepositoriesDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        public async Task<IQueryable<BiorepositoryInstitution>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            Func<BiorepositoryInstitutionServiceModel, bool> filter = x => Regex.IsMatch(content, Regex.Escape(x.InstitutionalCode) + "|" + Regex.Escape(x.NameOfInstitution));

            var matches = new List<BiorepositoryInstitution>();

            for (int i = 0; true; i += NumberOfItemsToTake)
            {
                var items = (await this.service.GetInstitutions(i, NumberOfItemsToTake)).ToList();

                if (items.Count < 1)
                {
                    break;
                }

                var matchedItems = items.Where(filter)
                    .Select(x => new BiorepositoryInstitution
                    {
                        InstitutionalCode = x.InstitutionalCode,
                        NameOfInstitution = x.NameOfInstitution,
                        Url = x.Url
                    });

                matches.AddRange(matchedItems);
            }

            var result = new HashSet<BiorepositoryInstitution>(matches);
            return result.AsQueryable();
        }
    }
}