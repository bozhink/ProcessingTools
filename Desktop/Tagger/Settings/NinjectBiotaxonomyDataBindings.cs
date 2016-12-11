namespace ProcessingTools.Tagger.Settings
{
    using Ninject.Modules;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories;
    using ProcessingTools.Configurator;

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

            this.Bind<ITaxonRankSearchableRepositoryProvider>()
                .To<XmlTaxonRankSearchableRepositoryProvider>();

            this.Bind<ITaxonRankRepositoryProvider>()
                .To<XmlTaxonRankRepositoryProvider>();

            this.Bind<IBiotaxonomicBlackListIterableRepositoryProvider>()
                .To<XmlBiotaxonomicBlackListIterableRepositoryProvider>();

            this.Bind<IBiotaxonomicBlackListRepositoryProvider>()
                .To<XmlBiotaxonomicBlackListRepositoryProvider>();
        }
    }
}
