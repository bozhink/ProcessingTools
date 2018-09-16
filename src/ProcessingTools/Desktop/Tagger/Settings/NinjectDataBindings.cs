namespace ProcessingTools.Tagger.Settings
{
    using global::Ninject.Extensions.Conventions;
    using global::Ninject.Modules;
    using ProcessingTools.Common.Constants.Configuration;

    /// <summary>
    /// NinjectModule to bind database objects.
    /// </summary>
    public class NinjectDataBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind(b =>
            {
                b.From(Bio.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();

                b.From(Bio.Environments.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();

                b.From(Geo.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();

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
                    AppSettings.BiorepositoriesMongoDatabaseName);

            this.Bind<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>()
                .To<ProcessingTools.Data.Common.Mongo.MongoDatabaseProvider>()
                .WhenInjectedInto(typeof(ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.BiorepositoriesRepositoryProvider<>))
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.BiorepositoriesMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.BiorepositoriesMongoDatabaseName);

            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Biorepositories.Data.Mongo.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });
        }
    }
}
