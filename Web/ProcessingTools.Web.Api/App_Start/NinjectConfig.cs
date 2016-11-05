namespace ProcessingTools.Web.Api
{
    using System;
    using System.Web;

    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;

    public static class NinjectConfig
    {
        public static Action<IKernel> DependenciesRegistration => kernel =>
        {
            ////kernel
            ////    .Bind<MediaType.Data.Contracts.IMediaTypesDbContext>()
            ////    .To<MediaType.Data.MediaTypesDbContext>();
            ////kernel
            ////    .Bind(typeof(MediaType.Data.Repositories.Contracts.IMediaTypesRepository<>))
            ////    .To(typeof(MediaType.Data.Repositories.MediaTypesRepository<>));
            ////kernel
            ////    .Bind<MediaType.Services.Data.Contracts.IMediaTypeDataService>()
            ////    .To<MediaType.Services.Data.Services.MediaTypeDataServiceStaticDictionary>();
            ////kernel
            ////    .Bind<MediaType.Services.Data.Contracts.IMediaTypeDataService>()
            ////    .To<MediaType.Services.Data.Services.MediaTypeDataServiceWindowsRegistry>();
            kernel.Bind(b =>
            {
                b.From(MediaType.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            kernel.Bind(b =>
            {
                b.From(Bio.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            kernel.Bind(b =>
            {
                b.From(Bio.Environments.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            kernel.Bind(b =>
            {
                b.From(Geo.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            kernel.Bind(b =>
            {
                b.From(Services.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            kernel.Bind(b =>
            {
                b.From(Geo.Services.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            kernel.Bind(b =>
            {
                b.From(Bio.Services.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            kernel.Bind(b =>
            {
                b.From(MediaType.Services.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            kernel.Bind(b =>
            {
                b.From(Bio.Environments.Services.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            kernel.Bind(b =>
            {
                b.From(ProcessingTools.Common.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            kernel.Bind(b =>
            {
                b.From(Bio.ServiceClient.ExtractHcmr.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            kernel.Bind(b =>
            {
                b.From(Bio.Taxonomy.ServiceClient.CatalogueOfLife.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            kernel.Bind(b =>
            {
                b.From(Bio.Taxonomy.ServiceClient.Gbif.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            kernel.Bind(b =>
            {
                b.From(Bio.Taxonomy.ServiceClient.Gbif.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            kernel.Bind(b =>
            {
                b.From(Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            kernel.Bind(b =>
            {
                b.From(Bio.Taxonomy.Services.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });
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
        }
    }
}