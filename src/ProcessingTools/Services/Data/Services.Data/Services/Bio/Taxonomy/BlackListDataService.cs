namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts.Repositories.Bio.Taxonomy;
    using ProcessingTools.Data.Contracts.Repositories;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Models.Bio.Taxonomy;

    public class BlackListDataService : IBlackListDataService
    {
        private readonly IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider;

        public BlackListDataService(IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        public async Task<object> Add(params string[] items)
        {
            var validItems = this.ValidateInputItems(items);

            return await this.repositoryProvider.Execute(async (repository) =>
            {
                var tasks = validItems.Select(s => new BlackListEntity
                {
                    Content = s
                })
                .Select(b => repository.AddAsync(b))
                .ToArray();

                await Task.WhenAll(tasks).ConfigureAwait(false);
                return await repository.SaveChangesAsync().ConfigureAwait(false);
            })
            .ConfigureAwait(false);
        }

        public async Task<object> Delete(params string[] items)
        {
            var validItems = this.ValidateInputItems(items);

            return await this.repositoryProvider.Execute(async (repository) =>
            {
                var tasks = validItems.Select(b => repository.DeleteAsync(b)).ToArray();
                await Task.WhenAll(tasks).ConfigureAwait(false);
                return await repository.SaveChangesAsync().ConfigureAwait(false);
            })
            .ConfigureAwait(false);
        }

        private IEnumerable<string> ValidateInputItems(params string[] items)
        {
            if (items == null || items.Length < 1)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var validItems = items.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

            if (validItems.Length < 1)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return validItems;
        }
    }
}
