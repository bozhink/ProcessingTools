// <copyright file="ServicesAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Autofac;
    using Microsoft.Extensions.Configuration;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Services.Admin;
    using ProcessingTools.Services.Contracts.Admin;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Contracts.Geo.Coordinates;
    using ProcessingTools.Services.Contracts.History;
    using ProcessingTools.Services.Contracts.IO;
    using ProcessingTools.Services.Contracts.Layout.Styles;
    using ProcessingTools.Services.Contracts.Rules;
    using ProcessingTools.Services.Contracts.Tools;
    using ProcessingTools.Services.Documents;
    using ProcessingTools.Services.Geo.Coordinates;
    using ProcessingTools.Services.History;
    using ProcessingTools.Services.IO;
    using ProcessingTools.Services.Layout.Styles;
    using ProcessingTools.Services.Rules;
    using ProcessingTools.Services.Tools;

    /// <summary>
    /// Autofac bindings for ProcessingTools.Services.*
    /// </summary>
    public class ServicesAutofacModule : Module
    {
        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ObjectHistoryDataService>().As<IObjectHistoryDataService>().InstancePerLifetimeScope();

            builder.RegisterType<PublishersDataService>().As<IPublishersDataService>().InstancePerLifetimeScope();
            builder.RegisterType<JournalsDataService>().As<IJournalsDataService>().InstancePerLifetimeScope();
            builder.RegisterType<ArticlesDataService>().As<IArticlesDataService>().InstancePerLifetimeScope();
            builder.RegisterType<DocumentsDataService>().As<IDocumentsDataService>().InstancePerLifetimeScope();
            builder.RegisterType<FilesDataService>().As<IFilesDataService>().InstancePerLifetimeScope();

            builder.RegisterType<JournalsService>().As<IJournalsService>().InstancePerLifetimeScope();
            builder.RegisterType<ArticlesService>().As<IArticlesService>().InstancePerLifetimeScope();
            builder.RegisterType<DocumentsService>().As<IDocumentsService>().InstancePerLifetimeScope();
            builder.RegisterType<DocumentMetaResolver>().As<IDocumentMetaResolver>().InstancePerLifetimeScope();
            builder.RegisterType<DocumentMetaService>().As<IDocumentMetaService>().InstancePerLifetimeScope();
            builder.RegisterType<XmlPresenter>().As<IXmlPresenter>().InstancePerLifetimeScope();

            builder.RegisterType<DocumentProcessingService>().As<IDocumentProcessingService>().InstancePerLifetimeScope();

            builder.RegisterType<XmlReadService>().As<IXmlReadService>().InstancePerLifetimeScope();

            builder.RegisterType<FloatObjectTagStylesDataService>().As<IFloatObjectTagStylesDataService>().InstancePerLifetimeScope();
            builder.RegisterType<FloatObjectParseStylesDataService>().As<IFloatObjectParseStylesDataService>().InstancePerLifetimeScope();
            builder.RegisterType<ReferenceTagStylesDataService>().As<IReferenceTagStylesDataService>().InstancePerLifetimeScope();
            builder.RegisterType<ReferenceParseStylesDataService>().As<IReferenceParseStylesDataService>().InstancePerLifetimeScope();
            builder.RegisterType<JournalStylesDataService>().As<IJournalStylesDataService>().InstancePerLifetimeScope();

            builder.RegisterType<JournalStylesService>().As<IJournalStylesService>().PropertiesAutowired(PropertyWiringOptions.PreserveSetValues).InstancePerLifetimeScope();

            builder.RegisterType<YamlSourceXmlReplaceRuleSetParser>().As<IXmlReplaceRuleSetParser>().InstancePerLifetimeScope();

            builder.RegisterType<DatabasesService>().As<IDatabasesService>().InstancePerLifetimeScope();

            builder.RegisterType<CoordinatesParseService>().As<ICoordinatesParseService>().InstancePerLifetimeScope();

            builder.RegisterType<DecodeService>().As<IDecodeService>().InstancePerLifetimeScope();
            builder.RegisterType<EncodeService>().As<IEncodeService>().InstancePerLifetimeScope();
            builder.RegisterType<HashService>().As<IHashService>().InstancePerLifetimeScope();

            builder
                .RegisterType<ProcessingTools.Services.Bio.Taxonomy.BlackListDataService>()
                .As<ProcessingTools.Services.Contracts.Bio.Taxonomy.IBlackListDataService>()
                .InstancePerDependency();
            builder
                .RegisterType<ProcessingTools.Services.Bio.Taxonomy.TaxonRankDataService>()
                .As<ProcessingTools.Services.Contracts.Bio.Taxonomy.ITaxonRankDataService>()
                .InstancePerDependency();

            builder
                .RegisterType<Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider>()
                .As<Microsoft.AspNetCore.StaticFiles.IContentTypeProvider>().
                SingleInstance();
            builder
                .RegisterType<ProcessingTools.Web.Services.Files.MimeMappingService>()
                .As<ProcessingTools.Services.Contracts.Files.IMimeMappingService>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.Services.Files.MediatypesDataService>()
                .As<ProcessingTools.Services.Contracts.Files.IMediatypesDataService>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.Services.Files.FilesDataService>()
                .As<ProcessingTools.Services.Contracts.Files.IFilesDataService>()
                .InstancePerDependency();
            builder
                .RegisterType<ProcessingTools.Services.Files.FileNameGeneratorWithUniqueNaming>()
                .As<ProcessingTools.Services.Contracts.Files.IFileNameGenerator>()
                .InstancePerLifetimeScope();
            builder
                .Register(c =>
                {
                    return new ProcessingTools.Services.Files.FileNameResolver
                    {
                        BaseDirectoryName = this.Configuration[ConfigurationConstants.FilesRootDirectory]
                    };
                })
                .As<ProcessingTools.Services.Contracts.Files.IFileNameResolver>()
                .InstancePerLifetimeScope();
        }
    }
}
