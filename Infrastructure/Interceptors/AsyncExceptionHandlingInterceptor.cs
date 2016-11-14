/// <summary>
/// See http://stackoverflow.com/questions/28099669/intercept-async-method-that-returns-generic-task-via-dynamicproxy
/// </summary>
namespace ProcessingTools.Interceptors
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using Ninject.Extensions.Interception;

    public class AsyncExceptionHandlingInterceptor : IInterceptor
    {
        private static readonly MethodInfo handleAsyncMethodInfo = typeof(AsyncExceptionHandlingInterceptor)
            .GetMethod(nameof(HandleAsyncWithResult), BindingFlags.Instance | BindingFlags.NonPublic);

        private readonly IExceptionHandler _handler;

        public AsyncExceptionHandlingInterceptor(IExceptionHandler handler)
        {
            _handler = handler;
        }

        public void Intercept(IInvocation invocation)
        {
            var delegateType = GetDelegateType(invocation);
            if (delegateType == MethodType.Synchronous)
            {
                _handler.HandleExceptions(() => invocation.Proceed());
            }
            if (delegateType == MethodType.AsyncAction)
            {
                invocation.Proceed();
                invocation.ReturnValue = HandleAsync((Task)invocation.ReturnValue);
            }
            if (delegateType == MethodType.AsyncFunction)
            {
                invocation.Proceed();
                ExecuteHandleAsyncWithResultUsingReflection(invocation);
            }
        }

        private void ExecuteHandleAsyncWithResultUsingReflection(IInvocation invocation)
        {
            var resultType = invocation.Request.Method.ReturnType.GetGenericArguments()[0];
            var mi = handleAsyncMethodInfo.MakeGenericMethod(resultType);
            invocation.ReturnValue = mi.Invoke(this, new[] { invocation.ReturnValue });
        }

        private async Task HandleAsync(Task task)
        {
            await _handler.HandleExceptions(async () => await task);
        }

        private async Task<T> HandleAsyncWithResult<T>(Task<T> task)
        {
            return await _handler.HandleExceptions(async () => await task);
        }

        private MethodType GetDelegateType(IInvocation invocation)
        {
            var returnType = invocation.Request.Method.ReturnType;
            if (returnType == typeof(Task))
                return MethodType.AsyncAction;
            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
                return MethodType.AsyncFunction;
            return MethodType.Synchronous;
        }

        private enum MethodType
        {
            Synchronous,
            AsyncAction,
            AsyncFunction
        }

        public interface IExceptionHandler
        {
            Task HandleExceptions(Action p);

            Task<T> HandleExceptions<T>(Func<Task<T>> p);
        }
    }
}
