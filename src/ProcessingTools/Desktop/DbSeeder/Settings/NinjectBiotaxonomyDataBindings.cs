﻿namespace ProcessingTools.DbSeeder.Settings
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
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;

    public class NinjectBiotaxonomyDataBindings : NinjectModule
    {
        public override void Load()
        {
            // MongoDB
            this.Bind<IMongoTaxonRankRepository>()
                .To<MongoTaxonRankRepository>();

            this.Bind<IMongoBiotaxonomicBlackListRepository>()
                .To<MongoBiotaxonomicBlackListRepository>();

            this.Bind<IMongoDatabaseProvider>()
                .To<MongoDatabaseProvider>()
                .WhenInjectedInto<MongoTaxonRankRepository>()
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.BiotaxonomyMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.BiotaxonomyMongoDabaseName);

            this.Bind<IMongoDatabaseProvider>()
                .To<MongoDatabaseProvider>()
                .WhenInjectedInto<MongoBiotaxonomicBlackListRepository>()
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.BiotaxonomyMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.BiotaxonomyMongoDabaseName);

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
                    AppSettings.BiotaxonomyMongoDabaseName);

            this.Bind<IMongoDatabaseProvider>()
                .To<MongoDatabaseProvider>()
                .WhenInjectedInto<BiotaxonomyMongoDatabaseInitializer>()
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.BiotaxonomyMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.BiotaxonomyMongoDabaseName);

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
            this.Bind<ITaxonRankRepository>()
                .To<XmlTaxonRankRepository>()
                .WithConstructorArgument(
                    ParameterNames.DataFileName,
                    AppSettings.BiotaxonomyRankListXmlFileName);

            this.Bind<IBiotaxonomicBlackListRepository>()
                .To<XmlBiotaxonomicBlackListRepository>()
                .WithConstructorArgument(
                    ParameterNames.DataFileName,
                    AppSettings.BiotaxonomyBlackListXmlFileName);

            this.Bind<IRepositoryFactory<ITaxonRankRepository>>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IRepositoryFactory<IBiotaxonomicBlackListRepository>>()
                .ToFactory()
                .InSingletonScope();
        }
    }
}
