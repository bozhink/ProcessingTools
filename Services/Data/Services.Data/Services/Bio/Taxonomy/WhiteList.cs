namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Bio.Taxonomy;
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;

    public class WhiteList : IWhiteList
    {
        private readonly IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider;

        public WhiteList(IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        public Task<IEnumerable<string>> Items
        {
            get
            {
                return this.repositoryProvider.Execute<IEnumerable<string>>(async (repository) =>
                {
                    var query = await repository.Find(t => t.IsWhiteListed == true);

                    var result = query.Select(t => t.Name)
                        .ToList();

                    return new HashSet<string>(result);
                });
            }
        }
    }
}
