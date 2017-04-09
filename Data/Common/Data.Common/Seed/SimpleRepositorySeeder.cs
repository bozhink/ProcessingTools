namespace ProcessingTools.Data.Common.Seed
{
    using System;
    using System.Threading.Tasks;

    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Extensions;

    public class SimpleRepositorySeeder<TEntity>
        where TEntity : class
    {
        private const int NumberOfItemsToInsertBeforeRepositoryReset = 100;

        private readonly ICrudRepositoryProvider<TEntity> repositoryProvider;

        public SimpleRepositorySeeder(ICrudRepositoryProvider<TEntity> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
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
                await repository.Add(entity);
                ++numberOfInsertedItems;

                if (numberOfInsertedItems >= NumberOfItemsToInsertBeforeRepositoryReset)
                {
                    await repository.SaveChangesAsync();
                    repository.TryDispose();
                    repository = this.repositoryProvider.Create();

                    numberOfInsertedItems = 0;
                }
            }

            await repository.SaveChangesAsync();
            repository.TryDispose();
        }
    }
}