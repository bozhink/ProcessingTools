namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Bio.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Extensions.Linq;

    public class BlackList : IBlackList
    {
        private readonly IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider;

        public BlackList(IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider)
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
                    var result = await repository.Entities
                        .Select(s => s.Content)
                        .ToListAsync();

                    return new HashSet<string>(result);
                });
            }
        }
    }
}
