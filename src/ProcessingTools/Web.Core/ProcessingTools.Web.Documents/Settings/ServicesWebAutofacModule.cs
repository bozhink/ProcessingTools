// <copyright file="ServicesWebAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Autofac;
    using ProcessingTools.Web.Services.Admin;
    using ProcessingTools.Web.Services.Contracts.Admin;
    using ProcessingTools.Web.Services.Contracts.Documents;
    using ProcessingTools.Web.Services.Contracts.Geo.Coordinates;
    using ProcessingTools.Web.Services.Contracts.Layout.Styles;
    using ProcessingTools.Web.Services.Documents;
    using ProcessingTools.Web.Services.Geo.Coordinates;
    using ProcessingTools.Web.Services.Layout.Styles;

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

            builder.RegisterType<FloatObjectTagStylesService>().As<IFloatObjectTagStylesService>().InstancePerDependency();
            builder.RegisterType<FloatObjectParseStylesService>().As<IFloatObjectParseStylesService>().InstancePerDependency();
            builder.RegisterType<ReferenceTagStylesService>().As<IReferenceTagStylesService>().InstancePerDependency();
            builder.RegisterType<ReferenceParseStylesService>().As<IReferenceParseStylesService>().InstancePerDependency();
            builder.RegisterType<JournalStylesService>().As<IJournalStylesService>().InstancePerDependency();

            builder.RegisterType<CoordinatesCalculatorWebService>().As<ICoordinatesCalculatorWebService>().InstancePerDependency();

            builder.RegisterType<DatabasesWebService>().As<IDatabasesWebService>().InstancePerDependency();
        }
    }
}
