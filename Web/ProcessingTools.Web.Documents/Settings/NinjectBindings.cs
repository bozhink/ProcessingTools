namespace ProcessingTools.Web.Documents.Settings
{
    using Ninject.Extensions.Conventions;
    using Ninject.Modules;
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
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Net.Assembly.Assembly.GetType().Assembly)
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
                b.From(ProcessingTools.Xml.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind<ProcessingTools.Bio.Taxonomy.Data.Common.Repositories.Contracts.ITaxonRankRepositoryProvider>()
                .To<ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.XmlTaxonRankRepositoryProvider>();

            this.Bind<ProcessingTools.Bio.Taxonomy.Data.Common.Repositories.Contracts.IBiotaxonomicBlackListIterableRepositoryProvider>()
                .To<ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.XmlBiotaxonomicBlackListIterableRepositoryProvider>();

            this.Bind<ProcessingTools.Bio.Taxonomy.Data.Common.Repositories.Contracts.IBiotaxonomicBlackListRepositoryProvider>()
                .To<ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.XmlBiotaxonomicBlackListRepositoryProvider>();

            this.Bind<ProcessingTools.Contracts.Services.Data.Files.IStreamingFilesDataService>()
                .To<StreamingSystemFilesDataService>();
        }
    }
}
