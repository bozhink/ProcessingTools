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

    public class BiorepositoriesInstitutionsDataMiner : BiorepositoriesDataMinerBase<BiorepositoriesInstitution, IInstitution>, IBiorepositoriesInstitutionsDataMiner
    {
        private readonly IBiorepositoriesInstitutionsDataService service;

        public BiorepositoriesInstitutionsDataMiner(IBiorepositoriesInstitutionsDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IEnumerable<IBiorepositoriesInstitution>> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            var matches = new List<BiorepositoriesInstitution>();

            await this.GetMatches(
                this.service,
                matches,
                filter: x => Regex.IsMatch(context, Regex.Escape(x.Code) + "|" + Regex.Escape(x.Name)),
                projection: x => new BiorepositoriesInstitution
                {
                    InstitutionalCode = x.Code,
                    NameOfInstitution = x.Name,
                    Url = x.Url
                }).ConfigureAwait(false);

            var result = new HashSet<BiorepositoriesInstitution>(matches);
            return result;
        }
    }
}
