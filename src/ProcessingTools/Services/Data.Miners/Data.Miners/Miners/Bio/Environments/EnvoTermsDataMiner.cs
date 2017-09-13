namespace ProcessingTools.Data.Miners.Miners.Bio.Environments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Environments.Services.Data.Contracts;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio.Environments;
    using ProcessingTools.Data.Miners.Contracts.Models.Bio.Environments;
    using ProcessingTools.Data.Miners.Models.Bio.Environments;

    public class EnvoTermsDataMiner : IEnvoTermsDataMiner
    {
        private readonly IEnvoTermsDataService service;

        public EnvoTermsDataMiner(IEnvoTermsDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IEnumerable<IEnvoTerm>> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            string text = context.ToLowerInvariant();

            var query = (await this.service.All().ConfigureAwait(false))
                .Select(t => new EnvoTerm
                {
                    EntityId = t.EntityId,
                    EnvoId = t.EnvoId,
                    Content = t.Content
                });
            var data = query.ToList();
            var result = new HashSet<EnvoTerm>(data.Where(t => text.Contains(t.Content.ToLowerInvariant())));
            return result;
        }
    }
}
