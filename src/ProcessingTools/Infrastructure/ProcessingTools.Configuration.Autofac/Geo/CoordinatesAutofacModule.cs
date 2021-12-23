// <copyright file="CoordinatesAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Autofac.Geo
{
    using System;
    using global::Autofac;
    using ProcessingTools.Contracts.Models.Geo.Coordinates;
    using ProcessingTools.Contracts.Services.Geo.Coordinates;
    using ProcessingTools.Services.Geo.Coordinates;

    /// <summary>
    /// Coordinates autofac module.
    /// </summary>
    public class CoordinatesAutofacModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

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
