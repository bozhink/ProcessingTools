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

            this.Bind(typeof(ProcessingTools.Data.Contracts.IGenericRepositoryProvider<>))
                .To(typeof(ProcessingTools.Common.Code.Data.Repositories.RepositoryProviderAsync<>));

            this.Bind(b =>
            {
                b.From(ProcessingTools.Services.Data.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });
        }
    }
}
