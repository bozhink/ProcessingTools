namespace ProcessingTools.Bio.Harvesters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models;
    using Models.Contracts;

    using ProcessingTools.Harvesters.Common.Factories;
    using ServiceClient.ExtractHcmr.Contracts;

    public class ExtractHcmrHarvester : GenericHarvesterFactory<IExtractHcmrEnvoTerm>, IExtractHcmrHarvester
    {
        private IExtractHcmrDataRequester requester;

        public ExtractHcmrHarvester(IExtractHcmrDataRequester requester)
            : base()
        {
            this.requester = requester;
        }

        public override void Harvest(string content)
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

            this.Items = new HashSet<IExtractHcmrEnvoTerm>(result);
        }
    }
}