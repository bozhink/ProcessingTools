namespace ProcessingTools.DbSeeder.Settings
{
    using System;
    using System.Reflection;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Extensions.Interception.Infrastructure.Language;
    using Ninject.Modules;
    using ProcessingTools.Contracts;
    using ProcessingTools.DbSeeder.Contracts.Providers;
    using ProcessingTools.DbSeeder.Contracts.Seeders;
    using ProcessingTools.DbSeeder.Core;
    using ProcessingTools.DbSeeder.Providers;
    using ProcessingTools.Interceptors;
    using ProcessingTools.Loggers.Loggers;

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
                .With<CommantRunnerTimeLoggingInterceptor>();

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

            // MediaType.Data
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
                b.From(ProcessingTools.Bio.Taxonomy.Data.Xml.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Taxonomy.Data.Mongo.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

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

            this.Bind<ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories.ITaxonRankRepositoryProvider>()
                .To<ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.XmlTaxonRankRepositoryProvider>();

            this.Bind<ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories.IBiotaxonomicBlackListIterableRepositoryProvider>()
                .To<ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.XmlBiotaxonomicBlackListIterableRepositoryProvider>();

            this.Bind<ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories.IBiotaxonomicBlackListRepositoryProvider>()
                .To<ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.XmlBiotaxonomicBlackListRepositoryProvider>();
        }
    }
}
