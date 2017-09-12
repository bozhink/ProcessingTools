namespace ProcessingTools.Tagger.Settings
{
    using Ninject.Extensions.Conventions;
    using Ninject.Modules;
    using ProcessingTools.Constants.Configuration;

    /// <summary>
    /// NinjectModule to bind database objects.
    /// </summary>
    public class NinjectDataBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind(b =>
            {
                b.From(Cache.Data.Redis.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Bio.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Bio.Environments.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Geo.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(Data.Common.Redis.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Bio.Biorepositories.Data
            this.Bind<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>()
                .To<ProcessingTools.Data.Common.Mongo.MongoDatabaseProvider>()
                .WhenInjectedInto(typeof(ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.BiorepositoriesRepository<>))
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.BiorepositoriesMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.BiorepositoriesMongoDabaseName);

            this.Bind<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>()
                .To<ProcessingTools.Data.Common.Mongo.MongoDatabaseProvider>()
                .WhenInjectedInto(typeof(ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.BiorepositoriesRepositoryProvider<>))
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.BiorepositoriesMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.BiorepositoriesMongoDabaseName);

            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Biorepositories.Data.Mongo.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });
        }
    }
}
