namespace ProcessingTools.DbSeeder.Settings
{
    using System;
    using System.Configuration;
    using System.Reflection;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Extensions.Interception.Infrastructure.Language;
    using Ninject.Modules;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Contracts;
    using ProcessingTools.DbSeeder.Contracts.Providers;
    using ProcessingTools.DbSeeder.Contracts.Seeders;
    using ProcessingTools.DbSeeder.Core;
    using ProcessingTools.DbSeeder.Providers;
    using ProcessingTools.Interceptors;
    using ProcessingTools.Loggers.Loggers;
    using ProcessingTools.Reporters;

    /// <summary>
    /// NinjectModule to bind seeder objects.
    /// </summary>
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind(b =>
            {
                b.From(Assembly.GetExecutingAssembly())
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(typeof(ProcessingTools.Contracts.Data.Repositories.IGenericRepositoryProvider<>))
                .To(typeof(ProcessingTools.Data.Common.Repositories.RepositoryProvider<>));

            this.Bind<ITypesProvider>()
                .To<SeederTypesProvider>()
                .InSingletonScope();

            this.Bind<Func<Type, IDbSeeder>>()
                .ToMethod(context => t => (IDbSeeder)context.Kernel.Get(t))
                .InSingletonScope();

            this.Bind<ILogger>()
                .To<ConsoleLogger>()
                .InSingletonScope();

            this.Bind<IReporter>()
                .To<LogReporter>()
                .InSingletonScope();

            this.Bind<IHelpProvider>()
                .To<HelpProvider>()
                .InSingletonScope();

            this.Bind<ICommandNamesProvider>()
                .To<CommandNamesProvider>()
                .InSingletonScope();

            this.Bind<ICommandRunner>()
                .To<SeedCommandRunner>()
                .Intercept()
                .With<CommandRunnerTimeLoggingInterceptor>();

            this.Bind<ISandbox>()
                .To<Sandbox>();

            // Geo.Data
            this.Bind(b =>
            {
                b.From(ProcessingTools.Geo.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Geo.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Mediatypes.Data
            this.Bind(b =>
            {
                b.From(ProcessingTools.Mediatypes.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Mediatypes.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // DataResources
            this.Bind(b =>
            {
                b.From(ProcessingTools.DataResources.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.DataResources.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Bio.Biorepositories.Data
            this.Bind<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>()
                .To<ProcessingTools.Data.Common.Mongo.MongoDatabaseProvider>()
                .WhenInjectedInto(typeof(ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.BiorepositoriesRepository<>))
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    ConfigurationManager.AppSettings[AppSettingsKeys.BiorepositoriesMongoConnection])
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.BiorepositoriesMongoDabaseName]);

            this.Bind<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>()
                .To<ProcessingTools.Data.Common.Mongo.MongoDatabaseProvider>()
                .WhenInjectedInto(typeof(ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.BiorepositoriesRepositoryProvider<>))
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    ConfigurationManager.AppSettings[AppSettingsKeys.BiorepositoriesMongoConnection])
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.BiorepositoriesMongoDabaseName]);

            this.Bind<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>()
                .To<ProcessingTools.Data.Common.Mongo.MongoDatabaseProvider>()
                .WhenInjectedInto<ProcessingTools.Bio.Biorepositories.Data.Seed.Seeders.BiorepositoriesDataSeeder>()
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    ConfigurationManager.AppSettings[AppSettingsKeys.BiorepositoriesMongoConnection])
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.BiorepositoriesMongoDabaseName]);

            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Biorepositories.Data.Mongo.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Biorepositories.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Bio.Data
            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Bio.Environments.Data
            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Environments.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Environments.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Bio.Taxonomy.Data
            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Taxonomy.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Taxonomy.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });
        }
    }
}
