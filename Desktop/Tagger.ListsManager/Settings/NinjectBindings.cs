namespace ProcessingTools.ListsManager.Settings
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

            this.Bind(typeof(ProcessingTools.Contracts.Data.Repositories.IGenericRepositoryProvider<>))
                .To(typeof(ProcessingTools.Data.Common.Repositories.RepositoryProvider<>));

            this.Bind(b =>
            {
                b.From(Bio.Taxonomy.Data.Xml.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Bio.Taxonomy.Data.Mongo.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Bio.Taxonomy.Services.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind<Bio.Taxonomy.Data.Common.Contracts.Repositories.ITaxonRankRepositoryProvider>()
                .To<Bio.Taxonomy.Data.Xml.Repositories.XmlTaxonRankRepositoryProvider>();

            this.Bind<Bio.Taxonomy.Data.Common.Contracts.Repositories.IBiotaxonomicBlackListIterableRepositoryProvider>()
                .To<Bio.Taxonomy.Data.Xml.Repositories.XmlBiotaxonomicBlackListIterableRepositoryProvider>();

            this.Bind<Bio.Taxonomy.Data.Common.Contracts.Repositories.IBiotaxonomicBlackListRepositoryProvider>()
                .To<Bio.Taxonomy.Data.Xml.Repositories.XmlBiotaxonomicBlackListRepositoryProvider>();
        }
    }
}
