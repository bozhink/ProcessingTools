namespace ProcessingTools.Web.Settings
{
    using System.Configuration;
    using Ninject.Extensions.Conventions;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using ProcessingTools.Constants;
    using ProcessingTools.Constants.Configuration;

    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind(configure =>
            {
                configure.FromAssembliesMatching(
                    $"{nameof(ProcessingTools)}.*.{FileConstants.DllFileExtension}")
                .SelectAllClasses()
                .BindDefaultInterface();
            });

            this.Bind<ProcessingTools.Contracts.Services.IEnvironment>()
                .To<ProcessingTools.Web.Services.EnvironmentService>()
                .InSingletonScope();

            this.Bind<ProcessingTools.History.Data.Entity.Contracts.IHistoryDbContext>()
                .To<ProcessingTools.History.Data.Entity.HistoryDbContext>()
                .WhenInjectedInto<ProcessingTools.History.Data.Entity.Repositories.EntityHistoryRepository>()
                .InRequestScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.HistoryDatabaseConnection].ConnectionString);

            this.Bind<ProcessingTools.Contracts.Data.History.Repositories.IHistoryRepository>()
                .To<ProcessingTools.History.Data.Entity.Repositories.EntityHistoryRepository>()
                .InRequestScope();

            this.Bind<ProcessingTools.Journals.Data.Entity.Contracts.IJournalsDbContext>()
                .To<ProcessingTools.Journals.Data.Entity.JournalsDbContext>()
                .WhenInjectedInto(typeof(ProcessingTools.Data.Common.Entity.Repositories.GenericRepository<,>))
                .InRequestScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.JournalsDatabaseConnection].ConnectionString);

            this.Bind<ProcessingTools.Contracts.Data.Journals.Repositories.IPublishersRepository>()
                .To<ProcessingTools.Journals.Data.Entity.Repositories.EntityPublishersRepository>()
                .InRequestScope();

            this.Bind<ProcessingTools.Geo.Data.Entity.Contracts.IGeoDbContext>()
                .To<ProcessingTools.Geo.Data.Entity.GeoDbContext>()
                .WhenInjectedInto(typeof(ProcessingTools.Geo.Data.Entity.Repositories.GeoRepository<>))
                .InRequestScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.GeoDatabseConnection].ConnectionString);

            this.Bind<ProcessingTools.Contracts.Services.Data.Geo.Services.ICitiesDataService>()
                .To<ProcessingTools.Geo.Services.Data.Entity.Services.EntityCitiesDataService>()
                .InRequestScope();

            this.Bind<ProcessingTools.Contracts.Services.Data.Geo.Services.IGeoNamesDataService>()
                .To<ProcessingTools.Geo.Services.Data.Entity.Services.EntityGeoNamesDataService>()
                .InRequestScope();

            this.Bind<ProcessingTools.Contracts.Services.Data.Geo.Services.IGeoEpithetsDataService>()
                .To<ProcessingTools.Geo.Services.Data.Entity.Services.EntityGeoEpithetsDataService>()
                .InRequestScope();

            this.Bind<ProcessingTools.Contracts.Services.Data.Geo.Services.IContinentsDataService>()
                .To<ProcessingTools.Geo.Services.Data.Entity.Services.EntityContinentsDataService>()
                .InRequestScope();

            this.Bind<ProcessingTools.Contracts.Services.Data.Geo.Services.ICountriesDataService>()
                .To<ProcessingTools.Geo.Services.Data.Entity.Services.EntityCountriesDataService>()
                .InRequestScope();
        }
    }
}
