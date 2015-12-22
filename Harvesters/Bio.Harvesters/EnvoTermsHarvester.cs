namespace ProcessingTools.Bio.Harvesters
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Environments.Services.Data.Contracts;
    using Models;
    using Models.Contracts;

    public class EnvoTermsHarvester : IEnvoTermsHarvester
    {
        private ICollection<IEnvoTerm> data;
        private IEnvoTermsDataService service;

        public EnvoTermsHarvester(IEnvoTermsDataService service)
        {
            this.data = new HashSet<IEnvoTerm>();
            this.service = service;
        }

        public IQueryable<IEnvoTerm> Data
        {
            get
            {
                return this.data.AsQueryable();
            }
        }

        public void Harvest(string content)
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

            this.data = new HashSet<IEnvoTerm>(terms);
        }
    }
}