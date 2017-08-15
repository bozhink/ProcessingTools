/// <summary>
/// See https://blog.kulman.sk/intercepting-methods-with-ninject-for-error-logging/
/// </summary>
namespace ProcessingTools.Interceptors
{
    using System;
    using System.Text;
    using Ninject.Extensions.Interception;
    using ProcessingTools.Contracts;

    public class ExceptionLoggingInterceptor : IInterceptor
    {
        private readonly ILogger logger;

        public ExceptionLoggingInterceptor(ILogger logger)
        {
            this.logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                var sb = new StringBuilder();
                sb.AppendFormat(
                    "Executing {0}.{1} (",
                    invocation.Request.Target.GetType().Name,
                    invocation.Request.Method.Name);

                var parameters = invocation.Request.Method.GetParameters();
                for (int i = 0; i < invocation.Request.Arguments.Length; ++i)
                {
                    sb.AppendFormat(
                        "{0}={1},",
                        parameters[i].Name,
                        invocation.Request.Arguments[i]);
                }

                sb.AppendFormat(
                    ") {0} caught: {1})",
                    e.GetType().Name,
                    e.Message);

                this.logger?.Log(e, message: sb.ToString());
                throw;
            }
        }
    }
}
