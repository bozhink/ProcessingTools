namespace ProcessingTools.Interceptors
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Ninject.Extensions.Interception;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts;

    public class CommandRunnerTimeLoggingInterceptor : IInterceptor
    {
        private readonly ILogger logger;

        public CommandRunnerTimeLoggingInterceptor(ILogger logger)
        {
            this.logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Request.Target is ICommandRunner &&
                invocation.Request.Method.Name == nameof(ICommandRunner.RunAsync))
            {
                var target = invocation.Request.Target as ICommandRunner;
                var commandName = invocation.Request.Arguments.Single().ToString();

                var timer = new Stopwatch();
                timer.Start();

                var invocationMessage = string.Format(
                    "{0}.{1}({2})",
                    invocation.Request.Target.GetType().Name,
                    invocation.Request.Method.Name,
                    commandName);

                try
                {
                    invocation.ReturnValue = Task.FromResult(target.RunAsync(commandName).Result);
                }
                catch (AggregateException e)
                {
                    foreach (var i in e.InnerExceptions)
                    {
                        this.logger?.Log(i, "\nInvocation: {0}\n", invocationMessage);
                    }

                    invocation.ReturnValue = Task.FromResult<object>(false);
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, "\nInvocation: {0}\n", invocationMessage);
                    invocation.ReturnValue = Task.FromResult<object>(false);
                }

                this.logger?.Log(
                    LogType.Info,
                    "Elapsed time for execution of {0}: {1}.",
                    invocationMessage,
                    timer.Elapsed);
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
