// <copyright file="XmlTransformersAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using System;
    using Autofac;
    using Autofac.Core;
    using Microsoft.Extensions.Configuration;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Processors.Contracts.Layout;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Xml;

    /// <summary>
    /// Autofac bindings for the XML transformers.
    /// </summary>
    public class XmlTransformersAutofacModule : Module
    {
        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<XslTransformCacheFromFile>()
                .As<IXslTransformCache>()
                .Named<IXslTransformCache>(nameof(XslTransformCacheFromFile))
                .InstancePerDependency();
            builder
                .RegisterType<XslTransformCacheFromContent>()
                .As<IXslTransformCache>()
                .Named<IXslTransformCache>(nameof(XslTransformCacheFromContent))
                .InstancePerDependency();

            builder
                .RegisterType<XslTransformCacheFromFile>()
                .As<IXslTransformCache>()
                .SingleInstance();

            builder
                .RegisterType<XslTransformerFromFile>()
                .As<IXmlTransformer>()
                .Named<IXmlTransformer>(nameof(IDocumentsFormatTransformersFactory.GetFormatHtmlToXmlTransformer))
                .WithParameter(new TypedParameter(typeof(string), this.Configuration[ConfigurationConstants.FormatHtmlToXmlXslFilePath]))
                .InstancePerDependency();
            builder
                .RegisterType<XslTransformerFromFile>()
                .As<IXmlTransformer>()
                .Named<IXmlTransformer>(nameof(IDocumentsFormatTransformersFactory.GetFormatXmlToHtmlTransformer))
                .WithParameter(new TypedParameter(typeof(string), this.Configuration[ConfigurationConstants.FormatXmlToHtmlXslFilePath]))
                .InstancePerDependency();

            builder
                .RegisterType<XslTransformerFromFile>()
                .As<IXmlTransformer>()
                .Named<IXmlTransformer>(nameof(IFormatTransformerFactory.GetFormatToNlmTransformer))
                .WithParameter(new TypedParameter(typeof(string), this.Configuration[ConfigurationConstants.FormatSystemToNlmXslFilePath]))
                .InstancePerDependency();
            builder
                .RegisterType<XslTransformerFromFile>()
                .As<IXmlTransformer>()
                .Named<IXmlTransformer>(nameof(IFormatTransformerFactory.GetFormatToSystemTransformer))
                .WithParameter(new TypedParameter(typeof(string), this.Configuration[ConfigurationConstants.FormatNlmToSystemXslFilePath]))
                .InstancePerDependency();
            builder
                .RegisterType<XslTransformerFromFile>()
                .As<IXmlTransformer>()
                .Named<IXmlTransformer>(nameof(IFormatTransformerFactory.GetNlmInitialFormatTransformer))
                .WithParameter(new TypedParameter(typeof(string), this.Configuration[ConfigurationConstants.NlmInitialFormatXslFilePath]))
                .InstancePerDependency();
            builder
                .RegisterType<XslTransformerFromFile>()
                .As<IXmlTransformer>()
                .Named<IXmlTransformer>(nameof(IFormatTransformerFactory.GetSystemInitialFormatTransformer))
                .WithParameter(new TypedParameter(typeof(string), this.Configuration[ConfigurationConstants.SystemInitialFormatXslFilePath]))
                .InstancePerDependency();

            builder.RegisterType<XslTransformerFromContent>().As<IXslTransformerFromContent>();

            builder.Register<Func<string, IXslTransformerFromContent>>(
                ctx =>
                {
                    var context = ctx.Resolve<IComponentContext>();
                    return (s) => context.Resolve<IXslTransformerFromContent>(
                        new ResolvedParameter((p, c) => p.ParameterType == typeof(string), (p, c) => s),
                        new ResolvedParameter((p, c) => p.ParameterType == typeof(IXslTransformCache), (p, c) => c.ResolveNamed<IXslTransformCache>(nameof(XslTransformCacheFromContent))));
                })
                .As<Func<string, IXslTransformerFromContent>>()
                .InstancePerDependency();

            builder.RegisterType<XslTransformerFromFile>().As<IXslTransformerFromFile>();

            builder.Register<Func<string, IXslTransformerFromFile>>(
                ctx =>
                {
                    var context = ctx.Resolve<IComponentContext>();
                    return (s) => context.Resolve<IXslTransformerFromFile>(
                        new ResolvedParameter((p, c) => p.ParameterType == typeof(string), (p, c) => s),
                        new ResolvedParameter((p, c) => p.ParameterType == typeof(IXslTransformCache), (p, c) => c.ResolveNamed<IXslTransformCache>(nameof(XslTransformCacheFromFile))));
                })
                .As<Func<string, IXslTransformerFromFile>>()
                .InstancePerDependency();
        }
    }
}
