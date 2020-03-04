// <copyright file="Engine.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services;

    /// <summary>
    /// DbSeeder engine.
    /// </summary>
    public class Engine : IEngine
    {
        private readonly ICommandRunner commandRunner;
        private readonly ISandbox sandbox;
        private readonly IHelpProvider helpProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        /// <param name="commandRunner">Instance of <see cref="ICommandRunner"/>.</param>
        /// <param name="sandbox"></param>
        /// <param name="helpProvider"></param>
        public Engine(ICommandRunner commandRunner, ISandbox sandbox, IHelpProvider helpProvider)
        {
            this.commandRunner = commandRunner ?? throw new ArgumentNullException(nameof(commandRunner));
            this.sandbox = sandbox ?? throw new ArgumentNullException(nameof(sandbox));
            this.helpProvider = helpProvider;
        }

        /// <inheritdoc/>
        public async Task RunAsync(string[] args)
        {
            if (args is null || args.Length < 1)
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
