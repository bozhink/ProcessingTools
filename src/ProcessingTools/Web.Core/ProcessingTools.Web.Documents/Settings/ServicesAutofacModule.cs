// <copyright file="ServicesAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Autofac;
    using ProcessingTools.Services.Admin;
    using ProcessingTools.Services.Contracts.Admin;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Contracts.History;
    using ProcessingTools.Services.Contracts.IO;
    using ProcessingTools.Services.Contracts.Layout.Styles;
    using ProcessingTools.Services.Documents;
    using ProcessingTools.Services.History;
    using ProcessingTools.Services.IO;
    using ProcessingTools.Services.Layout.Styles;

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

            builder.RegisterType<FloatObjectTagStylesDataService>().As<IFloatObjectTagStylesDataService>().InstancePerDependency();

            builder.RegisterType<ArticlesService>().As<IArticlesService>().InstancePerDependency();
            builder.RegisterType<DocumentsService>().As<IDocumentsService>().InstancePerDependency();
            builder.RegisterType<XmlPresenter>().As<IXmlPresenter>().InstancePerDependency();

            builder.RegisterType<XmlReadService>().As<IXmlReadService>().InstancePerDependency();

            builder.RegisterType<DatabasesService>().As<IDatabasesService>().InstancePerDependency();
        }
    }
}
