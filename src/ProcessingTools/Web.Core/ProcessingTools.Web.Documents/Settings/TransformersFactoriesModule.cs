// <copyright file="TransformersFactoriesModule.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using System;
    using Autofac;
    using Autofac.Extras.DynamicProxy;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Web.Documents.Interceptors;

    /// <summary>
    /// Transformers factories module.
    /// </summary>
    public class TransformersFactoriesModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<TransformersFactory>()
                .As<IDocumentsFormatTransformersFactory>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(FactoryInterceptor));
        }

        private class TransformersFactory : IDocumentsFormatTransformersFactory
        {
            public IXmlTransformer GetFormatHtmlToXmlTransformer() => throw new NotSupportedException();

            public IXmlTransformer GetFormatXmlToHtmlTransformer() => throw new NotSupportedException();
        }
    }
}
