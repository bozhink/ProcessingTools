namespace ProcessingTools.Interceptors
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Logging;
    using Ninject.Extensions.Interception;

    public class FileNotFoundInterceptor : IInterceptor
    {
        private readonly ILogger logger;

        public FileNotFoundInterceptor(ILogger<FileNotFoundInterceptor> logger)
        {
            this.logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Request.Arguments.Length < 1)
            {
                throw new InvalidOperationException($"{nameof(FileNotFoundInterceptor)} requires invocation with at least 1 argument");
            }

            var fileName = (string)invocation.Request.Arguments[0];
            if (!File.Exists(fileName))
            {
                string message = $"File '{fileName}' does not exist.";
                this.logger?.LogError(message);
                throw new FileNotFoundException(message: message, fileName: fileName);
            }

            invocation.Proceed();
        }
    }
}
