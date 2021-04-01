// <copyright file="SeedCommandRunner.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Core
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.DbSeeder.Contracts.Providers;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    /// <summary>
    /// Seed command runner.
    /// </summary>
    internal class SeedCommandRunner : ICommandRunner
    {
        private readonly ICommandNamesProvider commandNamesProvider;
        private readonly ITypesProvider typesProvider;
        private readonly Func<Type, IDbSeeder> seederFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeedCommandRunner"/> class.
        /// </summary>
        /// <param name="commandNamesProvider">Instance of <see cref="ICommandNamesProvider"/>.</param>
        /// <param name="typesProvider">Instance of <see cref="ITypesProvider"/>.</param>
        /// <param name="seederFactory">Factory of seeders.</param>
        public SeedCommandRunner(
            ICommandNamesProvider commandNamesProvider,
            ITypesProvider typesProvider,
            Func<Type, IDbSeeder> seederFactory)
        {
            this.commandNamesProvider = commandNamesProvider ?? throw new ArgumentNullException(nameof(commandNamesProvider));
            this.typesProvider = typesProvider ?? throw new ArgumentNullException(nameof(typesProvider));
            this.seederFactory = seederFactory ?? throw new ArgumentNullException(nameof(seederFactory));
        }

        /// <inheritdoc/>
        public async Task<object> RunAsync(string commandName)
        {
            var name = commandName ?? " ";

            var matchedNames = this.commandNamesProvider.CommandNames
                .Where(n => n.IndexOf(name, StringComparison.InvariantCultureIgnoreCase) == 0)
                .ToArray();

            if (matchedNames.Length < 1)
            {
                throw new CommandNotFoundException(commandName);
            }
            else if (matchedNames.Length > 1 && matchedNames.Count(n => n.ToUpperInvariant() == name.ToUpperInvariant()) != 1)
            {
                throw new AmbiguousCommandException(commandName, matchedNames);
            }
            else
            {
                name = matchedNames.Single(n => n.ToUpperInvariant() == name.ToUpperInvariant());
                var seederType = this.typesProvider.GetTypes().Single(t => t.Name == $"I{name}DbSeeder");

                var seeder = this.seederFactory(seederType);
                await seeder.SeedAsync().ConfigureAwait(false);
            }

            return true;
        }
    }
}
