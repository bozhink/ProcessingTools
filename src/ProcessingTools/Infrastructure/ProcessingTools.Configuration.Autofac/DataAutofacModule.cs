// <copyright file="DataAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Autofac
{
    using System;
    using System.Collections.Generic;
    using global::Autofac;
    using Microsoft.Extensions.Configuration;
    using MongoDB.Driver;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Data;
    using ProcessingTools.Data.Mongo;

    /// <summary>
    /// Autofac bindings for ProcessingTools.Data.*.
    /// </summary>
    public class DataAutofacModule : Module
    {
        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            // Register MongoCollectionSettings
            builder
                .Register(c => new MongoCollectionSettings
                {
                    AssignIdOnInsert = true,
                    GuidRepresentation = MongoDB.Bson.GuidRepresentation.Unspecified,
                    WriteConcern = new WriteConcern(WriteConcern.Acknowledged.W),
                    ReadPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred),
                })
                .As<MongoCollectionSettings>()
                .Named<MongoCollectionSettings>(InjectionConstants.MongoDBHistoryDatabaseBindingName)
                .SingleInstance();

            builder
                .Register(c => new MongoCollectionSettings
                {
                    AssignIdOnInsert = true,
                    GuidRepresentation = MongoDB.Bson.GuidRepresentation.Unspecified,
                    WriteConcern = new WriteConcern(WriteConcern.WMajority.W),
                })
                .As<MongoCollectionSettings>()
                .Named<MongoCollectionSettings>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)
                .SingleInstance();

            builder
                .Register(c => new MongoCollectionSettings
                {
                    AssignIdOnInsert = true,
                    GuidRepresentation = MongoDB.Bson.GuidRepresentation.Unspecified,
                    WriteConcern = new WriteConcern(WriteConcern.WMajority.W),
                })
                .As<MongoCollectionSettings>()
                .Named<MongoCollectionSettings>(InjectionConstants.MongoDBFilesDatabaseBindingName)
                .SingleInstance();

            builder
                .Register(c => new MongoCollectionSettings
                {
                    AssignIdOnInsert = true,
                    GuidRepresentation = MongoDB.Bson.GuidRepresentation.Unspecified,
                    WriteConcern = new WriteConcern(WriteConcern.WMajority.W),
                })
                .As<MongoCollectionSettings>()
                .Named<MongoCollectionSettings>(InjectionConstants.MongoDBLayoutDatabaseBindingName)
                .SingleInstance();

            builder
                .Register(c => new MongoCollectionSettings
                {
                    AssignIdOnInsert = true,
                    GuidRepresentation = MongoDB.Bson.GuidRepresentation.Unspecified,
                    WriteConcern = new WriteConcern(WriteConcern.WMajority.W),
                })
                .As<MongoCollectionSettings>()
                .Named<MongoCollectionSettings>(InjectionConstants.MongoDBBiotaxonomyDatabaseBindingName)
                .SingleInstance();

            // Register MongoDB databases
            builder.RegisterMongoDatabase(
                connectionString: this.Configuration.GetConnectionString(ConfigurationConstants.HistoryDatabaseMongoDBConnectionStringName),
                databaseName: this.Configuration[ConfigurationConstants.HistoryMongoDBDatabaseName],
                bindingName: InjectionConstants.MongoDBHistoryDatabaseBindingName);

            builder.RegisterMongoDatabase(
                connectionString: this.Configuration.GetConnectionString(ConfigurationConstants.DocumentsDatabaseMongoDBConnectionStringName),
                databaseName: this.Configuration[ConfigurationConstants.DocumentsMongoDBDatabaseName],
                bindingName: InjectionConstants.MongoDBDocumentsDatabaseBindingName);

            builder.RegisterMongoDatabase(
                connectionString: this.Configuration.GetConnectionString(ConfigurationConstants.FilesDatabaseMongoDBConnectionStringName),
                databaseName: this.Configuration[ConfigurationConstants.FilesMongoDBDatabaseName],
                bindingName: InjectionConstants.MongoDBFilesDatabaseBindingName);

            builder.RegisterMongoDatabase(
                connectionString: this.Configuration.GetConnectionString(ConfigurationConstants.LayoutDatabaseMongoDBConnectionStringName),
                databaseName: this.Configuration[ConfigurationConstants.LayoutMongoDBDatabaseName],
                bindingName: InjectionConstants.MongoDBLayoutDatabaseBindingName);

            builder.RegisterMongoDatabase(
                connectionString: this.Configuration.GetConnectionString(ConfigurationConstants.BiotaxonomyDatabaseMongoDBConnectionStringName),
                databaseName: this.Configuration[ConfigurationConstants.BiotaxonomyMongoDBDatabaseName],
                bindingName: InjectionConstants.MongoDBBiotaxonomyDatabaseBindingName);

            // Register MongoDB collections
            builder
                .RegisterMongoCollectionBinding<ProcessingTools.Data.Models.Mongo.History.ObjectHistory>(InjectionConstants.MongoDBHistoryDatabaseBindingName)
                ;

            builder
                .RegisterMongoCollectionBinding<ProcessingTools.Data.Models.Mongo.Files.Mediatype>(InjectionConstants.MongoDBFilesDatabaseBindingName)
                ;

            builder
                .RegisterMongoCollectionBinding<ProcessingTools.Data.Models.Mongo.Layout.Styles.FloatObjectParseStyle>(InjectionConstants.MongoDBLayoutDatabaseBindingName)
                .RegisterMongoCollectionBinding<ProcessingTools.Data.Models.Mongo.Layout.Styles.FloatObjectTagStyle>(InjectionConstants.MongoDBLayoutDatabaseBindingName)
                .RegisterMongoCollectionBinding<ProcessingTools.Data.Models.Mongo.Layout.Styles.JournalStyle>(InjectionConstants.MongoDBLayoutDatabaseBindingName)
                .RegisterMongoCollectionBinding<ProcessingTools.Data.Models.Mongo.Layout.Styles.ReferenceParseStyle>(InjectionConstants.MongoDBLayoutDatabaseBindingName)
                .RegisterMongoCollectionBinding<ProcessingTools.Data.Models.Mongo.Layout.Styles.ReferenceTagStyle>(InjectionConstants.MongoDBLayoutDatabaseBindingName)
                .RegisterMongoCollectionBinding<ProcessingTools.Data.Models.Mongo.Layout.Templates.HandlebarsTemplate>(InjectionConstants.MongoDBLayoutDatabaseBindingName)
                ;

            builder
                .RegisterMongoCollectionBinding<ProcessingTools.Data.Models.Mongo.Bio.Taxonomy.BlackListItem>(InjectionConstants.MongoDBBiotaxonomyDatabaseBindingName)
                .RegisterMongoCollectionBinding<ProcessingTools.Data.Models.Mongo.Bio.Taxonomy.WhiteListItem>(InjectionConstants.MongoDBBiotaxonomyDatabaseBindingName)
                .RegisterMongoCollectionBinding<ProcessingTools.Data.Models.Mongo.Bio.Taxonomy.TaxonRankItem>(InjectionConstants.MongoDBBiotaxonomyDatabaseBindingName)
                ;

            // Register database initializers
            builder
               .Register<Func<IEnumerable<IDatabaseInitializer>>>(c =>
               {
                   var context = c.Resolve<IComponentContext>();
                   return () => new IDatabaseInitializer[]
                   {
                        context.Resolve<ProcessingTools.Data.Mongo.Documents.MongoDocumentsDatabaseInitializer>(),
                        context.Resolve<ProcessingTools.Data.Mongo.Files.MongoFilesDatabaseInitializer>(),
                        context.Resolve<ProcessingTools.Data.Mongo.Layout.MongoLayoutDatabaseInitializer>(),
                        context.Resolve<ProcessingTools.Data.Mongo.Bio.Taxonomy.MongoBiotaxonomyDatabaseInitializer>(),
                   };
               })
               .As<Func<IEnumerable<IDatabaseInitializer>>>()
               .InstancePerDependency();

            builder
                .RegisterMongoDatabaseInitializer<ProcessingTools.Data.Mongo.Documents.MongoDocumentsDatabaseInitializer>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)
                .RegisterMongoDatabaseInitializer<ProcessingTools.Data.Mongo.Files.MongoFilesDatabaseInitializer>(InjectionConstants.MongoDBFilesDatabaseBindingName)
                .RegisterMongoDatabaseInitializer<ProcessingTools.Data.Mongo.Layout.MongoLayoutDatabaseInitializer>(InjectionConstants.MongoDBLayoutDatabaseBindingName)
                .RegisterMongoDatabaseInitializer<ProcessingTools.Data.Mongo.Bio.Taxonomy.MongoBiotaxonomyDatabaseInitializer>(InjectionConstants.MongoDBBiotaxonomyDatabaseBindingName)
                ;

            // Documents DB provider
            builder
                .RegisterType<MongoDatabaseProvider>()
                .As<IMongoDatabaseProvider>()
                .Named<IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)
                .WithParameter(InjectionConstants.ConnectionStringParameterName, this.Configuration.GetConnectionString(ConfigurationConstants.DocumentsDatabaseMongoDBConnectionStringName))
                .WithParameter(InjectionConstants.DatabaseNameParameterName, this.Configuration[ConfigurationConstants.DocumentsMongoDBDatabaseName])
                .InstancePerLifetimeScope();

            // Files DB provider
            builder
                .RegisterType<MongoDatabaseProvider>()
                .As<IMongoDatabaseProvider>()
                .Named<IMongoDatabaseProvider>(InjectionConstants.MongoDBFilesDatabaseBindingName)
                .WithParameter(InjectionConstants.ConnectionStringParameterName, this.Configuration.GetConnectionString(ConfigurationConstants.FilesDatabaseMongoDBConnectionStringName))
                .WithParameter(InjectionConstants.DatabaseNameParameterName, this.Configuration[ConfigurationConstants.FilesMongoDBDatabaseName])
                .InstancePerLifetimeScope();

            // History DB provider
            builder
                .RegisterType<MongoDatabaseProvider>()
                .As<IMongoDatabaseProvider>()
                .Named<IMongoDatabaseProvider>(InjectionConstants.MongoDBHistoryDatabaseBindingName)
                .WithParameter(InjectionConstants.ConnectionStringParameterName, this.Configuration.GetConnectionString(ConfigurationConstants.HistoryDatabaseMongoDBConnectionStringName))
                .WithParameter(InjectionConstants.DatabaseNameParameterName, this.Configuration[ConfigurationConstants.HistoryMongoDBDatabaseName])
                .InstancePerLifetimeScope();
        }
    }
}
