// <copyright file="XmlTransformersAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Autofac;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Xml;
    using System;
    using System.Collections.Generic;
    using Autofac;
    using Autofac.Core;
    using Microsoft.Extensions.Configuration;
    using ProcessingTools.Constants;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Data.Contracts.History;
    using ProcessingTools.Data.Documents.Mongo;
    using ProcessingTools.Data.History.Mongo;

    /// <summary>
    /// Autofac bindings for the Xml transformers.
    /// </summary>
    public class XmlTransformersAutofacModule : Module
    {
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
