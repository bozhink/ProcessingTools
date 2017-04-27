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

        public Task Run(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return Task.Run(() =>
            {
                try
                {
                    action.Invoke();
                }
                catch (AggregateException e)
                {
                    foreach (var i in e.InnerExceptions)
                    {
                        this.logger?.Log(i, "\n");
                    }
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                }
            });
        }

        public Task Run(Func<Task> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Run(() =>
            {
                try
                {
                    function.Invoke().Wait();
                }
                catch (AggregateException e)
                {
                    foreach (var i in e.InnerExceptions)
                    {
                        this.logger?.Log(i, "\n");
                    }
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                }
            });
        }

        public Task<T> Run<T>(Func<Task<T>> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Run(() =>
            {
                try
                {
                    return function.Invoke().Result;
                }
                catch (AggregateException e)
                {
                    foreach (var i in e.InnerExceptions)
                    {
                        this.logger?.Log(i, "\n");
                    }

                    throw e;
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                    throw e;
                }
            });
        }
    }
}
