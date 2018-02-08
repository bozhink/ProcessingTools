namespace ProcessingTools.Web.Api.Settings
{
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

            this.Bind<ProcessingTools.Contracts.ILogger>()
                .To<ProcessingTools.Loggers.Loggers.Log4NetLogger>()
                .InTransientScope();

            this.Bind<ProcessingTools.Contracts.Services.IEnvironment>()
                .To<ProcessingTools.Web.Api.Services.EnvironmentService>()
                .InSingletonScope();

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
