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
        private IEnvoTermsDataService service;

        public EnvoTermsDataMiner(IEnvoTermsDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        public async Task<IEnumerable<IEnvoTerm>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            string text = content.ToLower();

            var terms = (await this.service.All())
                .Select(t => new EnvoTerm
                {
                    EntityId = t.EntityId,
                    EnvoId = t.EnvoId,
                    Content = t.Content
                })
                .ToList()
                .Where(t => text.Contains(t.Content.ToLower()));

            var result = new HashSet<EnvoTerm>(terms);
            return result;
        }
    }
}
