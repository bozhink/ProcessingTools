namespace ProcessingTools.MainProgram.Settings
{
    using Ninject.Extensions.Conventions;
    using Ninject.Modules;

    /// <summary>
    /// NinjectModule to bind data miner objects.
    /// </summary>
    public class NinjectDataMinersBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind(b =>
            {
                b.From(Bio.Data.Miners.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Geo.Data.Miners.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Data.Miners.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });
        }
    }
}