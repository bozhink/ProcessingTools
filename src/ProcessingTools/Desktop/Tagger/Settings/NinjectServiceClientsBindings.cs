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
                b.From(typeof(ProcessingTools.Clients.Bio.ExtractHcmr.ExtractHcmrDataRequester).Assembly)
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
