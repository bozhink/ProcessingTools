namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Data.Repositories.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;

    public class WhiteList : IWhiteList
    {
        private readonly IGenericRepositoryProvider<ITaxonRanksRepository> repositoryProvider;

        public WhiteList(IGenericRepositoryProvider<ITaxonRanksRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        public IEnumerable<string> GetItems()
        {
            return this.GetItemsAsync().Result;
        }

        public Task<IEnumerable<string>> GetItemsAsync()
        {
            return this.repositoryProvider.Execute<IEnumerable<string>>(async (repository) =>
            {
                var data = await repository.FindAsync(t => t.IsWhiteListed).ConfigureAwait(false);

                var result = data.Select(t => t.Name).ToList();

                return new HashSet<string>(result);
            });
        }
    }
}
