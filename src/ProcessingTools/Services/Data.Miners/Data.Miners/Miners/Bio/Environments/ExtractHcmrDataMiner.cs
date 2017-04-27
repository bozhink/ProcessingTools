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
        private IExtractHcmrDataRequester requester;

        public ExtractHcmrDataMiner(IExtractHcmrDataRequester requester)
        {
            if (requester == null)
            {
                throw new ArgumentNullException(nameof(requester));
            }

            this.requester = requester;
        }

        public async Task<IEnumerable<IExtractHcmrEnvoTerm>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            var response = await this.requester?.RequestData(content);

            if (response == null || response.Items == null)
            {
                throw new ApplicationException("No information found.");
            }

            var result = new HashSet<ExtractHcmrEnvoTerm>(response.Items
                .Select(i => new ExtractHcmrEnvoTerm
                {
                    Content = i.Name,
                    Types = i.Entities?.Select(e => e.Type)?.ToArray(),
                    Identifiers = i.Entities?.Select(e => e.Identifier)?.ToArray()
                }));

            return result;
        }
    }
}
