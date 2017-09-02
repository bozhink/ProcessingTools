namespace ProcessingTools.Data.Common.Seed
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Contracts.Data.Repositories;

    public class SimpleRepositorySeeder<TEntity>
        where TEntity : class
    {
        private const int NumberOfItemsToInsertBeforeRepositoryReset = 100;

        private readonly ICrudRepositoryProvider<TEntity> repositoryProvider;

        public SimpleRepositorySeeder(ICrudRepositoryProvider<TEntity> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        public async Task Seed(params TEntity[] data)
        {
            if (data == null || data.Length < 1)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var repository = this.repositoryProvider.Create();

            int numberOfInsertedItems = 0;
            foreach (var entity in data)
            {
                await repository.AddAsync(entity).ConfigureAwait(false);
                ++numberOfInsertedItems;

                if (numberOfInsertedItems >= NumberOfItemsToInsertBeforeRepositoryReset)
                {
                    await repository.SaveChangesAsync().ConfigureAwait(false);
                    repository.TryDispose();
                    repository = this.repositoryProvider.Create();

                    numberOfInsertedItems = 0;
                }
            }

            await repository.SaveChangesAsync().ConfigureAwait(false);
            repository.TryDispose();
        }
    }
}
