﻿/// <summary>
/// See http://stackoverflow.com/questions/28099669/intercept-async-method-that-returns-generic-task-via-dynamicproxy
/// </summary>
namespace ProcessingTools.Interceptors
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using Ninject.Extensions.Interception;
    using ProcessingTools.Contracts;

    public class AsyncExceptionHandlingInterceptor : IInterceptor
    {
        private static readonly MethodInfo HandleAsyncMethodInfo = typeof(AsyncExceptionHandlingInterceptor)
            .GetMethod(nameof(HandleAsyncWithResult), BindingFlags.Instance | BindingFlags.NonPublic);

        private readonly IExceptionHandler handler;

        public AsyncExceptionHandlingInterceptor(IExceptionHandler handler)
        {
            this.handler = handler;
        }

        private enum MethodType
        {
            Synchronous,
            AsyncAction,
            AsyncFunction
        }

        public void Intercept(IInvocation invocation)
        {
            var delegateType = this.GetDelegateType(invocation.Request.Method);
            switch (delegateType)
            {
                case MethodType.Synchronous:
                    this.handler.HandleExceptions(() => invocation.Proceed());
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

        private MethodType GetDelegateType(MethodInfo method)
        {
            var returnType = method.ReturnType;
            if (returnType == typeof(Task))
            {
                return MethodType.AsyncAction;
            }

            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                return MethodType.AsyncFunction;
            }

            return MethodType.Synchronous;
        }

        private async Task HandleAsync(Task task)
        {
            await this.handler.HandleExceptions(async () => await task);
        }

        private async Task<T> HandleAsyncWithResult<T>(Task<T> task)
        {
            return await this.handler.HandleExceptions(async () => await task);
        }
    }
}
