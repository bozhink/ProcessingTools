namespace ProcessingTools.DbSeeder.Core
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    internal class Sandbox : ISandbox
    {
        private readonly ILogger logger;

        public Sandbox(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task Run(Action action)
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
                        this.logger?.Log(exception: i, message: "\n");
                    }
                }
                catch (Exception e)
                {
                    this.logger?.Log(exception: e, message: string.Empty);
                }
            });
        }

        public async Task Run(Func<Task> function)
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
                        this.logger?.Log(exception: i, message: "\n");
                    }
                }
                catch (Exception e)
                {
                    this.logger?.Log(exception: e, message: string.Empty);
                }
            });
        }

        public async Task<T> Run<T>(Func<Task<T>> function)
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
                        this.logger?.Log(exception: i, message: "\n");
                    }

                    throw;
                }
                catch (Exception e)
                {
                    this.logger?.Log(exception: e, message: string.Empty);
                    throw;
                }
            });
        }
    }
}
