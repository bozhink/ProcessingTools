namespace ProcessingTools.Tagger.Settings
{
    using System;
    using Contracts.Controllers;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Modules;

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
                .To<ProcessingTools.Loggers.ConsoleLogger>();

            this.Bind<Bio.Taxonomy.Data.Common.Repositories.Contracts.ITaxonRankSearchableRepositoryProvider>()
                .To<Bio.Taxonomy.Data.Xml.Repositories.XmlTaxonRankSearchableRepositoryProvider>();

            this.Bind<Bio.Taxonomy.Data.Common.Repositories.Contracts.ITaxonRankRepositoryProvider>()
                .To<Bio.Taxonomy.Data.Xml.Repositories.XmlTaxonRankRepositoryProvider>();

            this.Bind<Bio.Taxonomy.Data.Common.Repositories.Contracts.IBiotaxonomicBlackListIterableRepositoryProvider>()
                .To<Bio.Taxonomy.Data.Xml.Repositories.XmlBiotaxonomicBlackListIterableRepositoryProvider>();

            this.Bind<Bio.Taxonomy.Data.Common.Repositories.Contracts.IBiotaxonomicBlackListRepositoryProvider>()
                .To<Bio.Taxonomy.Data.Xml.Repositories.XmlBiotaxonomicBlackListRepositoryProvider>();

            this.Bind<ProcessingTools.Contracts.IDocumentFactory>()
                .To<ProcessingTools.DocumentProvider.Factories.TaxPubDocumentFactory>();

            this.Bind<ProcessingTools.Cache.Data.Common.Contracts.Repositories.IValidationCacheDataRepository>()
                .To<ProcessingTools.Cache.Data.Redis.Repositories.RedisValidationCacheDataRepository>();

            this.Bind<ProcessingTools.Contracts.IDateTimeProvider>()
                .To<ProcessingTools.Common.Providers.DateTimeProvider>();

            this.Bind<Func<Type, ITaggerController>>()
                .ToMethod(context => t => (ITaggerController)context.Kernel.Get(t))
                .InSingletonScope();
        }
    }
}
