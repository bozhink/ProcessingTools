namespace ProcessingTools.Data.Miners.Miners.Bio
{
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

    public class BiorepositoriesCollectionsDataMiner : BiorepositoriesDataMinerBase<BiorepositoriesCollection, Collection>, IBiorepositoriesCollectionsDataMiner
    {
        private readonly IBiorepositoriesInstitutionalCollectionsDataService institutionalCollectionsDataService;
        private readonly IBiorepositoriesPersonalCollectionsDataService personalCollectionsDataService;

        public BiorepositoriesCollectionsDataMiner(
            IBiorepositoriesInstitutionalCollectionsDataService institutionalCollectionsDataService,
            IBiorepositoriesPersonalCollectionsDataService personalCollectionsDataService)
        {
            this.institutionalCollectionsDataService = institutionalCollectionsDataService ?? throw new ArgumentNullException(nameof(institutionalCollectionsDataService));
            this.personalCollectionsDataService = personalCollectionsDataService ?? throw new ArgumentNullException(nameof(personalCollectionsDataService));
        }

        protected override Func<Collection, BiorepositoriesCollection> Project => x => new BiorepositoriesCollection
        {
            CollectionCode = x.Code,
            CollectionName = x.Name,
            Url = x.Url
        };

        public async Task<IEnumerable<IBiorepositoriesCollection>> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            Func<Collection, bool> filter = x => Regex.IsMatch(context, Regex.Escape(x.Code) + "|" + Regex.Escape(x.Name));

            var matches = new List<BiorepositoriesCollection>();

            await this.GetMatches(this.institutionalCollectionsDataService, matches, filter).ConfigureAwait(false);
            await this.GetMatches(this.personalCollectionsDataService, matches, filter).ConfigureAwait(false);

            var result = new HashSet<BiorepositoriesCollection>(matches);
            return result;
        }
    }
}
