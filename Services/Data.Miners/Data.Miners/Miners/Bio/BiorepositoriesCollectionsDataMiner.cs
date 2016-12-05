using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProcessingTools.Bio.Biorepositories.Services.Data.Contracts;
using ProcessingTools.Bio.Biorepositories.Services.Data.Models;
using ProcessingTools.Data.Miners.Abstractions;
using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
using ProcessingTools.Data.Miners.Contracts.Models.Bio;
using ProcessingTools.Data.Miners.Models.Bio;

namespace ProcessingTools.Data.Miners.Miners.Bio
{
    public class BiorepositoriesCollectionsDataMiner : BiorepositoriesDataMinerBase<BiorepositoriesCollection, BiorepositoriesCollectionServiceModel>, IBiorepositoriesCollectionsDataMiner
    {
        private readonly IBiorepositoriesInstitutionalCollectionsDataService institutionalCollectionsDataService;
        private readonly IBiorepositoriesPersonalCollectionsDataService personalCollectionsDataService;

        public BiorepositoriesCollectionsDataMiner(
            IBiorepositoriesInstitutionalCollectionsDataService institutionalCollectionsDataService,
            IBiorepositoriesPersonalCollectionsDataService personalCollectionsDataService)
        {
            if (institutionalCollectionsDataService == null)
            {
                throw new ArgumentNullException(nameof(institutionalCollectionsDataService));
            }

            if (personalCollectionsDataService == null)
            {
                throw new ArgumentNullException(nameof(personalCollectionsDataService));
            }

            this.institutionalCollectionsDataService = institutionalCollectionsDataService;
            this.personalCollectionsDataService = personalCollectionsDataService;
        }

        protected override Func<BiorepositoriesCollectionServiceModel, BiorepositoriesCollection> Project => x => new BiorepositoriesCollection
        {
            CollectionCode = x.CollectionCode,
            CollectionName = x.CollectionName,
            Url = x.Url
        };

        public async Task<IEnumerable<IBiorepositoriesCollection>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            Func<BiorepositoriesCollectionServiceModel, bool> filter = x => Regex.IsMatch(content, Regex.Escape(x.CollectionCode) + "|" + Regex.Escape(x.CollectionName));

            var matches = new List<BiorepositoriesCollection>();

            await this.GetMatches(this.institutionalCollectionsDataService, matches, filter);
            await this.GetMatches(this.personalCollectionsDataService, matches, filter);

            var result = new HashSet<BiorepositoriesCollection>(matches);
            return result;
        }
    }
}
