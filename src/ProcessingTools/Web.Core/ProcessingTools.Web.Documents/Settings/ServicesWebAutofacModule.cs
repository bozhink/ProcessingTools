﻿// <copyright file="ServicesWebAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Autofac;
    using ProcessingTools.Web.Services.Admin;
    using ProcessingTools.Web.Services.Contracts.Admin;
    using ProcessingTools.Web.Services.Contracts.Documents;
    using ProcessingTools.Web.Services.Contracts.Files;
    using ProcessingTools.Web.Services.Contracts.Geo.Coordinates;
    using ProcessingTools.Web.Services.Contracts.Layout.Styles;
    using ProcessingTools.Web.Services.Contracts.Tools;
    using ProcessingTools.Web.Services.Documents;
    using ProcessingTools.Web.Services.Files;
    using ProcessingTools.Web.Services.Geo.Coordinates;
    using ProcessingTools.Web.Services.Layout.Styles;
    using ProcessingTools.Web.Services.Tools;

    /// <summary>
    /// Autofac bindings for ProcessingTools.Web.Services.*
    /// </summary>
    public class ServicesWebAutofacModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PublishersWebService>().As<IPublishersWebService>().InstancePerDependency();
            builder.RegisterType<JournalsWebService>().As<IJournalsWebService>().InstancePerDependency();
            builder.RegisterType<ArticlesWebService>().As<IArticlesWebService>().InstancePerDependency();
            builder.RegisterType<DocumentsWebService>().As<IDocumentsWebService>().InstancePerDependency();

            builder.RegisterType<MediatypesWebService>().As<IMediatypesWebService>().InstancePerDependency();

            builder.RegisterType<FloatObjectTagStylesWebService>().As<IFloatObjectTagStylesWebService>().InstancePerDependency();
            builder.RegisterType<FloatObjectParseStylesWebService>().As<IFloatObjectParseStylesWebService>().InstancePerDependency();
            builder.RegisterType<ReferenceTagStylesWebService>().As<IReferenceTagStylesWebService>().InstancePerDependency();
            builder.RegisterType<ReferenceParseStylesWebService>().As<IReferenceParseStylesWebService>().InstancePerDependency();
            builder.RegisterType<JournalStylesWebService>().As<IJournalStylesWebService>().InstancePerDependency();

            builder.RegisterType<CoordinatesCalculatorWebService>().As<ICoordinatesCalculatorWebService>().InstancePerDependency();
            builder.RegisterType<DecodeWebService>().As<IDecodeWebService>().InstancePerDependency();
            builder.RegisterType<EncodeWebService>().As<IEncodeWebService>().InstancePerDependency();
            builder.RegisterType<HashesWebService>().As<IHashesWebService>().InstancePerDependency();
            builder.RegisterType<BarcodeWebService>().As<IBarcodeWebService>().InstancePerDependency();

            builder.RegisterType<DatabasesWebService>().As<IDatabasesWebService>().InstancePerDependency();
        }
    }
}
