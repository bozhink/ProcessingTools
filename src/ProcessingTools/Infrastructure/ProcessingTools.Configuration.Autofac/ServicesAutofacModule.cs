// <copyright file="ServicesAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Autofac
{
    using global::Autofac;
    using Microsoft.Extensions.Configuration;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services.Bio.Biorepositories.Admin;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Documents;
    using ProcessingTools.Contracts.Services.Files;
    using ProcessingTools.Contracts.Services.Geo.Coordinates;
    using ProcessingTools.Contracts.Services.History;
    using ProcessingTools.Contracts.Services.IO;
    using ProcessingTools.Contracts.Services.Layout.Styles;
    using ProcessingTools.Contracts.Services.Rules;
    using ProcessingTools.Contracts.Services.Tools;
    using ProcessingTools.Services.Admin;
    using ProcessingTools.Services.Documents;
    using ProcessingTools.Services.Geo.Coordinates;
    using ProcessingTools.Services.History;
    using ProcessingTools.Services.IO;
    using ProcessingTools.Services.Layout.Styles;
    using ProcessingTools.Services.Rules;
    using ProcessingTools.Services.Tools;
    using IFilesDataService = ProcessingTools.Contracts.Services.Documents.IFilesDataService;

    /// <summary>
    /// Autofac bindings for ProcessingTools.Services.*.
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
                .As<IBlackListDataService>()
                .InstancePerDependency();
            builder
                .RegisterType<ProcessingTools.Services.Bio.Taxonomy.TaxonRankDataService>()
                .As<ITaxonRankDataService>()
                .InstancePerDependency();

            builder
                .RegisterType<Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider>()
                .As<Microsoft.AspNetCore.StaticFiles.IContentTypeProvider>().
                SingleInstance();
            builder
                .RegisterType<ProcessingTools.Web.Services.Files.MimeMappingService>()
                .As<IMimeMappingService>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.Services.Files.MediatypesDataService>()
                .As<IMediatypesDataService>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.Services.Files.FilesDataService>()
                .As<Contracts.Services.Files.IFilesDataService>()
                .InstancePerDependency();
            builder
                .RegisterType<ProcessingTools.Services.Files.FileNameGeneratorWithUniqueNaming>()
                .As<IFileNameGenerator>()
                .InstancePerLifetimeScope();
            builder
                .Register(c =>
                {
                    return new ProcessingTools.Services.Files.FileNameResolver
                    {
                        BaseDirectoryName = this.Configuration[ConfigurationConstants.FilesRootDirectory],
                    };
                })
                .As<IFileNameResolver>()
                .InstancePerLifetimeScope();
        }
    }
}
