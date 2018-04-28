// <copyright file="DataAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using System;
    using System.Collections.Generic;
    using Autofac;
    using Autofac.Core;
    using Microsoft.Extensions.Configuration;
    using ProcessingTools.Constants;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Data.Contracts.History;
    using ProcessingTools.Data.Contracts.Layout.Styles;
    using ProcessingTools.Data.Documents.Mongo;
    using ProcessingTools.Data.History.Mongo;
    using ProcessingTools.Data.Layout.Mongo;

    /// <summary>
    /// Autofac bindings for ProcessingTools.Data.*
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
                        context.Resolve<MongoLayoutDatabaseInitializer>()
                   };
               })
               .As<Func<IEnumerable<IDatabaseInitializer>>>()
               .InstancePerDependency();

            builder
                .RegisterType<MongoDatabaseProvider>()
                .As<IMongoDatabaseProvider>()
                .Named<IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)
                .WithParameter(InjectionConstants.ConnectionStringParameterName, this.Configuration.GetConnectionString(ConfigurationConstants.DocumentsDatabaseMongoDBConnectionStringName))
                .WithParameter(InjectionConstants.DatabaseNameParameterName, this.Configuration[ConfigurationConstants.DocumentsMongoDBDatabaseName])
                .InstancePerLifetimeScope();

            builder
                .RegisterType<MongoDatabaseProvider>()
                .As<IMongoDatabaseProvider>()
                .Named<IMongoDatabaseProvider>(InjectionConstants.MongoDBLayoutDatabaseBindingName)
                .WithParameter(InjectionConstants.ConnectionStringParameterName, this.Configuration.GetConnectionString(ConfigurationConstants.DocumentsDatabaseMongoDBConnectionStringName))
                .WithParameter(InjectionConstants.DatabaseNameParameterName, this.Configuration[ConfigurationConstants.LayoutMongoDBDatabaseName])
                .InstancePerLifetimeScope();

            builder
                .RegisterType<MongoDatabaseProvider>()
                .As<IMongoDatabaseProvider>()
                .Named<IMongoDatabaseProvider>(InjectionConstants.MongoDBHistoryDatabaseBindingName)
                .WithParameter(InjectionConstants.ConnectionStringParameterName, this.Configuration.GetConnectionString(ConfigurationConstants.HistoryDatabaseMongoDBConnectionStringName))
                .WithParameter(InjectionConstants.DatabaseNameParameterName, this.Configuration[ConfigurationConstants.HistoryMongoDBDatabaseName])
                .InstancePerLifetimeScope();

            builder
                .RegisterType<MongoObjectHistoryDataAccessObject>()
                .As<IObjectHistoryDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBHistoryDatabaseBindingName)))
                .InstancePerLifetimeScope();

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

            builder
                .RegisterType<MongoDocumentsDatabaseInitializer>()
                .AsSelf()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerDependency();

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
                .RegisterType<MongoLayoutDatabaseInitializer>()
                .AsSelf()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBLayoutDatabaseBindingName)))
                .InstancePerDependency();
        }
    }
}
