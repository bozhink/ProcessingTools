namespace ProcessingTools.DbSeeder.Core
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts;

    internal class Sandbox : ISandbox
    {
        private readonly ILogger logger;

        public Sandbox(ILogger<Sandbox> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            await Task.Run(() =>
            {
                try
                {
                    action.Invoke();
                }
                catch (AggregateException e)
                {
                    foreach (var i in e.InnerExceptions)
                    {
                        this.logger.LogError(exception: i, message: "\n");
                    }
                }
                catch (Exception e)
                {
                    this.logger.LogError(exception: e, message: string.Empty);
                }
            })
            .ConfigureAwait(false);
        }

        public async Task RunAsync(Func<Task> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            await Task.Run(() =>
            {
                try
                {
                    function.Invoke().Wait();
                }
                catch (AggregateException e)
                {
                    foreach (var i in e.InnerExceptions)
                    {
                        this.logger.LogError(exception: i, message: "\n");
                    }
                }
                catch (Exception e)
                {
                    this.logger.LogError(exception: e, message: string.Empty);
                }
            })
            .ConfigureAwait(false);
        }

        public async Task<T> RunAsync<T>(Func<Task<T>> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return await Task.Run<T>(() =>
            {
                try
                {
                    return function.Invoke().Result;
                }
                catch (AggregateException e)
                {
                    foreach (var i in e.InnerExceptions)
                    {
                        this.logger.LogError(exception: i, message: "\n");
                    }

                    throw;
                }
                catch (Exception e)
                {
                    this.logger.LogError(exception: e, message: string.Empty);
                    throw;
                }
            })
            .ConfigureAwait(false);
        }
    }
}
