﻿// <copyright file="ServicesWebAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Autofac;
    using ProcessingTools.Web.Services.Admin;
    using ProcessingTools.Web.Services.Contracts.Admin;
    using ProcessingTools.Web.Services.Contracts.Documents;
    using ProcessingTools.Web.Services.Contracts.Layout.Styles;
    using ProcessingTools.Web.Services.Documents;
    using ProcessingTools.Web.Services.Layout.Styles;

    /// <summary>
    /// Autofac bindings for ProcessingTools.Web.Services.*
    /// </summary>
    public class ServicesWebAutofacModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PublishersService>().As<IPublishersService>().InstancePerDependency();
            builder.RegisterType<JournalsService>().As<IJournalsService>().InstancePerDependency();
            builder.RegisterType<ArticlesService>().As<IArticlesService>().InstancePerDependency();
            builder.RegisterType<DocumentsService>().As<IDocumentsService>().InstancePerDependency();

            builder.RegisterType<FloatObjectTagStylesService>().As<IFloatObjectTagStylesService>().InstancePerDependency();

            builder.RegisterType<DatabasesService>().As<IDatabasesService>().InstancePerDependency();
        }
    }
}
