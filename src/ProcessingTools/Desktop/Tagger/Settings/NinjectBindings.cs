namespace ProcessingTools.Tagger.Settings
{
    using System;
    using Interceptors;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Extensions.Factory;
    using Ninject.Extensions.Interception.Infrastructure.Language;
    using Ninject.Modules;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Interceptors;
    using ProcessingTools.Loggers.Loggers;
    using ProcessingTools.Services.Data.Services.Files;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;

    /// <summary>
    /// NinjectModule to bind other infrastructure objects.
    /// </summary>
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind(b =>
            {
                b.FromThisAssembly()
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(typeof(ITaggerCommand).Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(typeof(ProcessingTools.Contracts.Data.Repositories.IGenericRepositoryProvider<>))
                .To(typeof(ProcessingTools.Data.Common.Repositories.RepositoryProvider<>));

            this.Bind(b =>
            {
                b.From(ProcessingTools.Net.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Xml.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Geo.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind<ProcessingTools.Geo.Contracts.Factories.ICoordinatesFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Geo.Contracts.Parsers.ICoordinate2DParser>()
                .To<ProcessingTools.Geo.Parsers.Coordinate2DParser>()
                .WhenInjectedInto<ProcessingTools.Geo.Parsers.CoordinateParser>()
                .Intercept()
                .With<LogParsedCoordinatesInterceptor>();

            this.Bind(b =>
            {
                b.From(ProcessingTools.Processors.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Layout.Processors.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Special.Processors.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Serialization.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Custom hard-coded bindings
            this.Bind<ProcessingTools.Contracts.ILogger>()
                .To<ConsoleLogger>()
                .InSingletonScope();

            this.Bind<ProcessingTools.Contracts.IReporter>()
                .To<Reporters.LogReporter>();

            this.Bind<ProcessingTools.Contracts.IDocumentFactory>()
                .To<ProcessingTools.DocumentProvider.Factories.TaxPubDocumentFactory>()
                .InSingletonScope();

            this.Bind<ProcessingTools.Contracts.Data.Cache.Repositories.IValidationCacheDataRepository>()
                ////.To<ProcessingTools.Cache.Data.Redis.Repositories.RedisValidationCacheDataRepository>();
                .To<ProcessingTools.Cache.Data.Mongo.Repositories.MongoValidationCacheDataRepository>();
            this.Bind<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>()
                .To<ProcessingTools.Data.Common.Mongo.MongoDatabaseProvider>()
                .WhenInjectedInto<ProcessingTools.Cache.Data.Mongo.Repositories.MongoValidationCacheDataRepository>()
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.CacheMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.CacheMongoDabaseName);

            this.Bind<ProcessingTools.Contracts.IDateTimeProvider>()
                .To<ProcessingTools.Services.Providers.DateTimeProvider>()
                .InSingletonScope();

            this.Bind<Func<Type, ITaggerCommand>>()
                .ToMethod(context => t => (ITaggerCommand)context.Kernel.Get(t))
                .InSingletonScope();

            this.Bind<ProcessingTools.Contracts.Files.IO.IXmlFileReader>()
                .To<ProcessingTools.FileSystem.IO.BrokenXmlFileReader>()
                .WhenInjectedInto<XmlFileContentDataService>();

            this.Bind<ProcessingTools.Contracts.Files.IO.IXmlFileReader>()
                .To<ProcessingTools.FileSystem.IO.XmlFileReader>()
                .Intercept()
                .With<FileNotFoundInterceptor>();

            this.Bind<ProcessingTools.Contracts.Files.IO.IXmlFileWriter>()
                .To<ProcessingTools.FileSystem.IO.XmlFileWriter>()
                .Intercept()
                .With<FileExistsRaiseWarningInterceptor>();

            this.Bind<ProcessingTools.Contracts.Files.Generators.IFileNameGenerator>()
                .To<ProcessingTools.FileSystem.Generators.SequentialFileNameGenerator>()
                .InSingletonScope();

            this.Bind<Func<Type, ProcessingTools.Processors.Contracts.Strategies.Bio.Taxonomy.IParseLowerTaxaStrategy>>()
                .ToMethod(context =>
                {
                    return t => context.Kernel.Get(t) as ProcessingTools.Processors.Contracts.Strategies.Bio.Taxonomy.IParseLowerTaxaStrategy;
                });
        }
    }
}
