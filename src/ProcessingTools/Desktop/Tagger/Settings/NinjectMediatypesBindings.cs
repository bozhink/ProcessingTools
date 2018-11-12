namespace ProcessingTools.Tagger.Settings
{
    using global::Ninject.Modules;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.Mediatypes.Data.Entity;
    using ProcessingTools.Mediatypes.Data.Entity.Contracts;
    using ProcessingTools.Mediatypes.Data.Entity.Factories;
    using ProcessingTools.Mediatypes.Data.Entity.Providers;
    using ProcessingTools.Mediatypes.Data.Mongo.Repositories;

    public class NinjectMediatypesBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMongoDatabaseProvider>()
                .To<MongoDatabaseProvider>()
                .WhenInjectedInto<MongoMediatypesSearchableRepository>()
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.MediatypesMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.MediatypesMongoDatabaseName);

            this.Bind<IMediatypesDbContext>()
                .To<MediatypesDbContext>()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    ConnectionStrings.MediatypesDatabaseConnection);

            this.Bind<IMediatypesDbContextFactory>()
                .To<MediatypesDbContextFactory>()
                .InSingletonScope();

            this.Bind<IMediatypesDbContextProvider>()
                .To<MediatypesDbContextProvider>()
                .InSingletonScope();
        }
    }
}
