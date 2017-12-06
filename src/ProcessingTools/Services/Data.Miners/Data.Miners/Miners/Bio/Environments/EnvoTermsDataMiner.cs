namespace ProcessingTools.Data.Miners.Miners.Bio.Environments
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio.Environments;
    using ProcessingTools.Contracts.Services.Data.Bio.Environments;
    using ProcessingTools.Contracts.Services.Models.Data.Bio.Environments;

    public class EnvoTermsDataMiner : IEnvoTermsDataMiner
    {
        private readonly IEnvoTermsDataService service;

        public EnvoTermsDataMiner(IEnvoTermsDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IEnvoTerm[]> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            string text = context.ToUpperInvariant();

            var data = await this.service.AllAsync().ConfigureAwait(false);
            return data.Where(t => text.Contains(t.Content.ToUpperInvariant())).ToArray();
        }
    }
}
