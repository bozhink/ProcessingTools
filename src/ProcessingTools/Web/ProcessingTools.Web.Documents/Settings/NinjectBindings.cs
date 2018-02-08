namespace ProcessingTools.Web.Documents.Settings
{
    using System;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands;
    using ProcessingTools.Contracts.Commands.Tagger;
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

                b.FromAssembliesMatching("ProcessingTools.Web.*")
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
                b.From(ProcessingTools.FileSystem.Assembly.Assembly.GetType().Assembly)
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

            this.Bind<ProcessingTools.Contracts.Services.Data.Files.IStreamingFilesDataService>()
                .To<StreamingSystemFilesDataService>();

            this.Bind<ProcessingTools.Contracts.IDocumentFactory>()
                .To<ProcessingTools.Common.TaxPubDocumentFactory>()
                .InSingletonScope();

            this.Bind<Func<Type, ITaggerCommand>>()
                .ToMethod(context => t => context.Kernel.Get(t) as ITaggerCommand)
                .InSingletonScope();

            this.Bind<IFactory<ICommandSettings>>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Contracts.IReporter>()
                .To<ProcessingTools.Reporters.LogReporter>();

            this.Bind<ProcessingTools.Contracts.ILogger>()
                .To<ProcessingTools.Loggers.Loggers.Log4NetLogger>()
                .InSingletonScope();

            this.Bind<ProcessingTools.Contracts.IDateTimeProvider>()
                .To<ProcessingTools.Services.Providers.DateTimeProvider>()
                .InSingletonScope();

            this.Bind<Func<Type, ProcessingTools.Contracts.Strategies.Bio.Taxonomy.IParseLowerTaxaStrategy>>()
                .ToMethod(context =>
                {
                    return t => context.Kernel.Get(t) as ProcessingTools.Contracts.Strategies.Bio.Taxonomy.IParseLowerTaxaStrategy;
                });

            this.Bind<ProcessingTools.Contracts.Processors.Imaging.IQRCodeEncoder>()
                .To<ProcessingTools.Imaging.Processors.QRCodeEncoder>()
                .InRequestScope();

            this.Bind<ProcessingTools.Contracts.Processors.Imaging.IBarcodeEncoder>()
                .To<ProcessingTools.Imaging.Processors.BarcodeEncoder>()
                .InRequestScope();
        }
    }
}
