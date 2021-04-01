// <copyright file="SimpleRepositorySeeder.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Data.Seed
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Simple Repository Seeder.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    public class SimpleRepositorySeeder<TEntity>
        where TEntity : class
    {
        private const int NumberOfItemsToInsertBeforeRepositoryReset = 100;

        private readonly Func<ICrudRepository<TEntity>> repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleRepositorySeeder{TEntity}"/> class.
        /// </summary>
        /// <param name="repositoryFactory">Repository factory.</param>
        public SimpleRepositorySeeder(Func<ICrudRepository<TEntity>> repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// Seeds repository with data.
        /// </summary>
        /// <param name="data">Data to be used for seeding.</param>
        /// <returns>Task.</returns>
        public async Task SeedAsync(params TEntity[] data)
        {
            if (data is null || data.Length < 1)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var repository = this.repositoryFactory.Invoke();

            int numberOfInsertedItems = 0;
            foreach (var entity in data)
            {
                await repository.AddAsync(entity).ConfigureAwait(false);
                ++numberOfInsertedItems;

                if (numberOfInsertedItems >= NumberOfItemsToInsertBeforeRepositoryReset)
                {
                    await repository.SaveChangesAsync().ConfigureAwait(false);
                    repository.TryDispose();
                    repository = this.repositoryFactory.Invoke();

                    numberOfInsertedItems = 0;
                }
            }

            await repository.SaveChangesAsync().ConfigureAwait(false);
            repository.TryDispose();
        }
    }
}
