namespace ProcessingTools.DbSeeder.Core
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.DbSeeder.Contracts.Providers;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

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

        public async Task<object> Run(string commandName)
        {
            var name = commandName ?? " ";

            var matchedNames = this.commandNamesProvider.CommandNames
                .Where(n => n.ToLower().IndexOf(name.ToLower()) == 0)
                .ToArray();

            if (matchedNames.Length < 1)
            {
                throw new Exception($"Invalid command '{commandName}'");
            }
            else if (matchedNames.Length > 1 && matchedNames.Count(n => n.ToLower() == name.ToLower()) != 1)
            {
                throw new Exception($"Ambiguous command '{commandName}'. Possible matches: {string.Join(", ", matchedNames)}");
            }
            else
            {
                name = matchedNames.Single(n => n.ToLower() == name.ToLower());
                var seederType = this.typesProvider.GetTypes()
                    .Single(t => t.Name == $"I{name}DbSeeder");

                var seeder = this.seederFactory(seederType);
                await seeder.Seed();
            }

            return true;
        }
    }
}
