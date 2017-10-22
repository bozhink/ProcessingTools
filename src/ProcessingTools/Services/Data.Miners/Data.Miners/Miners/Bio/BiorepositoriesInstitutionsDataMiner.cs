namespace ProcessingTools.Data.Miners.Miners.Bio
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Miners.Abstractions;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Services.Contracts.Data.Bio.Biorepositories;
    using ProcessingTools.Services.Models.Contracts.Data.Bio.Biorepositories;

    public class BiorepositoriesInstitutionsDataMiner : BiorepositoriesDataMinerBase<IInstitution>, IBiorepositoriesInstitutionsDataMiner
    {
        private readonly IBiorepositoriesInstitutionsDataService service;

        public BiorepositoriesInstitutionsDataMiner(IBiorepositoriesInstitutionsDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IInstitution[]> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            Func<IInstitution, bool> filter = x => Regex.IsMatch(context, Regex.Escape(x.Code) + "|" + Regex.Escape(x.Name));

            var matches = new List<IInstitution>();

            await this.GetMatches(this.service, matches, filter).ConfigureAwait(false);

            return matches.ToArray();
        }
    }
}
