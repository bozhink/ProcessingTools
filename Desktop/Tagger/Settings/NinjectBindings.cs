namespace ProcessingTools.Tagger.Settings
{
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
                b.From(ProcessingTools.BaseLibrary.Assembly.Assembly.GetType().Assembly)
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

            // Custom hard-coded bindings
            this.Bind<ProcessingTools.Contracts.ILogger>()
                .To<ProcessingTools.Loggers.TextWriterLogger>();

            this.Bind<ProcessingTools.Contracts.IXmlNamespaceManagerProvider>()
                .To<ProcessingTools.DocumentProvider.TaxPubXmlNamespaceManagerProvider>();

            this.Bind<Bio.Taxonomy.Data.Common.Repositories.Contracts.ITaxonRankSearchableRepositoryProvider>()
                .To<Bio.Taxonomy.Data.Xml.Repositories.XmlTaxonRankSearchableRepositoryProvider>();

            this.Bind<Bio.Taxonomy.Data.Common.Repositories.Contracts.ITaxonRankRepositoryProvider>()
                .To<Bio.Taxonomy.Data.Xml.Repositories.XmlTaxonRankRepositoryProvider>();

            this.Bind<Bio.Taxonomy.Data.Common.Repositories.Contracts.IBiotaxonomicBlackListIterableRepositoryProvider>()
                .To<Bio.Taxonomy.Data.Xml.Repositories.XmlBiotaxonomicBlackListIterableRepositoryProvider>();

            this.Bind<Bio.Taxonomy.Data.Common.Repositories.Contracts.IBiotaxonomicBlackListRepositoryProvider>()
                .To<Bio.Taxonomy.Data.Xml.Repositories.XmlBiotaxonomicBlackListRepositoryProvider>();
        }
    }
}