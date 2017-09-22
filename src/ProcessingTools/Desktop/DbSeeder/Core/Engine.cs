namespace ProcessingTools.DbSeeder.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.DbSeeder.Contracts.Core;
    using ProcessingTools.Processors.Contracts;

    public class Engine : IEngine
    {
        private readonly ICommandRunner commandRunner;
        private readonly ISandbox sandbox;
        private readonly IHelpProvider helpProvider;

        public Engine(ICommandRunner commandRunner, ISandbox sandbox, IHelpProvider helpProvider)
        {
            this.commandRunner = commandRunner ?? throw new ArgumentNullException(nameof(commandRunner));
            this.sandbox = sandbox ?? throw new ArgumentNullException(nameof(sandbox));
            this.helpProvider = helpProvider;
        }

        public async Task Run(string[] args)
        {
            if (args == null || args.Length < 1)
            {
                await this.helpProvider.GetHelpAsync().ConfigureAwait(false);
                return;
            }

            var tasks = new ConcurrentQueue<Task>();
            foreach (var arg in args)
            {
                tasks.Enqueue(this.sandbox.RunAsync(action: () => this.commandRunner.RunAsync(arg).Wait()));
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}
