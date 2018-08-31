/// <summary>
/// See http://stackoverflow.com/questions/28099669/intercept-async-method-that-returns-generic-task-via-dynamicproxy
/// </summary>
namespace ProcessingTools.Interceptors
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using Ninject.Extensions.Interception;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;

    public class AsyncExceptionHandlingInterceptor : IInterceptor
    {
        private static readonly MethodInfo HandleAsyncMethodInfo = typeof(AsyncExceptionHandlingInterceptor)
            .GetMethod(nameof(HandleAsyncWithResult), BindingFlags.Instance | BindingFlags.NonPublic);

        private readonly ISandbox sandbox;

        public AsyncExceptionHandlingInterceptor(ISandbox sandbox)
        {
            this.sandbox = sandbox;
        }

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
                    throw new NotImplementedException($"{nameof(MethodType)}.{delegateType.ToString()} is not implemented");
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
