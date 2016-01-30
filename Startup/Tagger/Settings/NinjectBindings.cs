namespace ProcessingTools.MainProgram.Settings
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
            this.Bind<ProcessingTools.Contracts.IXPathProvider>()
                .To<ProcessingTools.BaseLibrary.XPathProvider>();

            this.Bind(b =>
            {
                b.FromThisAssembly()
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });
        }
    }
}