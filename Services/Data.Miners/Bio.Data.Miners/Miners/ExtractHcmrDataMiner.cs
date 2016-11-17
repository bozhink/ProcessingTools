namespace ProcessingTools.Bio.Data.Miners
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.ServiceClient.ExtractHcmr.Contracts;

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

        public async Task<IEnumerable<ExtractHcmrEnvoTerm>> Mine(string content)
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