// <copyright file="SimpleRepositorySeeder.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Data.Seed
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

        private readonly ICrudRepositoryProvider<TEntity> repositoryProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleRepositorySeeder{TEntity}"/> class.
        /// </summary>
        /// <param name="repositoryProvider">Repository provider.</param>
        public SimpleRepositorySeeder(ICrudRepositoryProvider<TEntity> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        /// <summary>
        /// Seeds repository with data.
        /// </summary>
        /// <param name="data">Data to be used for seeding.</param>
        /// <returns>Task</returns>
        public async Task SeedAsync(params TEntity[] data)
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
