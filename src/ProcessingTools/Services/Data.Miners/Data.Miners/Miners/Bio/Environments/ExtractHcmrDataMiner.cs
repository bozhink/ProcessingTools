namespace ProcessingTools.Data.Miners.Miners.Bio.Environments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.ServiceClient.ExtractHcmr.Contracts;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio.Environments;
    using ProcessingTools.Data.Miners.Contracts.Models.Bio.Environments;
    using ProcessingTools.Data.Miners.Models.Bio.Environments;

    public class ExtractHcmrDataMiner : IExtractHcmrDataMiner
    {
        private readonly IExtractHcmrDataRequester requester;

        public ExtractHcmrDataMiner(IExtractHcmrDataRequester requester)
        {
            this.requester = requester ?? throw new ArgumentNullException(nameof(requester));
        }

        public async Task<IEnumerable<IExtractHcmrEnvoTerm>> MineAsync(string context)
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
