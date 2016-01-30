namespace ProcessingTools.MainProgram.Settings
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
            this.Bind(typeof(Data.Repositories.Contracts.IDataRepository<>))
                .To(typeof(Data.Repositories.DataRepository<>));

            this.Bind<Bio.Data.Contracts.IBioDbContext>()
                .To<Bio.Data.BioDbContext>();
            this.Bind(typeof(Bio.Data.Repositories.Contracts.IBioDataRepository<>))
                .To(typeof(Bio.Data.Repositories.BioDataRepository<>));

            this.Bind<Bio.Biorepositories.Data.Contracts.IBiorepositoriesDbContext>()
                .To<Bio.Biorepositories.Data.BiorepositoriesDbContext>();
            this.Bind(typeof(Bio.Biorepositories.Data.Repositories.Contracts.IBiorepositoriesRepository<>))
                .To(typeof(Bio.Biorepositories.Data.Repositories.BiorepositoriesRepository<>));
            this.Bind<Bio.Biorepositories.Data.Contracts.IBiorepositoriesDbFirstDbContext>()
                .To<Bio.Biorepositories.Data.BiorepositoriesDbFirstDbContext>();
            this.Bind(typeof(Bio.Biorepositories.Data.Repositories.Contracts.IBiorepositoriesDbFirstGenericRepository<>))
                .To(typeof(Bio.Biorepositories.Data.Repositories.BiorepositoriesDbFirstGenericRepository<>));

            this.Bind<Bio.Environments.Data.Contracts.IBioEnvironmentsDbContext>()
                .To<Bio.Environments.Data.BioEnvironmentsDbContext>();
            this.Bind(typeof(Bio.Environments.Data.Repositories.Contracts.IBioEnvironmentsRepository<>))
                .To(typeof(Bio.Environments.Data.Repositories.BioEnvironmentsRepository<>));

            this.Bind<Geo.Data.Contracts.IGeoDbContext>()
                .To<Geo.Data.GeoDbContext>();
            this.Bind(typeof(Geo.Data.Repositories.Contracts.IGeoDataRepository<>))
                .To(typeof(Geo.Data.Repositories.GeoDataRepository<>));

            this.Bind<MediaType.Data.Contracts.IMediaTypesDbContext>()
                .To<MediaType.Data.MediaTypesDbContext>();
            this.Bind(typeof(MediaType.Data.Repositories.Contracts.IMediaTypesRepository<>))
                .To(typeof(MediaType.Data.Repositories.MediaTypesRepository<>));

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
                .To<Bio.Biorepositories.Services.Data.Services.BiorepositoriesDataService>();

            this.Bind<Bio.Environments.Services.Data.Contracts.IEnvoTermsDataService>()
                .To<Bio.Environments.Services.Data.Services.EnvoTermsDataService>();

            this.Bind<Bio.Services.Data.Contracts.IMorphologicalEpithetsDataService>()
                .To<Bio.Services.Data.Services.MorphologicalEpithetsDataService>();

            this.Bind<Bio.Taxonomy.Services.Data.Contracts.IAboveGenusTaxaRankDataService>()
                .To<Bio.Taxonomy.Services.Data.Services.AboveGenusTaxaRankDataService>();
            this.Bind<Bio.Taxonomy.Services.Data.Contracts.IAphiaTaxaClassificationDataService>()
                .To<Bio.Taxonomy.Services.Data.Services.AphiaTaxaClassificationDataService>();
            this.Bind<Bio.Taxonomy.Services.Data.Contracts.ICatalogueOfLifeTaxaClassificationDataService>()
                .To<Bio.Taxonomy.Services.Data.Services.CatalogueOfLifeTaxaClassificationDataService>();
            this.Bind<Bio.Taxonomy.Services.Data.Contracts.IGbifTaxaClassificationDataService>()
                .To<Bio.Taxonomy.Services.Data.Services.GbifTaxaClassificationDataService>();
            this.Bind<Bio.Taxonomy.Services.Data.Contracts.ILocalDbTaxaRankDataService>()
                .To<Bio.Taxonomy.Services.Data.Services.LocalDbTaxaRankDataService>();
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
             * Miners bindings.
             */

            this.Bind<Bio.Data.Miners.Contracts.IBiorepositoryInstitutionalCodesDataMiner>()
                .To<Bio.Data.Miners.BiorepositoryInstitutionalCodesDataMiner>();
            this.Bind<Bio.Data.Miners.Contracts.IBiorepositoryInstitutionDataMiner>()
                .To<Bio.Data.Miners.BiorepositoryInstitutionDataMiner>();
            this.Bind<Bio.Data.Miners.Contracts.IEnvoTermsDataMiner>()
                .To<Bio.Data.Miners.EnvoTermsDataMiner>();
            this.Bind<Bio.Data.Miners.Contracts.IExtractHcmrDataMiner>()
                .To<Bio.Data.Miners.ExtractHcmrDataMiner>();
            ////this.Bind<Bio.Data.Miners.Contracts.IInstitutionalCodesDataMiner>()
            ////    .To<Bio.Data.Miners.InstitutionalCodesDataMiner>();
            this.Bind<Bio.Data.Miners.Contracts.IMorphologicalEpithetsDataMiner>()
                .To<Bio.Data.Miners.MorphologicalEpithetsDataMiner>();
            ////this.Bind<Bio.Data.Miners.Contracts.ISpecimenCodesDataMiner>()
            ////    .To<Bio.Data.Miners.SpecimenCodesDataMiner>();
            this.Bind<Bio.Data.Miners.Contracts.ISpecimenCountDataMiner>()
                .To<Bio.Data.Miners.SpecimenCountDataMiner>();

            this.Bind<Bio.Data.Miners.Contracts.IHigherTaxaDataMiner>()
                .To<Bio.Data.Miners.HigherTaxaDataMiner>();

            ////this.Bind<Geo.Data.Miners.Contracts.ICoordinatesDataMiner>()
            ////    .To<Geo.Data.Miners.Contracts.CoordinatesDataMiner>();
            this.Bind<Geo.Data.Miners.Contracts.IGeoEpithetsDataMiner>()
                .To<Geo.Data.Miners.GeoEpithetsDataMiner>();
            this.Bind<Geo.Data.Miners.Contracts.IGeoNamesDataMiner>()
                .To<Geo.Data.Miners.GeoNamesDataMiner>();

            this.Bind<Data.Miners.Contracts.IAltitudesDataMiner>()
                .To<Data.Miners.AltitudesDataMiner>();
            this.Bind<Data.Miners.Contracts.IDatesDataMiner>()
                .To<Data.Miners.DatesDataMiner>();
            this.Bind<Data.Miners.Contracts.IGeographicDeviationsDataMiner>()
                .To<Data.Miners.GeographicDeviationsDataMiner>();
            this.Bind<Data.Miners.Contracts.IInstitutionsDataMiner>()
                .To<Data.Miners.InstitutionsDataMiner>();
            this.Bind<Data.Miners.Contracts.INlmExternalLinksDataMiner>()
                .To<Data.Miners.NlmExternalLinksDataMiner>();
            this.Bind<Data.Miners.Contracts.IProductsDataMiner>()
                .To<Data.Miners.ProductsDataMiner>();
            this.Bind<Data.Miners.Contracts.IQuantitiesDataMiner>()
                .To<Data.Miners.QuantitiesDataMiner>();

            /*
             * Infrastructure bindings.
             */

            this.Bind<ProcessingTools.Contracts.IXPathProvider>()
                .To<ProcessingTools.BaseLibrary.XPathProvider>();
        }
    }
}