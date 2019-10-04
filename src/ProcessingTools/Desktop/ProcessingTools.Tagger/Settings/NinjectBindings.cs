namespace ProcessingTools.Tagger.Settings
{
    using System;
    using global::Ninject;
    using global::Ninject.Extensions.Conventions;
    using global::Ninject.Extensions.Interception.Infrastructure.Language;
    using global::Ninject.Modules;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Files;
    using ProcessingTools.Contracts.Services.IO;
    using ProcessingTools.Contracts.Services.Strategies.Bio.Taxonomy;
    using ProcessingTools.Ninject.Interceptors;
    using ProcessingTools.Services.IO;
    using ProcessingTools.Services.Net;

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
                b.From(typeof(HttpRequester).Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Custom hard-coded bindings
            this.Bind<IDocumentFactory>()
                .To<ProcessingTools.Services.TaxPubDocumentFactory>()
                .InSingletonScope();

            this.Bind<Func<Type, ITaggerCommand>>()
                .ToMethod(context => t => (ITaggerCommand)context.Kernel.Get(t))
                .InSingletonScope();

            this.Bind<IXmlReadService>()
                .To<ProcessingTools.Services.IO.BrokenXmlReadService>()
                .WhenInjectedInto<XmlFileContentDataService>();

            this.Bind<IXmlReadService>()
                .To<ProcessingTools.Services.IO.XmlReadService>()
                .Intercept()
                .With<FileNotFoundInterceptor>();

            this.Bind<IXmlWriteService>()
                .To<ProcessingTools.Services.IO.XmlWriteService>()
                .Intercept()
                .With<FileExistsRaiseWarningInterceptor>();

            this.Bind<IFileNameGenerator>()
                .To<ProcessingTools.Services.Files.FileNameGeneratorWithSequentialNumbering>()
                .InSingletonScope();

            this.Bind<Func<Type, IParseLowerTaxaStrategy>>()
                .ToMethod(context =>
                {
                    return t => context.Kernel.Get(t) as IParseLowerTaxaStrategy;
                });
        }
    }
}
