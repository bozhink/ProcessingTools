// <copyright file="DatabasesService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Admin
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Services.Contracts.Admin;
    using ProcessingTools.Services.Models.Admin.Databases;
    using ProcessingTools.Services.Models.Contracts.Admin.Databases;

    /// <summary>
    /// Databases service.
    /// </summary>
    public class DatabasesService : IDatabasesService
    {
        private readonly Func<IEnumerable<IDatabaseInitializer>> initializersFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabasesService"/> class.
        /// </summary>
        /// <param name="initializersFactory">Factory for database initializers.</param>
        public DatabasesService(Func<IEnumerable<IDatabaseInitializer>> initializersFactory)
        {
            this.initializersFactory = initializersFactory ?? throw new ArgumentNullException(nameof(initializersFactory));
        }

        /// <inheritdoc/>
        public async Task<IInitializeModel> InitializeAllAsync()
        {
            var initializers = this.initializersFactory.Invoke();

            ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();

            foreach (var initializer in initializers)
            {
                try
                {
                    await initializer.InitializeAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    exceptions.Enqueue(ex);
                }
            }

            var response = new InitializeModel
            {
                NumberOfDatabases = initializers.Count(),
                Success = !exceptions.Any(),
                Exceptions = exceptions.ToList()
            };

            return response;
        }
    }
}
