namespace ProcessingTools.DbSeeder.Core
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.DbSeeder.Contracts.Providers;
    using ProcessingTools.DbSeeder.Contracts.Seeders;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Contracts.Processors;

    internal class SeedCommandRunner : ICommandRunner
    {
        private readonly ICommandNamesProvider commandNamesProvider;
        private readonly ITypesProvider typesProvider;
        private readonly Func<Type, IDbSeeder> seederFactory;

        public SeedCommandRunner(
            ICommandNamesProvider commandNamesProvider,
            ITypesProvider typesProvider,
            Func<Type, IDbSeeder> seederFactory)
        {
            this.commandNamesProvider = commandNamesProvider ?? throw new ArgumentNullException(nameof(commandNamesProvider));
            this.typesProvider = typesProvider ?? throw new ArgumentNullException(nameof(typesProvider));
            this.seederFactory = seederFactory ?? throw new ArgumentNullException(nameof(seederFactory));
        }

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
            else if (matchedNames.Length > 1 && matchedNames.Count(n => string.Compare(n, name, true) == 0) != 1)
            {
                throw new AmbiguousCommandException(commandName, matchedNames);
            }
            else
            {
                name = matchedNames.Single(n => string.Compare(n, name, true) == 0);
                var seederType = this.typesProvider.GetTypes().Single(t => t.Name == $"I{name}DbSeeder");

                var seeder = this.seederFactory(seederType);
                await seeder.Seed().ConfigureAwait(false);
            }

            return true;
        }
    }
}
