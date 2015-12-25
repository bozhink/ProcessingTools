namespace ProcessingTools.Web.Api
{
    using System;
    using System.Web;

    using Bio.Environments.Services.Data;
    using Bio.Environments.Services.Data.Contracts;
    using Bio.ServiceClient.ExtractHcmr;
    using Bio.ServiceClient.ExtractHcmr.Contracts;
    using Bio.Services.Data;
    using Bio.Services.Data.Contracts;
    using Bio.Taxonomy.ServiceClient.CatalogueOfLife;
    using Bio.Taxonomy.ServiceClient.CatalogueOfLife.Contracts;
    using Bio.Taxonomy.ServiceClient.Gbif;
    using Bio.Taxonomy.ServiceClient.Gbif.Contracts;
    using Bio.Taxonomy.Services.Data;
    using Bio.Taxonomy.Services.Data.Contracts;
    using Common.Providers;
    using Common.Providers.Contracts;
    using MediaType.Services.Data;
    using MediaType.Services.Data.Contracts;
    using Ninject;
    using Ninject.Web.Common;
    using Services.Data;
    using Services.Data.Contracts;

    public static class NinjectConfig
    {
        public static Action<IKernel> DependenciesRegistration => kernel =>
        {
            kernel.Bind<Data.Contracts.IDataDbContext>().To<Data.DataDbContext>();
            kernel.Bind(typeof(Data.Repositories.IDataRepository<>)).To(typeof(Data.Repositories.EfDataGenericRepository<>));
            kernel.Bind<IInstitutionsDataService>().To<InstitutionsDataService>();
            kernel.Bind<IProductsDataService>().To<ProductsDataService>();

            kernel.Bind<Bio.Data.Contracts.IBioDbContext>().To<Bio.Data.BioDbContext>();
            kernel.Bind(typeof(Bio.Data.Repositories.IBioDataRepository<>)).To(typeof(Bio.Data.Repositories.EfBioDataGenericRepository<>));
            kernel.Bind<IMorphologicalEpithetsDataService>().To<MorphologicalEpithetsDataService>();

            kernel.Bind<MediaType.Data.Contracts.IMediaTypesDbContext>().To<MediaType.Data.MediaTypesDbContext>();
            kernel.Bind(typeof(MediaType.Data.Repositories.IMediaTypesRepository<>)).To(typeof(MediaType.Data.Repositories.MediaTypesGenericRepository<>));
            ////kernel.Bind<IMediaTypeDataService>().To<MediaTypeDataServiceStaticDictionary>();
            ////kernel.Bind<IMediaTypeDataService>().To<MediaTypeDataServiceWindowsRegistry>();
            kernel.Bind<IMediaTypeDataService>().To<MediaTypeDataService>();

            kernel.Bind<Bio.Environments.Data.Contracts.IBioEnvironmentsDbContext>().To<Bio.Environments.Data.BioEnvironmentsDbContext>();
            kernel.Bind(typeof(Bio.Environments.Data.Repositories.IBioEnvironmentsRepository<>)).To(typeof(Bio.Environments.Data.Repositories.BioEnvironmentsGenericRepository<>));
            kernel.Bind<IEnvoTermsDataService>().To<EnvoTermsDataService>();

            kernel.Bind<IRandomProvider>().To<RandomProvider>();

            kernel.Bind<IExtractHcmrDataRequester>().To<ExtractHcmrDataRequester>();
            kernel.Bind<ICatalogueOfLifeDataRequester>().To<CatalogueOfLifeDataRequester>();
            kernel.Bind<IGbifDataRequester>().To<GbifDataRequester>();

            kernel.Bind<IAphiaTaxaClassificationDataService>().To<AphiaTaxaClassificationDataService>();
            kernel.Bind<ICatalogueOfLifeTaxaClassificationDataService>().To<CatalogueOfLifeTaxaClassificationDataService>();
            kernel.Bind<IGbifTaxaClassificationDataService>().To<GbifTaxaClassificationDataService>();
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