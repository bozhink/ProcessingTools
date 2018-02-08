﻿namespace ProcessingTools.Web.Settings
{
    using Ninject.Extensions.Conventions;
    using Ninject.Extensions.Factory;
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

            this.Bind<ProcessingTools.Contracts.ILogger>()
                .To<ProcessingTools.Loggers.Loggers.Log4NetLogger>()
                .InTransientScope();

            this.Bind<ProcessingTools.Contracts.ILoggerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.History.Data.Entity.Contracts.IHistoryDbContext>()
                .To<ProcessingTools.History.Data.Entity.HistoryDbContext>()
                .WhenInjectedInto<ProcessingTools.History.Data.Entity.Repositories.EntityObjectHistoriesRepository>()
                .InRequestScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    ConnectionStrings.HistoryDatabaseConnection);

            this.Bind<ProcessingTools.Data.Contracts.History.IObjectHistoriesRepository>()
                .To<ProcessingTools.History.Data.Entity.Repositories.EntityObjectHistoriesRepository>()
                .InRequestScope();

            this.Bind<ProcessingTools.Journals.Data.Entity.Contracts.IJournalsDbContext>()
                .To<ProcessingTools.Journals.Data.Entity.JournalsDbContext>()
                .WhenInjectedInto(typeof(ProcessingTools.Data.Common.Entity.Repositories.EfRepository<,>))
                .InRequestScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    ConnectionStrings.JournalsDatabaseConnection);

            this.Bind<ProcessingTools.Data.Contracts.Journals.IPublishersRepository>()
                .To<ProcessingTools.Journals.Data.Entity.Repositories.EntityPublishersRepository>()
                .InRequestScope();

            this.Bind<ProcessingTools.Geo.Data.Entity.Contracts.IGeoDbContext>()
                .To<ProcessingTools.Geo.Data.Entity.GeoDbContext>()
                .WhenInjectedInto(typeof(ProcessingTools.Geo.Data.Entity.Repositories.GeoRepository<>))
                .InRequestScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    ConnectionStrings.GeoDatabseConnection);

            this.Bind<ProcessingTools.Data.Contracts.Geo.ICitiesRepository>()
                .To<ProcessingTools.Geo.Data.Entity.Repositories.EntityCitiesRepository>()
                .InRequestScope();

            this.Bind<ProcessingTools.Data.Contracts.Geo.IGeoNamesRepository>()
                .To<ProcessingTools.Geo.Data.Entity.Repositories.EntityGeoNamesRepository>()
                .InRequestScope();

            this.Bind<ProcessingTools.Data.Contracts.Geo.IGeoEpithetsRepository>()
                .To<ProcessingTools.Geo.Data.Entity.Repositories.EntityGeoEpithetsRepository>()
                .InRequestScope();

            this.Bind<ProcessingTools.Data.Contracts.Geo.IContinentsRepository>()
                .To<ProcessingTools.Geo.Data.Entity.Repositories.EntityContinentsRepository>()
                .InRequestScope();

            this.Bind<ProcessingTools.Data.Contracts.Geo.ICountriesRepository>()
                .To<ProcessingTools.Geo.Data.Entity.Repositories.EntityCountriesRepository>()
                .InRequestScope();
        }
    }
}
