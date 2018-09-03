// <copyright file="AsyncExceptionHandlingInterceptor.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

// See http://stackoverflow.com/questions/28099669/intercept-async-method-that-returns-generic-task-via-dynamicproxy
namespace ProcessingTools.Ninject.Interceptors
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using global::Ninject.Extensions.Interception;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Async exception handling interceptor.
    /// </summary>
    public class AsyncExceptionHandlingInterceptor : IInterceptor
    {
        private static readonly MethodInfo HandleAsyncMethodInfo = typeof(AsyncExceptionHandlingInterceptor)
            .GetMethod(nameof(HandleAsyncWithResult), BindingFlags.Instance | BindingFlags.NonPublic);

        private readonly ISandbox sandbox;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncExceptionHandlingInterceptor"/> class.
        /// </summary>
        /// <param name="sandbox">Sandbox to wrap executing code.</param>
        public AsyncExceptionHandlingInterceptor(ISandbox sandbox)
        {
            this.sandbox = sandbox ?? throw new ArgumentNullException(nameof(sandbox));
        }

        /// <inheritdoc/>
        public void Intercept(IInvocation invocation)
        {
            var delegateType = invocation.Request.Method.GetDelegateType();
            switch (delegateType)
            {
                case MethodType.Synchronous:
                    this.sandbox.RunAsync(() => invocation.Proceed());
                    break;

                case MethodType.AsyncAction:
                    invocation.Proceed();
                    invocation.ReturnValue = this.HandleAsync((Task)invocation.ReturnValue);
                    break;

                case MethodType.AsyncFunction:
                    invocation.Proceed();
                    this.ExecuteHandleAsyncWithResultUsingReflection(invocation);
                    break;

                default:
                    throw new NotSupportedException($"{nameof(MethodType)}.{delegateType.ToString()} is not supported.");
            }
        }

        private void ExecuteHandleAsyncWithResultUsingReflection(IInvocation invocation)
        {
            var resultType = invocation.Request.Method.ReturnType.GetGenericArguments()[0];
            var method = HandleAsyncMethodInfo.MakeGenericMethod(resultType);
            invocation.ReturnValue = method.Invoke(this, new[] { invocation.ReturnValue });
        }

        private async Task HandleAsync(Task task)
        {
            await this.sandbox.RunAsync(() => task).ConfigureAwait(false);
        }

        private async Task<T> HandleAsyncWithResult<T>(Task<T> task)
        {
            return await this.sandbox.RunAsync(() => task).ConfigureAwait(false);
        }
    }
}
