namespace ProcessingTools.Bio.Harvesters
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

    public class BiorepositoryInstitutionalCodesHarvester : IBiorepositoryInstitutionalCodesHarvester
    {
        private const int NumberOfItemsToTake = DefaultPagingConstants.MaximalItemsPerPageAllowed;

        private IBiorepositoriesDataService service;

        public BiorepositoryInstitutionalCodesHarvester(IBiorepositoriesDataService service)
        {
            this.service = service;
        }

        public Task<IQueryable<IBiorepositoryInstitutionalCode>> Harvest(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException("content");
            }

            return Task.Run(() =>
            {
                var matches = new List<IBiorepositoryInstitutionalCode>();

                for (int i = 0; true; i += NumberOfItemsToTake)
                {
                    var items = this.service.GetBiorepositoryInstitutionalCodes(i, NumberOfItemsToTake).ToList();

                    if (items.Count < 1)
                    {
                        break;
                    }

                    matches.AddRange(items.Where(x => content.Contains(x.NameOfInstitution))
                        .Select(x => new BiorepositoryInstitutionalCode
                        {
                            Description = x.NameOfInstitution,
                            InstitutionalCode = x.InstitutionalCode,
                            Url = x.Url
                        }));
                }

                var result = new HashSet<IBiorepositoryInstitutionalCode>(matches);
                return result.AsQueryable();
            });
        }
    }
}
