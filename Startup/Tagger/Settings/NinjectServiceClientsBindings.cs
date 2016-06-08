namespace ProcessingTools.Tagger.Settings
{
    using Ninject.Extensions.Conventions;
    using Ninject.Modules;

    /// <summary>
    /// NinjectModule to bind external service client objects.
    /// </summary>
    public class NinjectServiceClientsBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind(b =>
            {
                b.From(Bio.ServiceClient.ExtractHcmr.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            ////this.Bind(b =>
            ////{
            ////    b.From(Bio.ServiceClient.MaterialsParser.Assembly.Assembly.GetType().Assembly)
            ////        .SelectAllClasses()
            ////        .BindDefaultInterface();
            ////});

            this.Bind(b =>
            {
                b.From(Bio.Taxonomy.ServiceClient.CatalogueOfLife.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Bio.Taxonomy.ServiceClient.Gbif.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Bio.Taxonomy.ServiceClient.PaleobiologyDatabase.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });
        }
    }
}