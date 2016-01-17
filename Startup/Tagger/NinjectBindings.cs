namespace ProcessingTools.MainProgram
{
    using Ninject.Modules;

    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            /*
             * Database bindings.
             */

            this.Bind<Data.Contracts.IDataDbContext>()
                .To<Data.DataDbContext>();
            this.Bind(typeof(Data.Repositories.IDataRepository<>))
                .To(typeof(Data.Repositories.EfDataGenericRepository<>));

            this.Bind<Bio.Data.Contracts.IBioDbContext>()
                .To<Bio.Data.BioDbContext>();
            this.Bind(typeof(Bio.Data.Repositories.IBioDataRepository<>))
                .To(typeof(Bio.Data.Repositories.EfBioDataGenericRepository<>));

            this.Bind<Bio.Biorepositories.Data.Contracts.IBiorepositoriesDbContext>()
                .To<Bio.Biorepositories.Data.BiorepositoriesDbContext>();
            this.Bind(typeof(Bio.Biorepositories.Data.Repositories.IBiorepositoriesRepository<>))
                .To(typeof(Bio.Biorepositories.Data.Repositories.EfBiorepositoriesGenericRepository<>));
            this.Bind<Bio.Biorepositories.Data.Contracts.IBiorepositoriesDbFirstDbContext>()
                .To<Bio.Biorepositories.Data.BiorepositoriesDbFirstDbContext>();
            this.Bind(typeof(Bio.Biorepositories.Data.Repositories.IBiorepositoriesDbFirstGenericRepository<>))
                .To(typeof(Bio.Biorepositories.Data.Repositories.EfBiorepositoriesDbFirstGenericRepository<>));

            this.Bind<Bio.Environments.Data.Contracts.IBioEnvironmentsDbContext>()
                .To<Bio.Environments.Data.BioEnvironmentsDbContext>();
            this.Bind(typeof(Bio.Environments.Data.Repositories.IBioEnvironmentsRepository<>))
                .To(typeof(Bio.Environments.Data.Repositories.BioEnvironmentsGenericRepository<>));

            this.Bind<Geo.Data.Contracts.IGeoDbContext>()
                .To<Geo.Data.GeoDbContext>();
            this.Bind(typeof(Geo.Data.Repositories.IGeoDataRepository<>))
                .To(typeof(Geo.Data.Repositories.EfGeoDataGenericRepository<>));

            this.Bind<MediaType.Data.Contracts.IMediaTypesDbContext>()
                .To<MediaType.Data.MediaTypesDbContext>();
            this.Bind(typeof(MediaType.Data.Repositories.IMediaTypesRepository<>))
                .To(typeof(MediaType.Data.Repositories.MediaTypesGenericRepository<>));

            /*
             * Service clients bindings.
             */

            this.Bind<Bio.ServiceClient.ExtractHcmr.Contracts.IExtractHcmrDataRequester>()
                .To<Bio.ServiceClient.ExtractHcmr.ExtractHcmrDataRequester>();

            this.Bind<Bio.Taxonomy.ServiceClient.CatalogueOfLife.Contracts.ICatalogueOfLifeDataRequester>()
                .To<Bio.Taxonomy.ServiceClient.CatalogueOfLife.CatalogueOfLifeDataRequester>();

            this.Bind<Bio.Taxonomy.ServiceClient.Gbif.Contracts.IGbifDataRequester>()
                .To<Bio.Taxonomy.ServiceClient.Gbif.GbifDataRequester>();

            this.Bind<Bio.Taxonomy.ServiceClient.PaleobiologyDatabase.Contracts.IPaleobiologyDatabaseDataRequester>()
                .To<Bio.Taxonomy.ServiceClient.PaleobiologyDatabase.PaleobiologyDatabaseDataRequester>();

            /*
             * Services bindings.
             */

            this.Bind<Bio.Biorepositories.Services.Data.Contracts.IBiorepositoriesDataService>()
                .To<Bio.Biorepositories.Services.Data.BiorepositoriesDataService>();

            this.Bind<Bio.Environments.Services.Data.Contracts.IEnvoTermsDataService>()
                .To<Bio.Environments.Services.Data.EnvoTermsDataService>();

            this.Bind<Bio.Services.Data.Contracts.IMorphologicalEpithetsDataService>()
                .To<Bio.Services.Data.MorphologicalEpithetsDataService>();

            this.Bind<Bio.Taxonomy.Services.Data.Contracts.IAboveGenusTaxaRankDataService>()
                .To<Bio.Taxonomy.Services.Data.AboveGenusTaxaRankDataService>();
            this.Bind<Bio.Taxonomy.Services.Data.Contracts.IAphiaTaxaClassificationDataService>()
                .To<Bio.Taxonomy.Services.Data.AphiaTaxaClassificationDataService>();
            this.Bind<Bio.Taxonomy.Services.Data.Contracts.ICatalogueOfLifeTaxaClassificationDataService>()
                .To<Bio.Taxonomy.Services.Data.CatalogueOfLifeTaxaClassificationDataService>();
            this.Bind<Bio.Taxonomy.Services.Data.Contracts.IGbifTaxaClassificationDataService>()
                .To<Bio.Taxonomy.Services.Data.GbifTaxaClassificationDataService>();
            this.Bind<Bio.Taxonomy.Services.Data.Contracts.ILocalDbTaxaRankDataService>()
                .To<Bio.Taxonomy.Services.Data.LocalDbTaxaRankDataService>();
            this.Bind<Bio.Taxonomy.Services.Data.Contracts.ISuffixHigherTaxaRankDataService>()
                .To<Bio.Taxonomy.Services.Data.SuffixHigherTaxaRankDataService>();

            this.Bind<Geo.Services.Data.Contracts.IGeoEpithetsDataService>()
                .To<Geo.Services.Data.GeoEpithetsDataService>();
            this.Bind<Geo.Services.Data.Contracts.IGeoNamesDataService>()
                .To<Geo.Services.Data.GeoNamesDataService>();

            this.Bind<MediaType.Services.Data.Contracts.IMediaTypeDataService>()
                .To<MediaType.Services.Data.MediaTypeDataService>();

            this.Bind<Services.Data.Contracts.IInstitutionsDataService>()
                .To<Services.Data.InstitutionsDataService>();
            this.Bind<Services.Data.Contracts.IProductsDataService>()
                .To<Services.Data.ProductsDataService>();

            /*
             * Harvesters bindings.
             */

            this.Bind<Bio.Harvesters.Contracts.IBiorepositoryInstitutionalCodesHarvester>()
                .To<Bio.Harvesters.BiorepositoryInstitutionalCodesHarvester>();
            this.Bind<Bio.Harvesters.Contracts.IBiorepositoryInstitutionHarvester>()
                .To<Bio.Harvesters.BiorepositoryInstitutionHarvester>();
            this.Bind<Bio.Harvesters.Contracts.IEnvoTermsHarvester>()
                .To<Bio.Harvesters.EnvoTermsHarvester>();
            this.Bind<Bio.Harvesters.Contracts.IExtractHcmrHarvester>()
                .To<Bio.Harvesters.ExtractHcmrHarvester>();
            ////this.Bind<Bio.Harvesters.Contracts.IInstitutionalCodesHarvester>()
            ////    .To<Bio.Harvesters.InstitutionalCodesHarvester>();
            this.Bind<Bio.Harvesters.Contracts.IMorphologicalEpithetHarvester>()
                .To<Bio.Harvesters.MorphologicalEpithetsHarvester>();
            ////this.Bind<Bio.Harvesters.Contracts.ISpecimenCodesHarvester>()
            ////    .To<Bio.Harvesters.SpecimenCodesHarvester>();
            this.Bind<Bio.Harvesters.Contracts.ISpecimenCountHarvester>()
                .To<Bio.Harvesters.SpecimenCountHarvester>();

            this.Bind<Bio.Taxonomy.Harvesters.Contracts.IHigherTaxaHarvester>()
                .To<Bio.Taxonomy.Harvesters.HigherTaxaHarvester>();

            ////this.Bind<Geo.Harvesters.Contracts.ICoordinatesHarvester>()
            ////    .To<Geo.Harvesters.CoordinatesHarvester>();
            this.Bind<Geo.Harvesters.Contracts.IGeoEpithetsHarvester>()
                .To<Geo.Harvesters.GeoEpithetsHarvester>();
            this.Bind<Geo.Harvesters.Contracts.IGeoNamesHarvester>()
                .To<Geo.Harvesters.GeoNamesHarvester>();

            this.Bind<Harvesters.Contracts.IAltitudesHarvester>()
                .To<Harvesters.AltitudesHarvester>();
            this.Bind<Harvesters.Contracts.IDatesHarvester>()
                .To<Harvesters.DatesHarvester>();
            this.Bind<Harvesters.Contracts.IGeographicDeviationsHarvester>()
                .To<Harvesters.GeographicDeviationsHarvester>();
            this.Bind<Harvesters.Contracts.IInstitutionsHarvester>()
                .To<Harvesters.InstitutionsHarvester>();
            this.Bind<Harvesters.Contracts.INlmExternalLinksHarvester>()
                .To<Harvesters.NlmExternalLinksHarvester>();
            this.Bind<Harvesters.Contracts.IProductsHarvester>()
                .To<Harvesters.ProductsHarvester>();
            this.Bind<Harvesters.Contracts.IQuantitiesHarvester>()
                .To<Harvesters.QuantitiesHarvester>();

            /*
             * Infrastructure bindings.
             */

            this.Bind<ProcessingTools.Contracts.IXPathProvider>()
                .To<ProcessingTools.BaseLibrary.XPathProvider>();
        }
    }
}