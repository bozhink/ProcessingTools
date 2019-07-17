// <copyright file="DataAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Autofac
{
    using System;
    using System.Collections.Generic;
    using global::Autofac;
    using global::Autofac.Core;
    using Microsoft.Extensions.Configuration;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Data;
    using ProcessingTools.Contracts.DataAccess.Documents;
    using ProcessingTools.Contracts.DataAccess.Files;
    using ProcessingTools.Contracts.DataAccess.History;
    using ProcessingTools.Contracts.DataAccess.Layout.Styles;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.Data.Mongo.Documents;
    using ProcessingTools.Data.Mongo.Files;
    using ProcessingTools.Data.Mongo.History;
    using ProcessingTools.Data.Mongo.Layout;

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
            builder
               .Register<Func<IEnumerable<IDatabaseInitializer>>>(c =>
               {
                   var context = c.Resolve<IComponentContext>();
                   return () => new IDatabaseInitializer[]
                   {
                        context.Resolve<MongoDocumentsDatabaseInitializer>(),
                        context.Resolve<MongoFilesDatabaseInitializer>(),
                        context.Resolve<MongoLayoutDatabaseInitializer>(),
                        context.Resolve<ProcessingTools.Data.Mongo.Bio.Taxonomy.MongoBiotaxonomyDatabaseInitializer>(),
                   };
               })
               .As<Func<IEnumerable<IDatabaseInitializer>>>()
               .InstancePerDependency();

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

            // Layout DB provider
            builder
                .RegisterType<MongoDatabaseProvider>()
                .As<IMongoDatabaseProvider>()
                .Named<IMongoDatabaseProvider>(InjectionConstants.MongoDBLayoutDatabaseBindingName)
                .WithParameter(InjectionConstants.ConnectionStringParameterName, this.Configuration.GetConnectionString(ConfigurationConstants.LayoutDatabaseMongoDBConnectionStringName))
                .WithParameter(InjectionConstants.DatabaseNameParameterName, this.Configuration[ConfigurationConstants.LayoutMongoDBDatabaseName])
                .InstancePerLifetimeScope();

            // Bio-taxonomy DB provider
            builder
                .RegisterType<MongoDatabaseProvider>()
                .As<IMongoDatabaseProvider>()
                .Named<IMongoDatabaseProvider>(InjectionConstants.MongoDBBiotaxonomyDatabaseBindingName)
                .WithParameter(InjectionConstants.ConnectionStringParameterName, this.Configuration.GetConnectionString(ConfigurationConstants.BiotaxonomyDatabaseMongoDBConnectionStringName))
                .WithParameter(InjectionConstants.DatabaseNameParameterName, this.Configuration[ConfigurationConstants.BiotaxonomyMongoDBDatabaseName])
                .InstancePerLifetimeScope();

            // History DB provider
            builder
                .RegisterType<MongoDatabaseProvider>()
                .As<IMongoDatabaseProvider>()
                .Named<IMongoDatabaseProvider>(InjectionConstants.MongoDBHistoryDatabaseBindingName)
                .WithParameter(InjectionConstants.ConnectionStringParameterName, this.Configuration.GetConnectionString(ConfigurationConstants.HistoryDatabaseMongoDBConnectionStringName))
                .WithParameter(InjectionConstants.DatabaseNameParameterName, this.Configuration[ConfigurationConstants.HistoryMongoDBDatabaseName])
                .InstancePerLifetimeScope();

            // History DAO
            builder
                .RegisterType<MongoObjectHistoryDataAccessObject>()
                .As<IObjectHistoryDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBHistoryDatabaseBindingName)))
                .InstancePerLifetimeScope();

            // Documents DAO
            builder
                .RegisterType<MongoPublishersDataAccessObject>()
                .As<IPublishersDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerLifetimeScope();
            builder
                .RegisterType<MongoJournalsDataAccessObject>()
                .As<IJournalsDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerLifetimeScope();
            builder
                .RegisterType<MongoArticlesDataAccessObject>()
                .As<IArticlesDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerLifetimeScope();
            builder
                .RegisterType<MongoDocumentsDataAccessObject>()
                .As<IDocumentsDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerLifetimeScope();
            builder
                .RegisterType<MongoFilesDataAccessObject>()
                .As<IFilesDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerLifetimeScope();

            // Documents DB initializer
            builder
                .RegisterType<MongoDocumentsDatabaseInitializer>()
                .AsSelf()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerDependency();

            // Files DAO
            builder
                .RegisterType<MongoMediatypesDataAccessObject>()
                .As<IMediatypesDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBFilesDatabaseBindingName)))
                .InstancePerLifetimeScope();

            // Files DB initializer
            builder
                .RegisterType<MongoFilesDatabaseInitializer>()
                .AsSelf()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBFilesDatabaseBindingName)))
                .InstancePerDependency();

            // Layout DAO
            builder
                .RegisterType<MongoFloatObjectTagStylesDataAccessObject>()
                .As<IFloatObjectTagStylesDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBLayoutDatabaseBindingName)))
                .InstancePerLifetimeScope();
            builder
                .RegisterType<MongoFloatObjectParseStylesDataAccessObject>()
                .As<IFloatObjectParseStylesDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBLayoutDatabaseBindingName)))
                .InstancePerLifetimeScope();
            builder
                .RegisterType<MongoReferenceTagStylesDataAccessObject>()
                .As<IReferenceTagStylesDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBLayoutDatabaseBindingName)))
                .InstancePerLifetimeScope();
            builder
                .RegisterType<MongoReferenceParseStylesDataAccessObject>()
                .As<IReferenceParseStylesDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBLayoutDatabaseBindingName)))
                .InstancePerLifetimeScope();
            builder
                .RegisterType<MongoJournalStylesDataAccessObject>()
                .As<IJournalStylesDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBLayoutDatabaseBindingName)))
                .InstancePerLifetimeScope();

            // Layout DB initializer
            builder
                .RegisterType<MongoLayoutDatabaseInitializer>()
                .AsSelf()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBLayoutDatabaseBindingName)))
                .InstancePerDependency();

            // Biotaxonomy DAO
            builder
                .RegisterMongoCollectionBinding<ProcessingTools.Data.Models.Mongo.Bio.Taxonomy.BlackListItem>(InjectionConstants.MongoDBBiotaxonomyDatabaseBindingName)
                .RegisterMongoCollectionBinding<ProcessingTools.Data.Models.Mongo.Bio.Taxonomy.TaxonRankItem>(InjectionConstants.MongoDBBiotaxonomyDatabaseBindingName);
            builder
                .RegisterType<ProcessingTools.Data.Mongo.Bio.Taxonomy.MongoBlackListDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Bio.Taxonomy.IBlackListDataAccessObject>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.Data.Mongo.Bio.Taxonomy.MongoTaxonRanksDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Bio.Taxonomy.ITaxonRanksDataAccessObject>()
                .InstancePerLifetimeScope();

            // Biotaxonomy DB initializer
            builder
                .RegisterType<ProcessingTools.Data.Mongo.Bio.Taxonomy.MongoBiotaxonomyDatabaseInitializer>()
                .AsSelf()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBBiotaxonomyDatabaseBindingName)))
                .InstancePerDependency();
        }
    }
}
