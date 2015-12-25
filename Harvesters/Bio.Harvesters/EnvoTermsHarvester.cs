namespace ProcessingTools.Bio.Harvesters
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Environments.Services.Data.Contracts;
    using Models;
    using Models.Contracts;

    using ProcessingTools.Harvesters.Common.Factories;

    public class EnvoTermsHarvester : GenericHarvesterFactory<IEnvoTerm>, IEnvoTermsHarvester
    {
        private IEnvoTermsDataService service;

        public EnvoTermsHarvester(IEnvoTermsDataService service)
            : base()
        {
            this.service = service;
        }

        public override void Harvest(string content)
        {
            string text = content.ToLower();

            var terms = this.service.All()
                .Select(t => new EnvoTerm
                {
                    EntityId = t.EntityId,
                    EnvoId = t.EnvoId,
                    Content = t.Content
                })
                .ToList()
                .Where(t => text.Contains(t.Content.ToLower()));

            this.Items = new HashSet<IEnvoTerm>(terms);
        }
    }
}