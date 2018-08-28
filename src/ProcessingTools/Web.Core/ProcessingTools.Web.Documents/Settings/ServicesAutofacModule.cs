// <copyright file="ServicesAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Autofac;
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
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ObjectHistoryDataService>().As<IObjectHistoryDataService>().InstancePerDependency();

            builder.RegisterType<PublishersDataService>().As<IPublishersDataService>().InstancePerDependency();
            builder.RegisterType<JournalsDataService>().As<IJournalsDataService>().InstancePerDependency();
            builder.RegisterType<ArticlesDataService>().As<IArticlesDataService>().InstancePerDependency();
            builder.RegisterType<DocumentsDataService>().As<IDocumentsDataService>().InstancePerDependency();
            builder.RegisterType<FilesDataService>().As<IFilesDataService>().InstancePerDependency();

            builder.RegisterType<JournalsService>().As<IJournalsService>().InstancePerDependency();
            builder.RegisterType<ArticlesService>().As<IArticlesService>().InstancePerDependency();
            builder.RegisterType<DocumentsService>().As<IDocumentsService>().InstancePerDependency();
            builder.RegisterType<XmlPresenter>().As<IXmlPresenter>().InstancePerDependency();

            builder.RegisterType<DocumentProcessingService>().As<IDocumentProcessingService>().InstancePerDependency();

            builder.RegisterType<XmlReadService>().As<IXmlReadService>().InstancePerDependency();

            builder.RegisterType<FloatObjectTagStylesDataService>().As<IFloatObjectTagStylesDataService>().InstancePerDependency();
            builder.RegisterType<FloatObjectParseStylesDataService>().As<IFloatObjectParseStylesDataService>().InstancePerDependency();
            builder.RegisterType<ReferenceTagStylesDataService>().As<IReferenceTagStylesDataService>().InstancePerDependency();
            builder.RegisterType<ReferenceParseStylesDataService>().As<IReferenceParseStylesDataService>().InstancePerDependency();
            builder.RegisterType<JournalStylesDataService>().As<IJournalStylesDataService>().InstancePerDependency();

            builder.RegisterType<JournalStylesService>().As<IJournalStylesService>().PropertiesAutowired(PropertyWiringOptions.PreserveSetValues).InstancePerDependency();

            builder.RegisterType<YamlSourceXmlReplaceRuleSetParser>().As<IXmlReplaceRuleSetParser>().InstancePerLifetimeScope();

            builder.RegisterType<DatabasesService>().As<IDatabasesService>().InstancePerDependency();

            builder.RegisterType<CoordinatesParseService>().As<ICoordinatesParseService>().InstancePerDependency();

            builder.RegisterType<DecodeService>().As<IDecodeService>().InstancePerLifetimeScope();
            builder.RegisterType<EncodeService>().As<IEncodeService>().InstancePerLifetimeScope();
        }
    }
}
