namespace ProcessingTools.Web.Documents.Settings
{
    using System;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Imaging.Processors;
    using ProcessingTools.Processors.Contracts.Geo.Coordinates;
    using ProcessingTools.Processors.Imaging;
    using ProcessingTools.Processors.Imaging.Contracts;
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

            this.Bind(typeof(ProcessingTools.Data.Contracts.IGenericRepositoryProvider<>))
                .To(typeof(ProcessingTools.Common.Code.Data.Repositories.RepositoryProviderAsync<>));

            this.Bind(b =>
            {
                b.From(typeof(ProcessingTools.Net.NetConnector).Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind<ICoordinateFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind(b =>
            {
                b.From(ProcessingTools.Processors.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind<ProcessingTools.Services.Contracts.Files.IStreamingFilesDataService>()
                .To<StreamingSystemFilesDataService>();

            this.Bind<ProcessingTools.Contracts.IDocumentFactory>()
                .To<ProcessingTools.Common.Code.TaxPubDocumentFactory>()
                .InSingletonScope();

            this.Bind<Func<Type, ITaggerCommand>>()
                .ToMethod(context => t => context.Kernel.Get(t) as ITaggerCommand)
                .InSingletonScope();

            this.Bind<IFactory<ICommandSettings>>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<Func<Type, ProcessingTools.Contracts.Strategies.Bio.Taxonomy.IParseLowerTaxaStrategy>>()
                .ToMethod(context =>
                {
                    return t => context.Kernel.Get(t) as ProcessingTools.Contracts.Strategies.Bio.Taxonomy.IParseLowerTaxaStrategy;
                });

            this.Bind<IQRCodeEncoder>()
                .To<QRCodeEncoder>()
                .InRequestScope();

            this.Bind<IBarcodeEncoder>()
                .To<BarcodeEncoder>()
                .InRequestScope();
        }
    }
}
