/// <summary>
/// https://blog.kulman.sk/intercepting-methods-with-ninject-for-error-logging/
/// </summary>
namespace ProcessingTools.Interceptors
{
    using Ninject;
    using Ninject.Extensions.Interception;
    using Ninject.Extensions.Interception.Attributes;
    using Ninject.Extensions.Interception.Request;

    public class LogExceptionAttribute : InterceptAttribute
    {
        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            return request.Context.Kernel.Get<ExceptionLoggingInterceptor>();
        }
    }
}
