namespace ProcessingTools.Web.Api
{
    using System;
    using System.Web;
    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectConfig
    {
        public static Action<IKernel> DependenciesRegistration => kernel =>
        {
            kernel
                .Bind<Data.Contracts.IDataDbContext>()
                .To<Data.DataDbContext>();
            kernel
                .Bind(typeof(Data.Repositories.Contracts.IDataRepository<>))
                .To(typeof(Data.Repositories.DataRepository<>));
            kernel
                .Bind<Services.Data.Contracts.IInstitutionsDataService>()
                .To<Services.Data.InstitutionsDataService>();
            kernel
                .Bind<Services.Data.Contracts.IProductsDataService>()
                .To<Services.Data.ProductsDataService>();

            kernel
                .Bind<Geo.Data.Contracts.IGeoDbContext>()
                .To<Geo.Data.GeoDbContext>();
            kernel
                .Bind(typeof(Geo.Data.Repositories.Contracts.IGeoDataRepository<>))
                .To(typeof(Geo.Data.Repositories.GeoDataRepository<>));
            kernel
                .Bind<Geo.Services.Data.Contracts.IGeoEpithetsDataService>()
                .To<Geo.Services.Data.Services.GeoEpithetsDataService>();
            kernel
                .Bind<Geo.Services.Data.Contracts.IGeoNamesDataService>()
                .To<Geo.Services.Data.Services.GeoNamesDataService>();

            kernel
                .Bind<Bio.Data.Contracts.IBioDbContext>()
                .To<Bio.Data.BioDbContext>();
            kernel
                .Bind(typeof(Bio.Data.Repositories.Contracts.IBioDataRepository<>))
                .To(typeof(Bio.Data.Repositories.BioDataRepository<>));
            kernel
                .Bind<Bio.Services.Data.Contracts.IMorphologicalEpithetsDataService>()
                .To<Bio.Services.Data.Services.MorphologicalEpithetsDataService>();

            kernel
                .Bind<MediaType.Data.Contracts.IMediaTypesDbContext>()
                .To<MediaType.Data.MediaTypesDbContext>();
            kernel
                .Bind(typeof(MediaType.Data.Repositories.Contracts.IMediaTypesRepository<>))
                .To(typeof(MediaType.Data.Repositories.MediaTypesRepository<>));
            ////kernel
            ////    .Bind<MediaType.Services.Data.Contracts.IMediaTypeDataService>()
            ////    .To<MediaType.Services.Data.Services.MediaTypeDataServiceStaticDictionary>();
            ////kernel
            ////    .Bind<MediaType.Services.Data.Contracts.IMediaTypeDataService>()
            ////    .To<MediaType.Services.Data.Services.MediaTypeDataServiceWindowsRegistry>();
            kernel
                .Bind<MediaType.Services.Data.Contracts.IMediaTypeDataService>()
                .To<MediaType.Services.Data.Services.MediaTypeDataService>();

            kernel
                .Bind<Bio.Environments.Data.Contracts.IBioEnvironmentsDbContext>()
                .To<Bio.Environments.Data.BioEnvironmentsDbContext>();
            kernel
                .Bind(typeof(Bio.Environments.Data.Repositories.Contracts.IBioEnvironmentsRepository<>))
                .To(typeof(Bio.Environments.Data.Repositories.BioEnvironmentsRepository<>));
            kernel
                .Bind<Bio.Environments.Services.Data.Contracts.IEnvoTermsDataService>()
                .To<Bio.Environments.Services.Data.Services.EnvoTermsDataService>();

            kernel
                .Bind<Common.Providers.Contracts.IRandomProvider>()
                .To<Common.Providers.RandomProvider>();

            kernel
                .Bind<Bio.ServiceClient.ExtractHcmr.Contracts.IExtractHcmrDataRequester>()
                .To<Bio.ServiceClient.ExtractHcmr.ExtractHcmrDataRequester>();

            kernel
                .Bind<Bio.Taxonomy.ServiceClient.CatalogueOfLife.Contracts.ICatalogueOfLifeDataRequester>()
                .To<Bio.Taxonomy.ServiceClient.CatalogueOfLife.CatalogueOfLifeDataRequester>();

            kernel
                .Bind<Bio.Taxonomy.ServiceClient.Gbif.Contracts.IGbifDataRequester>()
                .To<Bio.Taxonomy.ServiceClient.Gbif.GbifDataRequester>();

            kernel
                .Bind<Bio.Taxonomy.Services.Data.Contracts.IAphiaTaxaClassificationDataService>()
                .To<Bio.Taxonomy.Services.Data.Services.AphiaTaxaClassificationDataService>();
            kernel
                .Bind<Bio.Taxonomy.Services.Data.Contracts.ICatalogueOfLifeTaxaClassificationDataService>()
                .To<Bio.Taxonomy.Services.Data.Services.CatalogueOfLifeTaxaClassificationDataService>();
            kernel
                .Bind<Bio.Taxonomy.Services.Data.Contracts.IGbifTaxaClassificationDataService>()
                .To<Bio.Taxonomy.Services.Data.Services.GbifTaxaClassificationDataService>();
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

            ////kernel.Bind(b => b
            ////    .From(Assemblies.MediaTypeDataServices)
            ////    .SelectAllClasses()
            ////    .BindDefaultInterface());

            ////kernel.Bind(b => b
            ////    .From(Assemblies.TaxonomicDataServices)
            ////    .SelectAllClasses()
            ////    .BindDefaultInterface());
        }
    }
}