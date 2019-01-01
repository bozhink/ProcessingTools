namespace ProcessingTools.Tagger.Settings
{
    using global::Ninject.Modules;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Data.Mongo;
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
        }
    }
}
