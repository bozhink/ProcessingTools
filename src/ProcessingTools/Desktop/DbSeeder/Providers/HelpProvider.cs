namespace ProcessingTools.DbSeeder.Providers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.DbSeeder.Contracts.Providers;

    internal class HelpProvider : IHelpProvider
    {
        private readonly IReporter reporter;
        private readonly ICommandNamesProvider commandNamesProvider;

        public HelpProvider(IReporter reporter, ICommandNamesProvider commandNamesProvider)
        {
            if (reporter == null)
            {
                throw new ArgumentNullException(nameof(reporter));
            }

            if (commandNamesProvider == null)
            {
                throw new ArgumentNullException(nameof(commandNamesProvider));
            }

            this.reporter = reporter;
            this.commandNamesProvider = commandNamesProvider;
        }

        public async Task GetHelp()
        {
            this.reporter.AppendContent("Available commands:");

            this.commandNamesProvider.CommandNames
                .OrderBy(c => c)
                .ToList()
                .ForEach(c =>
                {
                    this.reporter.AppendContent($"\t{c}");
                });

            await this.reporter.MakeReport().ConfigureAwait(false);
        }
    }
}
