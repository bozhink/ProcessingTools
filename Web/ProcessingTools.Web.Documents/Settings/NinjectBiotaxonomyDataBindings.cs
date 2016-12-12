namespace ProcessingTools.Web.Documents.Settings
{
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Xml;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories;
    using ProcessingTools.Configurator;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;

    public class NinjectBiotaxonomyDataBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IConfig>()
                .ToMethod(context =>
                {
                    return ConfigBuilder.Create();
                })
                .InSingletonScope();

            // Xml
            this.Bind<IXmlTaxaContext>()
                .To<XmlTaxaContext>()
                .InSingletonScope();

            this.Bind<IXmlBiotaxonomicBlackListContext>()
                .To<XmlBiotaxonomicBlackListContext>()
                .InSingletonScope();

            this.Bind<IFactory<IXmlTaxaContext>>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IFactory<IXmlBiotaxonomicBlackListContext>>()
                .ToFactory()
                .InSingletonScope();

            // Common
            this.Bind<ITaxonRankSearchableRepository>()
                .To<XmlTaxonRankRepository>();

            this.Bind<ITaxonRankRepository>()
                .To<XmlTaxonRankRepository>();

            this.Bind<IBiotaxonomicBlackListIterableRepository>()
                .To<XmlBiotaxonomicBlackListRepository>();

            this.Bind<IBiotaxonomicBlackListRepository>()
                .To<XmlBiotaxonomicBlackListRepository>();

            this.Bind<IRepositoryFactory<ITaxonRankSearchableRepository>>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IRepositoryFactory<ITaxonRankRepository>>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IRepositoryFactory<IBiotaxonomicBlackListIterableRepository>>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IRepositoryFactory<IBiotaxonomicBlackListRepository>>()
                .ToFactory()
                .InSingletonScope();
        }
    }
}
