namespace ProcessingTools.Data.Miners.Bio.Environments
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Contracts.Bio;
    using ProcessingTools.Data.Miners.Contracts.Bio.Environments;
    using ProcessingTools.Data.Miners.Models.Bio.Environments;
    using ProcessingTools.Data.Miners.Models.Contracts.Bio.Environments;

    public class ExtractHcmrDataMiner : IExtractHcmrDataMiner
    {
        private readonly IExtractHcmrDataRequester requester;

        public ExtractHcmrDataMiner(IExtractHcmrDataRequester requester)
        {
            this.requester = requester ?? throw new ArgumentNullException(nameof(requester));
        }

        public async Task<IExtractHcmrEnvoTerm[]> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = await this.requester.RequestDataAsync(context).ConfigureAwait(false);
            if (response == null || response.Items == null)
            {
                return null;
            }

            var result = response.Items
                .Select(i => new ExtractHcmrEnvoTerm
                {
                    Content = i.Name,
                    Types = i.Entities?.Select(e => e.Type)?.ToArray(),
                    Identifiers = i.Entities?.Select(e => e.Identifier)?.ToArray()
                })
                .ToArray();

            return result;
        }
    }
}
