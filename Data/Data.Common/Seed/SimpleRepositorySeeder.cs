namespace ProcessingTools.Data.Common.Seed
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using ProcessingTools.Extensions;
    using Repositories.Contracts;

    public class SimpleRepositorySeeder<TEntity>
        where TEntity : class
    {
        private const int NumberOfItemsToInsertBeforeRepositoryReset = 100;

        private readonly IGenericRepositoryProvider<IGenericRepository<TEntity>, TEntity> repositoryProvider;

        public SimpleRepositorySeeder(IGenericRepositoryProvider<IGenericRepository<TEntity>, TEntity> repositoryProvider)
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
                    await repository.SaveChanges();
                    repository.TryDispose();
                    repository = this.repositoryProvider.Create();

                    numberOfInsertedItems = 0;
                }
            }

            await repository.SaveChanges();
            repository.TryDispose();
        }
    }
}