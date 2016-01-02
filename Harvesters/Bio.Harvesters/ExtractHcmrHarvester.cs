namespace ProcessingTools.Bio.Harvesters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models;
    using Models.Contracts;
    using ServiceClient.ExtractHcmr.Contracts;

    public class ExtractHcmrHarvester : IExtractHcmrHarvester
    {
        private IExtractHcmrDataRequester requester;

        public ExtractHcmrHarvester(IExtractHcmrDataRequester requester)
        {
            this.requester = requester;
        }

        public async Task<IQueryable<IExtractHcmrEnvoTerm>> Harvest(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException("content");
            }

            var response = await this.requester?.RequestData(content);

            if (response == null || response.Items == null)
            {
                throw new ApplicationException("No information found.");
            }

            var result = new HashSet<IExtractHcmrEnvoTerm>(response.Items
                .Select(i => new ExtractHcmrEnvoTerm
                {
                    Content = i.Name,
                    Types = i.Entities?.Select(e => e.Type)?.ToArray(),
                    Identifiers = i.Entities?.Select(e => e.Identifier)?.ToArray()
                }));

            return result.AsQueryable();
        }
    }
}