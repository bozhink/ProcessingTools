namespace ProcessingTools.Tagger.Interceptors
{
    using Ninject.Extensions.Interception;

    public class ReadXmlFileWellFormReqularizeInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}