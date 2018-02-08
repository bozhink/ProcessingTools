namespace ProcessingTools.DbSeeder.Settings
{
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Seed.Seeders;
    using ProcessingTools.Bio.Taxonomy.Data.Xml;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;

    public class NinjectBiotaxonomyDataBindings : NinjectModule
    {
        public override void Load()
        {
            // MongoDB
            this.Bind<IMongoTaxonRankRepository>()
                .To<MongoTaxonRanksRepository>();

            this.Bind<IMongoBiotaxonomicBlackListRepository>()
                .To<MongoBiotaxonomicBlackListRepository>();

            this.Bind<IMongoDatabaseProvider>()
                .To<MongoDatabaseProvider>()
                .WhenInjectedInto<MongoTaxonRanksRepository>()
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.BiotaxonomyMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.BiotaxonomyMongoDatabaseName);

            this.Bind<IMongoDatabaseProvider>()
                .To<MongoDatabaseProvider>()
                .WhenInjectedInto<MongoBiotaxonomicBlackListRepository>()
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.BiotaxonomyMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.BiotaxonomyMongoDatabaseName);

            // Seeder
            this.Bind<IBiotaxonomyMongoDatabaseInitializer>()
                .To<BiotaxonomyMongoDatabaseInitializer>()
                .InSingletonScope();

            this.Bind<IMongoDatabaseProvider>()
                .To<MongoDatabaseProvider>()
                .WhenInjectedInto<BiotaxonomyMongoDatabaseSeeder>()
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.BiotaxonomyMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.BiotaxonomyMongoDatabaseName);

            this.Bind<IMongoDatabaseProvider>()
                .To<MongoDatabaseProvider>()
                .WhenInjectedInto<BiotaxonomyMongoDatabaseInitializer>()
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.BiotaxonomyMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.BiotaxonomyMongoDatabaseName);

            this.Bind<IRepositoryFactory<IMongoTaxonRankRepository>>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IRepositoryFactory<IMongoBiotaxonomicBlackListRepository>>()
                .ToFactory()
                .InSingletonScope();

            // Xml
            this.Bind<IXmlTaxaContext>()
                .To<XmlTaxaContext>()
                .InSingletonScope();

            this.Bind<IXmlBiotaxonomicBlackListContext>()
                .To<XmlBiotaxonomicBlackListContext>()
                .InSingletonScope();

            this.Bind<IFactory<IXmlTaxaContext>>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IFactory<IXmlBiotaxonomicBlackListContext>>()
                .ToFactory()
                .InSingletonScope();

            // Common
            this.Bind<ITaxonRanksRepository>()
                .To<XmlTaxonRanksRepository>()
                .WithConstructorArgument(
                    ParameterNames.DataFileName,
                    AppSettings.BiotaxonomyRankListXmlFileName);

            this.Bind<IBiotaxonomicBlackListRepository>()
                .To<XmlBiotaxonomicBlackListRepository>()
                .WithConstructorArgument(
                    ParameterNames.DataFileName,
                    AppSettings.BiotaxonomyBlackListXmlFileName);

            this.Bind<IRepositoryFactory<ITaxonRanksRepository>>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IRepositoryFactory<IBiotaxonomicBlackListRepository>>()
                .ToFactory()
                .InSingletonScope();
        }
    }
}
