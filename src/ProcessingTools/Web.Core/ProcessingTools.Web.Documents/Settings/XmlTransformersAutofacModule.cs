// <copyright file="XmlTransformersAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Autofac;
    using Microsoft.Extensions.Configuration;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Xml;
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
                .RegisterType<XslTransformCache>()
                .As<IXslTransformCache>()
                .SingleInstance();

            builder
                .RegisterType<XslTransformer>()
                .As<IXmlTransformer>()
                .Named<IXmlTransformer>(nameof(IDocumentsFormatTransformersFactory.GetFormatHtmlToXmlTransformer))
                .WithParameter(new TypedParameter(typeof(string), this.Configuration[ConfigurationConstants.FormatHtmlToXmlXslFilePath]))
                .InstancePerDependency();
            builder
                .RegisterType<XslTransformer>()
                .As<IXmlTransformer>()
                .Named<IXmlTransformer>(nameof(IDocumentsFormatTransformersFactory.GetFormatXmlToHtmlTransformer))
                .WithParameter(new TypedParameter(typeof(string), this.Configuration[ConfigurationConstants.FormatXmlToHtmlXslFilePath]))
                .InstancePerDependency();
        }
    }
}
