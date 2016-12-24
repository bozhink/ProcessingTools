namespace ProcessingTools.Bio.Taxonomy.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public class BiotaxonomicBlackListDataService : IBiotaxonomicBlackListDataService
    {
        private readonly IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider;

        public BiotaxonomicBlackListDataService(IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
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
                .Select(b => repository.Add(b))
                .ToArray();

                await Task.WhenAll(tasks);

                var result = await repository.SaveChanges();
                return result;
            });
        }

        public async Task<object> Delete(params string[] items)
        {
            var validItems = this.ValidateInputItems(items);

            return await this.repositoryProvider.Execute(async (repository) =>
            {
                var tasks = validItems.Select(s => new BlackListEntity
                {
                    Content = s
                })
                .Select(b => repository.Delete(b))
                .ToArray();

                await Task.WhenAll(tasks);

                var result = await repository.SaveChanges();
                return result;
            });
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
