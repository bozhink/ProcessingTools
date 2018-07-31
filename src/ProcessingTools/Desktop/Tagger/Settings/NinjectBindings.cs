namespace ProcessingTools.Tagger.Settings
{
    using System;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Extensions.Interception.Infrastructure.Language;
    using Ninject.Modules;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Interceptors;
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

            this.Bind(typeof(ProcessingTools.Data.Contracts.IGenericRepositoryProvider<>))
                .To(typeof(ProcessingTools.Common.Data.Repositories.RepositoryProviderAsync<>));

            this.Bind(b =>
            {
                b.From(typeof(ProcessingTools.Net.NetConnector).Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Custom hard-coded bindings
            this.Bind<ProcessingTools.Contracts.IDocumentFactory>()
                .To<ProcessingTools.Common.TaxPubDocumentFactory>()
                .InSingletonScope();

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
