namespace ProcessingTools.Tagger.Settings
{
    using System;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Extensions.Factory;
    using Ninject.Extensions.Interception.Infrastructure.Language;
    using Ninject.Modules;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Interceptors;
    using ProcessingTools.Loggers.Loggers;
    using ProcessingTools.Processors.Contracts.Geo.Coordinates;
    using ProcessingTools.Services.IO;

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
                b.From(typeof(ProcessingTools.Tagger.Commands.Commands.TestCommand).Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(typeof(ProcessingTools.Data.Contracts.IGenericRepositoryProvider<>))
                .To(typeof(ProcessingTools.Common.Data.Repositories.RepositoryProviderAsync<>));

            this.Bind(b =>
            {
                b.From(typeof(ProcessingTools.Net.NetConnector).Assembly)
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

            this.Bind<ICoordinateFactory>()
                .ToFactory()
                .InSingletonScope();

            //this.Bind<ICoordinate2DParser>()
            //    .To<Coordinate2DParser>()
            //    .WhenInjectedInto<CoordinateParser>()
            //    .Intercept()
            //    .With<LogParsedCoordinatesInterceptor>();

            // Custom hard-coded bindings
            this.Bind<ProcessingTools.Contracts.ILogger>()
                .To<ConsoleLogger>()
                .InSingletonScope();

            this.Bind<ProcessingTools.Contracts.IReporter>()
                .To<Reporters.LogReporter>();

            this.Bind<ProcessingTools.Contracts.IDocumentFactory>()
                .To<ProcessingTools.Common.TaxPubDocumentFactory>()
                .InSingletonScope();

            this.Bind<ProcessingTools.Data.Contracts.Cache.IValidationCacheDataRepository>()
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
                    AppSettings.CacheMongoDatabaseName);

            this.Bind<Func<Type, ITaggerCommand>>()
                .ToMethod(context => t => (ITaggerCommand)context.Kernel.Get(t))
                .InSingletonScope();

            this.Bind<ProcessingTools.Services.Contracts.IO.IXmlReadService>()
                .To<ProcessingTools.Services.IO.BrokenXmlReadService>()
                .WhenInjectedInto<XmlFileContentDataService>();

            this.Bind<ProcessingTools.Services.Contracts.IO.IXmlReadService>()
                .To<ProcessingTools.Services.IO.XmlReadService>()
                .Intercept()
                .With<FileNotFoundInterceptor>();

            this.Bind<ProcessingTools.Services.Contracts.IO.IXmlWriteService>()
                .To<ProcessingTools.Services.IO.XmlWriteService>()
                .Intercept()
                .With<FileExistsRaiseWarningInterceptor>();

            this.Bind<ProcessingTools.Services.Contracts.IO.IFileNameGenerator>()
                .To<ProcessingTools.Services.IO.SequentialFileNameGenerator>()
                .InSingletonScope();

            this.Bind<Func<Type, ProcessingTools.Contracts.Strategies.Bio.Taxonomy.IParseLowerTaxaStrategy>>()
                .ToMethod(context =>
                {
                    return t => context.Kernel.Get(t) as ProcessingTools.Contracts.Strategies.Bio.Taxonomy.IParseLowerTaxaStrategy;
                });
        }
    }
}
