namespace ProcessingTools.Tagger.Settings
{
    using System;
    using Contracts.Commands;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Extensions.Interception.Infrastructure.Language;
    using Ninject.Modules;
    using ProcessingTools.Interceptors;
    using ProcessingTools.Loggers.Loggers;
    using ProcessingTools.Services.Data.Services.Files;

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
                b.From(ProcessingTools.Processors.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Taxonomy.Processors.Assembly.Assembly.GetType().Assembly)
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
                .To<ConsoleLogger>();

            this.Bind<ProcessingTools.Contracts.IDocumentFactory>()
                .To<ProcessingTools.DocumentProvider.Factories.TaxPubDocumentFactory>();

            this.Bind<ProcessingTools.Cache.Data.Common.Contracts.Repositories.IValidationCacheDataRepository>()
                .To<ProcessingTools.Cache.Data.Redis.Repositories.RedisValidationCacheDataRepository>();

            this.Bind<ProcessingTools.Contracts.IDateTimeProvider>()
                .To<ProcessingTools.Common.Providers.DateTimeProvider>();

            this.Bind<Func<Type, ITaggerCommand>>()
                .ToMethod(context => t => (ITaggerCommand)context.Kernel.Get(t))
                .InSingletonScope();

            this.Bind<ProcessingTools.Contracts.Files.IO.IXmlFileReader>()
                .To<ProcessingTools.FileSystem.IO.XmlFileReader>()
                .WhenInjectedInto<ProcessingTools.FileSystem.IO.BrokenXmlFileReader>();
            this.Bind<ProcessingTools.Contracts.Files.IO.IXmlFileReader>()
                .To<ProcessingTools.FileSystem.IO.BrokenXmlFileReader>()
                .WhenInjectedInto<XmlFileContentDataService>();

            this.Bind<ProcessingTools.Contracts.Files.IO.IXmlFileWriter>()
                .To<ProcessingTools.FileSystem.IO.XmlFileWriter>()
                .Intercept()
                .With<FileExistsRaiseWarningInterceptor>();

            this.Bind<ProcessingTools.Contracts.Files.Generators.IFileNameGenerator>()
                .To<ProcessingTools.FileSystem.Generators.SequentialFileNameGenerator>()
                .InSingletonScope();

            this.Bind<ProcessingTools.Tagger.Contracts.IFileProcessor>()
                .To<ProcessingTools.Tagger.Core.SingleFileProcessor>();

            this.Bind<ProcessingTools.Geo.Contracts.ICoordinate2DParser>()
                .To<ProcessingTools.Geo.Coordinate2DParser>();

            this.Bind<ProcessingTools.Geo.Contracts.IUtmCoordianesTransformer>()
                .To<ProcessingTools.Geo.UtmCoordianesTransformer>();
        }
    }
}
