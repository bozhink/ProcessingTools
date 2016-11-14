namespace ProcessingTools.Interceptors
{
    using System;
    using System.IO;
    using Ninject.Extensions.Interception;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    /// <summary>
    /// Logs warning that a given file already exists.
    /// It is supposed that the full file name is passed as first parameter of the call.
    /// </summary>
    public class FileExistsRaiseWarningInterceptor : IInterceptor
    {
        private readonly ILogger logger;

        public FileExistsRaiseWarningInterceptor(ILogger logger)
        {
            this.logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Request.Arguments.Length < 1)
            {
                throw new InvalidOperationException($"{nameof(FileExistsRaiseWarningInterceptor)} requires invocation with at least 1 argument");
            }

            var fullName = (string)invocation.Request.Arguments[0];
            if (File.Exists(fullName))
            {
                this.logger?.Log(LogType.Warning, "Output file '{0}' already exists.", fullName);
            }

            invocation.Proceed();
        }
    }
}
