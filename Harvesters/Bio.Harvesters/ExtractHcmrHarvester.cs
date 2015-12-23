namespace ProcessingTools.Bio.Harvesters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models;
    using Models.Contracts;
    using ServiceClient.ExtractHcmr.Contracts;

    public class ExtractHcmrHarvester : IExtractHcmrHarvester
    {
        private IExtractHcmrDataRequester requester;
        private ICollection<IExtractHcmrEnvoTerm> data;

        public ExtractHcmrHarvester(IExtractHcmrDataRequester requester)
        {
            this.requester = requester;
            this.data = new HashSet<IExtractHcmrEnvoTerm>();
        }

        public IQueryable<IExtractHcmrEnvoTerm> Data
        {
            get
            {
                return this.data.AsQueryable();
            }
        }

        public void Harvest(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException("content");
            }

            var response = this.requester?.RequestData(content)?.Result;

            if (response == null || response.Items == null)
            {
                throw new ApplicationException("No information found.");
            }

            var result = response.Items
                .Select(i => new ExtractHcmrEnvoTerm
                {
                    Content = i.Name,
                    Types = i.Entities?.Select(e => e.Type)?.ToArray(),
                    Identifiers = i.Entities?.Select(e => e.Identifier)?.ToArray()
                });

            this.data = new HashSet<IExtractHcmrEnvoTerm>(result);
        }
    }
}