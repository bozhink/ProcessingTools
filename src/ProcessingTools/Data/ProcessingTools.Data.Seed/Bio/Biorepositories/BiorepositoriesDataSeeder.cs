﻿// <copyright file="BiorepositoriesDataSeeder.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Seed.Bio.Biorepositories
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Common.Code.Data.Seed;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Models.Mongo.Bio.Biorepositories;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.Data.Mongo.Bio.Biorepositories;
    using ProcessingTools.Data.Seed.Bio.Biorepositories.Models.Csv;
    using ProcessingTools.Extensions;
    using ProcessingTools.Services.Serialization.Csv;

    /// <summary>
    /// Biorepositories data seeder.
    /// </summary>
    public class BiorepositoriesDataSeeder : IBiorepositoriesDataSeeder
    {
        private readonly IMongoDatabaseProvider contextProvider;
        private readonly string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="BiorepositoriesDataSeeder"/> class.
        /// </summary>
        /// <param name="contextProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        public BiorepositoriesDataSeeder(IMongoDatabaseProvider contextProvider)
        {
            this.contextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));

            this.dataFilesDirectoryPath = AppSettings.BiorepositoriesSeedCsvDataFilesDirectoryName;
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        /// <summary>
        /// Seeds databases with data.
        /// </summary>
        /// <returns>Object to be awaited.</returns>
        public async Task<object> SeedAsync()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            var tasks = new List<Task>
            {
                this.ImportCsvFileToMongo<CollectionCsv, Collection>(),
                this.ImportCsvFileToMongo<CollectionLabelCsv, CollectionLabel>(),
                this.ImportCsvFileToMongo<CollectionPerCsv, CollectionPer>(),
                this.ImportCsvFileToMongo<CollectionPerLabelCsv, CollectionPerLabel>(),
                this.ImportCsvFileToMongo<InstitutionCsv, Institution>(),
                this.ImportCsvFileToMongo<InstitutionLabelCsv, InstitutionLabel>(),
                this.ImportCsvFileToMongo<StaffCsv, Staff>(),
                this.ImportCsvFileToMongo<StaffLabelCsv, StaffLabel>(),
            };
            await Task.WhenAll(tasks.ToArray()).ConfigureAwait(false);

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
        }

        private async Task ImportCsvFileToMongo<TSeedModel, TEntityModel>()
            where TSeedModel : class
            where TEntityModel : class, new()
        {
            try
            {
                Type seedModelType = typeof(TSeedModel);
                if (!(seedModelType.GetCustomAttributes(typeof(FileNameAttribute), false)?.FirstOrDefault() is FileNameAttribute fileNameAttribute))
                {
                    throw new ProcessingTools.Common.Exceptions.InvalidModelException($"Invalid seed model {seedModelType.Name}: There is no FileNameAttribute.");
                }

                string fileName = Path.Combine(this.dataFilesDirectoryPath, fileNameAttribute.Name);
                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException($"File {fileName} does not exist.");
                }

                string csvText = File.ReadAllText(fileName);
                var serializer = new CsvSerializer();
                var items = serializer.Deserialize<TSeedModel>(csvText)?.Select(i => i.Map<TEntityModel>())
                    .ToArray();
                if (items == null || items.Length < 1)
                {
                    throw new ProcessingTools.Common.Exceptions.InvalidDataException("De-serialized items are not valid.");
                }

                ICrudRepository<TEntityModel> RepositoryFactory() => new BiorepositoriesRepository<TEntityModel>(this.contextProvider);

                var seeder = new SimpleRepositorySeeder<TEntityModel>(RepositoryFactory);

                await seeder.SeedAsync(items).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }
    }
}
