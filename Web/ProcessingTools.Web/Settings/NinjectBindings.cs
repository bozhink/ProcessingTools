namespace ProcessingTools.Web.Settings
{
    using Ninject.Extensions.Conventions;
    using Ninject.Modules;
    using ProcessingTools.Constants;

    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind(configure =>
            {
                configure.FromAssembliesMatching(
                    $"{nameof(ProcessingTools)}.*.{FileConstants.DllFileExtension}")
                .SelectAllClasses()
                .BindDefaultInterface();
            });
        }
    }
}
