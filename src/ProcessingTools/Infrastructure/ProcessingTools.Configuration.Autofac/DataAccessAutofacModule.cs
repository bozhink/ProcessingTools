﻿// <copyright file="DataAccessAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Autofac
{
    using global::Autofac;
    using global::Autofac.Core;
    using Microsoft.Extensions.Configuration;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Data.Mongo;

    /// <summary>
    /// Autofac bindings for ProcessingTools.DataAccess.*.
    /// </summary>
    public class DataAccessAutofacModule : Module
    {
        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            if (builder is null)
            {
                throw new System.ArgumentNullException(nameof(builder));
            }

            // History DAO
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.History.MongoObjectHistoryDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.History.IObjectHistoryDataAccessObject>()
                .InstancePerLifetimeScope();

            // Documents DAO
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Documents.MongoPublishersDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Documents.IPublishersDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Documents.MongoJournalsDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Documents.IJournalsDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Documents.MongoArticlesDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Documents.IArticlesDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Documents.MongoDocumentsDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Documents.IDocumentsDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Documents.MongoFilesDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Documents.IFilesDataAccessObject>()
                .WithParameter(
                    new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(IMongoDatabaseProvider),
                        (p, c) => c.ResolveNamed<IMongoDatabaseProvider>(InjectionConstants.MongoDBDocumentsDatabaseBindingName)))
                .InstancePerLifetimeScope();

            // Files DAO
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Files.MongoMediatypesDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Files.IMediatypesDataAccessObject>()
                .InstancePerLifetimeScope();

            // Layout DAO
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Layout.Styles.MongoFloatObjectTagStylesDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Layout.Styles.IFloatObjectTagStylesDataAccessObject>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Layout.Styles.MongoFloatObjectParseStylesDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Layout.Styles.IFloatObjectParseStylesDataAccessObject>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Layout.Styles.MongoReferenceTagStylesDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Layout.Styles.IReferenceTagStylesDataAccessObject>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Layout.Styles.MongoReferenceParseStylesDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Layout.Styles.IReferenceParseStylesDataAccessObject>().InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Layout.Styles.MongoJournalStylesDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Layout.Styles.IJournalStylesDataAccessObject>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Layout.Templates.MongoHandlebarsTemplatesDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Layout.Templates.IHandlebarsTemplatesDataAccessObject>()
                .InstancePerLifetimeScope();

            // Biotaxonomy
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Bio.Taxonomy.MongoBlackListDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Bio.Taxonomy.IBlackListDataAccessObject>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Bio.Taxonomy.MongoWhiteListDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Bio.Taxonomy.IWhiteListDataAccessObject>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Bio.Taxonomy.MongoTaxonRanksDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Bio.Taxonomy.ITaxonRanksDataAccessObject>()
                .InstancePerLifetimeScope();

            // Biorepositories
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Bio.Biorepositories.MongoCollectionLabelsDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Bio.Biorepositories.ICollectionLabelsDataAccessObject>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Bio.Biorepositories.MongoCollectionPerLabelsDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Bio.Biorepositories.ICollectionPerLabelsDataAccessObject>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Bio.Biorepositories.MongoCollectionPersDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Bio.Biorepositories.ICollectionPersDataAccessObject>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Bio.Biorepositories.MongoCollectionsDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Bio.Biorepositories.ICollectionsDataAccessObject>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Bio.Biorepositories.MongoInstitutionLabelsDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Bio.Biorepositories.IInstitutionLabelsDataAccessObject>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Bio.Biorepositories.MongoInstitutionsDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Bio.Biorepositories.IInstitutionsDataAccessObject>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Bio.Biorepositories.MongoStaffDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Bio.Biorepositories.IStaffDataAccessObject>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.DataAccess.Mongo.Bio.Biorepositories.MongoStaffLabelsDataAccessObject>()
                .As<ProcessingTools.Contracts.DataAccess.Bio.Biorepositories.IStaffLabelsDataAccessObject>()
                .InstancePerLifetimeScope();
        }
    }
}