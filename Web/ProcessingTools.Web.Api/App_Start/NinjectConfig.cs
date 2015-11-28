namespace ProcessingTools.Web.Api
{
    using System;
    using System.Web;

    using Common.Constants;
    using Common.Providers;
    using Common.Providers.Contracts;
    using Data.Common.Repositories;

    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;
    using Mediatype.Services.Data.Contracts;
    using Mediatype.Services.Data;
    using Data.Common.Contracts;
    using Mediatype.Data;

    public static class NinjectConfig
    {
        public static Action<IKernel> DependenciesRegistration => kernel =>
        {
            kernel.Bind<IDbContext>().To<MediatypesDbContext>();
            kernel.Bind(typeof(IRepository<>)).To(typeof(EfGenericRepository<>));
            kernel.Bind<IRandomProvider>().To<RandomProvider>();
        };

        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            DependenciesRegistration(kernel);

            kernel.Bind(b => b
                .From(Assemblies.MediatypeDataServices)
                .SelectAllClasses()
                .BindDefaultInterface());
        }
    }
}