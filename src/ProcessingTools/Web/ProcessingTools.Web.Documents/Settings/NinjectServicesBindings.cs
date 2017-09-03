namespace ProcessingTools.Web.Documents.Settings
{
    using Ninject.Extensions.Conventions;
    using Ninject.Modules;

    /// <summary>
    /// NinjectModule to bind service objects.
    /// </summary>
    public class NinjectServicesBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Biorepositories.Services.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();

                b.From(ProcessingTools.Bio.Environments.Services.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();

                b.From(ProcessingTools.Bio.Services.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();

                b.From(ProcessingTools.DataResources.Services.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();

                b.From(ProcessingTools.Geo.Services.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();

                b.From(ProcessingTools.Services.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();

                b.From(ProcessingTools.Services.Cache.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();

                b.From(ProcessingTools.Services.Validation.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();

                b.From(ProcessingTools.Documents.Services.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });
        }
    }
}