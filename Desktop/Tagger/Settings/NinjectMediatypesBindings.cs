namespace ProcessingTools.Tagger.Settings
{
    using Ninject.Modules;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories;
    using ProcessingTools.Mediatypes.Data.Entity;
    using ProcessingTools.Mediatypes.Data.Entity.Contracts;
    using ProcessingTools.Mediatypes.Data.Entity.Factories;
    using ProcessingTools.Mediatypes.Data.Entity.Providers;
    using ProcessingTools.Mediatypes.Data.Entity.Repositories;
    using ProcessingTools.Services.Data.Contracts.Mediatypes;
    using ProcessingTools.Services.Data.Services.Mediatypes;


    using System.Configuration;
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Xml;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
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
                    ConfigurationManager.AppSettings[AppSettingsKeys.MediatypesMongoConnection])
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.MediatypesMongoDabaseName]);


            this.Bind<IMediatypesDbContext>()
                .To<MediatypesDbContext>()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    ConnectionStringsKeys.MediatypesDatabaseConnection);

            this.Bind<IMediatypesDbContextFactory>()
                .To<MediatypesDbContextFactory>()
                .InSingletonScope();

            this.Bind<IMediatypesDbContextProvider>()
                .To<MediatypesDbContextProvider>()
                .InSingletonScope();

            this.Bind<ISearchableMediatypesRepository>()
                ////.To<MediatypesRepository>()
                .To<MongoMediatypesSearchableRepository>()
                .InThreadScope();

            this.Bind<IMediatypesRepository>()
                .To<MediatypesRepository>();

            this.Bind<IMediatypeStringResolver>()
                .To<MediatypeStringResolverWithStaticDictionary>()
                .InSingletonScope();

            this.Bind<IMediatypesResolver>()
                .To<MediatypesResolverWithDatabase>();
        }
    }
}
