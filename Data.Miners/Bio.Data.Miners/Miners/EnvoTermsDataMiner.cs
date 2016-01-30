namespace ProcessingTools.Bio.Data.Miners
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Environments.Services.Data.Contracts;
    using Models;
    using Models.Contracts;

    public class EnvoTermsDataMiner : IEnvoTermsDataMiner
    {
        private IEnvoTermsDataService service;

        public EnvoTermsDataMiner(IEnvoTermsDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            this.service = service;
        }

        public Task<IQueryable<IEnvoTerm>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException("content");
            }

            string text = content.ToLower();

            return Task.Run(() =>
            {
                var terms = this.service.All()
                    .Select(t => new EnvoTerm
                    {
                        EntityId = t.EntityId,
                        EnvoId = t.EnvoId,
                        Content = t.Content
                    })
                    .ToList()
                    .Where(t => text.Contains(t.Content.ToLower()));

                var result = new HashSet<IEnvoTerm>(terms);
                return result.AsQueryable();
            });
        }
    }
}