namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Repositories.Contracts;
    using ProcessingTools.Extensions;

    public class BiotaxonomicBlackListDataService : IBiotaxonomicBlackListDataService
    {
        private readonly IBiotaxonomicBlackListRepositoryProvider provider;

        public BiotaxonomicBlackListDataService(IBiotaxonomicBlackListRepositoryProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
        }

        public async Task<object> Add(params string[] items)
        {
            var validItems = this.ValidateInputItems(items);

            var repository = this.provider.Create();

            {
                var tasks = validItems.Select(s => new BlackListEntity
                {
                    Content = s
                })
                .Select(b => repository.Add(b))
                .ToArray();

                await Task.WhenAll(tasks);
            }

            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
        }

        public async Task<object> Delete(params string[] items)
        {
            var validItems = this.ValidateInputItems(items);

            var repository = this.provider.Create();

            {
                var tasks = validItems.Select(s => new BlackListEntity
                {
                    Content = s
                })
                .Select(b => repository.Delete(b))
                .ToArray();

                await Task.WhenAll(tasks);
            }

            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
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
