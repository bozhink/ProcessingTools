// <copyright file="ProcessorsAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using System;
    using Autofac;
    using ProcessingTools.Common.Code;
    using ProcessingTools.Contracts;
    using ProcessingTools.Geo;
    using ProcessingTools.Geo.Contracts;
    using ProcessingTools.Processors.Contracts.Documents;
    using ProcessingTools.Processors.Contracts.Geo.Coordinates;
    using ProcessingTools.Processors.Contracts.Layout;
    using ProcessingTools.Processors.Contracts.References;
    using ProcessingTools.Processors.Contracts.Rules;
    using ProcessingTools.Processors.Documents;
    using ProcessingTools.Processors.Geo.Coordinates;
    using ProcessingTools.Processors.Imaging;
    using ProcessingTools.Processors.Imaging.Contracts;
    using ProcessingTools.Processors.Layout;
    using ProcessingTools.Processors.References;
    using ProcessingTools.Processors.Rules;

    /// <summary>
    /// Autofac bindings for ProcessingTools.Processors.*
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

            builder.RegisterType<CoordinateParser>().As<ICoordinateParser>().InstancePerLifetimeScope();
            builder.RegisterType<Coordinate2DParser>().As<ICoordinate2DParser>().InstancePerLifetimeScope();
            builder.RegisterType<UtmCoordinatesTransformer>().As<IUtmCoordinatesTransformer>().InstancePerLifetimeScope();
            builder.RegisterType<UtmCoordinatesConverter>().As<IUtmCoordinatesConverter>().InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.Processors.Models.Geo.Coordinates.Coordinate>()
                .As<ProcessingTools.Processors.Models.Contracts.Geo.Coordinates.ICoordinate>()
                .InstancePerDependency();
            builder
                .Register<Func<ProcessingTools.Processors.Models.Contracts.Geo.Coordinates.ICoordinate>>(ctx =>
                {
                    var context = ctx.Resolve<IComponentContext>();
                    return () => context.Resolve<ProcessingTools.Processors.Models.Contracts.Geo.Coordinates.ICoordinate>();
                })
                .As<Func<ProcessingTools.Processors.Models.Contracts.Geo.Coordinates.ICoordinate>>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ProcessingTools.Processors.Models.Geo.Coordinates.CoordinatePart>()
                .As<ProcessingTools.Processors.Models.Contracts.Geo.Coordinates.ICoordinatePart>()
                .InstancePerDependency();
            builder
                .Register<Func<ProcessingTools.Processors.Models.Contracts.Geo.Coordinates.ICoordinatePart>>(ctx =>
                {
                    var context = ctx.Resolve<IComponentContext>();
                    return () => context.Resolve<ProcessingTools.Processors.Models.Contracts.Geo.Coordinates.ICoordinatePart>();
                })
                .As<Func<ProcessingTools.Processors.Models.Contracts.Geo.Coordinates.ICoordinatePart>>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CoordinateFactory>().As<ICoordinateFactory>().InstancePerLifetimeScope();
        }
    }
}
