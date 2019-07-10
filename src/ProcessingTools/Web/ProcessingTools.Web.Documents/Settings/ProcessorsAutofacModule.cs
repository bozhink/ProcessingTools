// <copyright file="ProcessorsAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services;
using ProcessingTools.Contracts.Services.Documents;
using ProcessingTools.Contracts.Services.Geo.Coordinates;
using ProcessingTools.Contracts.Services.Imaging;
using ProcessingTools.Contracts.Services.Layout;
using ProcessingTools.Contracts.Services.Models.Geo.Coordinates;
using ProcessingTools.Contracts.Services.References;
using ProcessingTools.Contracts.Services.Rules;

namespace ProcessingTools.Web.Documents.Settings
{
    using System;
    using Autofac;
    using ProcessingTools.Services.Documents;
    using ProcessingTools.Services.Geo.Coordinates;
    using ProcessingTools.Services.Imaging;
    using ProcessingTools.Services.Layout;
    using ProcessingTools.Services.References;
    using ProcessingTools.Services.Rules;
    using ProcessingTools.Services;

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
                .As<ICoordinate>()
                .InstancePerDependency();
            builder
                .Register<Func<ICoordinate>>(ctx =>
                {
                    var context = ctx.Resolve<IComponentContext>();
                    return () => context.Resolve<ICoordinate>();
                })
                .As<Func<ICoordinate>>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.Services.Models.Geo.Coordinates.CoordinatePart>()
                .As<ICoordinatePart>()
                .InstancePerDependency();
            builder
                .Register<Func<ICoordinatePart>>(ctx =>
                {
                    var context = ctx.Resolve<IComponentContext>();
                    return () => context.Resolve<ICoordinatePart>();
                })
                .As<Func<ICoordinatePart>>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CoordinateFactory>().As<ICoordinateFactory>().InstancePerLifetimeScope();
        }
    }
}
