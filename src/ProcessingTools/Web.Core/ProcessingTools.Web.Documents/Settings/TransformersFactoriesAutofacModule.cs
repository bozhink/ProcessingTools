﻿// <copyright file="TransformersFactoriesAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using System;
    using Autofac;
    using Autofac.Extras.DynamicProxy;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Processors.Contracts.Layout;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Web.Documents.Interceptors;

    /// <summary>
    /// Transformers factories module.
    /// </summary>
    public class TransformersFactoriesAutofacModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<TransformersFactory>()
                .As<IDocumentsFormatTransformersFactory>()
                .As<IFormatTransformerFactory>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(FactoryInterceptor));
        }

        private class TransformersFactory : IDocumentsFormatTransformersFactory, IFormatTransformerFactory
        {
            public IXmlTransformer GetFormatHtmlToXmlTransformer() => throw new NotSupportedException();

            public IXmlTransformer GetFormatToNlmTransformer() => throw new NotSupportedException();

            public IXmlTransformer GetFormatToSystemTransformer() => throw new NotSupportedException();

            public IXmlTransformer GetFormatXmlToHtmlTransformer() => throw new NotSupportedException();

            public IXmlTransformer GetNlmInitialFormatTransformer() => throw new NotSupportedException();

            public IXmlTransformer GetSystemInitialFormatTransformer() => throw new NotSupportedException();
        }
    }
}
