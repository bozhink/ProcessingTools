namespace ProcessingTools.DbSeeder.Settings
{
    using System.Reflection;
    using Ninject.Extensions.Conventions;
    using Ninject.Modules;

    /// <summary>
    /// NinjectModule to bind seeder objects.
    /// </summary>
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind(b =>
            {
                b.From(Assembly.GetExecutingAssembly())
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Geo.Data
            this.Bind(b =>
            {
                b.From(Geo.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Geo.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // MediaType.Data
            this.Bind(b =>
            {
                b.From(MediaType.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(MediaType.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Data
            this.Bind(b =>
            {
                b.From(Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Bio.Biorepositories.Data
            this.Bind(b =>
            {
                b.From(Bio.Biorepositories.Data.Mongo.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Bio.Biorepositories.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Bio.Data
            this.Bind(b =>
            {
                b.From(Bio.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Bio.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Bio.Environments.Data
            this.Bind(b =>
            {
                b.From(Bio.Environments.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Bio.Environments.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Bio.Taxonomy.Data
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
                b.From(Bio.Taxonomy.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Bio.Taxonomy.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind<Bio.Taxonomy.Data.Common.Repositories.Contracts.ITaxonRankRepositoryProvider>()
                .To<Bio.Taxonomy.Data.Xml.Repositories.XmlTaxonRankRepositoryProvider>();

            this.Bind<Bio.Taxonomy.Data.Common.Repositories.Contracts.IBiotaxonomicBlackListIterableRepositoryProvider>()
                .To<Bio.Taxonomy.Data.Xml.Repositories.XmlBiotaxonomicBlackListIterableRepositoryProvider>();

            this.Bind<Bio.Taxonomy.Data.Common.Repositories.Contracts.IBiotaxonomicBlackListRepositoryProvider>()
                .To<Bio.Taxonomy.Data.Xml.Repositories.XmlBiotaxonomicBlackListRepositoryProvider>();
        }
    }
}