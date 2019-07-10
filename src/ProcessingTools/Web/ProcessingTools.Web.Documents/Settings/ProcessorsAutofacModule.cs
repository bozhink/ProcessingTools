// <copyright file="ProcessorsAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using System;
    using Autofac;
    using ProcessingTools.Geo;
    using ProcessingTools.Geo.Contracts;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Contracts.Geo.Coordinates;
    using ProcessingTools.Services.Contracts.Layout;
    using ProcessingTools.Services.Contracts.References;
    using ProcessingTools.Services.Contracts.Rules;
    using ProcessingTools.Services.Documents;
    using ProcessingTools.Services.Geo.Coordinates;
    using ProcessingTools.Services.Imaging;
    using ProcessingTools.Services.Imaging.Contracts;
    using ProcessingTools.Services.Layout;
    using ProcessingTools.Services.References;
    using ProcessingTools.Services.Rules;
    using ProcessingTools.Services;
    using ProcessingTools.Services.Contracts;

    /// <summary>
    /// Autofac bindings for ProcessingTools.Services.*.
    /// </summary>
    public class ProcessorsAutofacModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TaxPubDocumentFactory>().As<IDocumentFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DocumentSchemaNormalizer>().As<IDocumentSchemaNormalizer>().InstancePerLifetimeScope();
            builder.RegisterType<DocumentMetaUpdater>().As<IDocumentMetaUpdater>().InstancePerLifetimeScope();
            builder.RegisterType<NormalizationTransformerFactory>().As<INormalizationTransformerFactory>().InstancePerLifetimeScope();

            builder.RegisterType<DocumentRulesProcessor>().As<IDocumentRulesProcessor>().InstancePerDependency();
            builder.RegisterType<XmlContextRulesProcessor>().As<IXmlContextRulesProcessor>().InstancePerDependency();
            builder.RegisterType<ReferencesParser>().As<IReferencesParser>().InstancePerDependency();
            builder.RegisterType<ReferencesTagger>().As<IReferencesTagger>().InstancePerDependency();

            builder.RegisterType<QRCodeEncoder>().As<IQRCodeEncoder>().InstancePerLifetimeScope();
            builder.RegisterType<BarcodeEncoder>().As<IBarcodeEncoder>().InstancePerLifetimeScope();

            builder.RegisterType<CoordinateParser>().As<ICoordinateParser>().InstancePerLifetimeScope();
            builder.RegisterType<Coordinate2DParser>().As<ICoordinate2DParser>().InstancePerLifetimeScope();
            builder.RegisterType<UtmCoordinatesTransformer>().As<IUtmCoordinatesTransformer>().InstancePerLifetimeScope();
            builder.RegisterType<UtmCoordinatesConverter>().As<IUtmCoordinatesConverter>().InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.Services.Models.Geo.Coordinates.Coordinate>()
                .As<ProcessingTools.Services.Models.Contracts.Geo.Coordinates.ICoordinate>()
                .InstancePerDependency();
            builder
                .Register<Func<ProcessingTools.Services.Models.Contracts.Geo.Coordinates.ICoordinate>>(ctx =>
                {
                    var context = ctx.Resolve<IComponentContext>();
                    return () => context.Resolve<ProcessingTools.Services.Models.Contracts.Geo.Coordinates.ICoordinate>();
                })
                .As<Func<ProcessingTools.Services.Models.Contracts.Geo.Coordinates.ICoordinate>>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.Services.Models.Geo.Coordinates.CoordinatePart>()
                .As<ProcessingTools.Services.Models.Contracts.Geo.Coordinates.ICoordinatePart>()
                .InstancePerDependency();
            builder
                .Register<Func<ProcessingTools.Services.Models.Contracts.Geo.Coordinates.ICoordinatePart>>(ctx =>
                {
                    var context = ctx.Resolve<IComponentContext>();
                    return () => context.Resolve<ProcessingTools.Services.Models.Contracts.Geo.Coordinates.ICoordinatePart>();
                })
                .As<Func<ProcessingTools.Services.Models.Contracts.Geo.Coordinates.ICoordinatePart>>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CoordinateFactory>().As<ICoordinateFactory>().InstancePerLifetimeScope();
        }
    }
}
