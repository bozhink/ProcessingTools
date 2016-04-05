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

    public class BiorepositoryInstitutionalCodesDataMiner : IBiorepositoryInstitutionalCodesDataMiner
    {
        private const int NumberOfItemsToTake = DefaultPagingConstants.MaximalItemsPerPageAllowed;

        private IBiorepositoriesDataService service;

        public BiorepositoryInstitutionalCodesDataMiner(IBiorepositoriesDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        public Task<IQueryable<BiorepositoryInstitutionalCode>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            return Task.Run(() =>
            {
                var matches = new List<BiorepositoryInstitutionalCode>();

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

                var result = new HashSet<BiorepositoryInstitutionalCode>(matches);
                return result.AsQueryable();
            });
        }
    }
}
