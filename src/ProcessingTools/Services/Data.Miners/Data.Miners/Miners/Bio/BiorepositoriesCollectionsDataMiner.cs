namespace ProcessingTools.Data.Miners.Miners.Bio
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Miners.Abstractions;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Data.Miners.Contracts.Models.Bio;
    using ProcessingTools.Data.Miners.Models.Bio;
    using ProcessingTools.Services.Contracts.Data.Bio.Biorepositories;
    using ProcessingTools.Services.Models.Contracts.Data.Bio.Biorepositories;

    public class BiorepositoriesCollectionsDataMiner : BiorepositoriesDataMinerBase<BiorepositoriesCollection, ICollection>, IBiorepositoriesCollectionsDataMiner
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

        public async Task<IEnumerable<IBiorepositoriesCollection>> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            Func<ICollection, bool> filter = x => Regex.IsMatch(context, Regex.Escape(x.Code) + "|" + Regex.Escape(x.Name));

            Func<ICollection, BiorepositoriesCollection> projection = x => new BiorepositoriesCollection
            {
                CollectionCode = x.Code,
                CollectionName = x.Name,
                Url = x.Url
            };

            var matches = new List<BiorepositoriesCollection>();

            await this.GetMatches(this.institutionalCollectionsDataService, matches, filter, projection).ConfigureAwait(false);
            await this.GetMatches(this.personalCollectionsDataService, matches, filter, projection).ConfigureAwait(false);

            var result = new HashSet<BiorepositoriesCollection>(matches);
            return result;
        }
    }
}
