// <copyright file="TransformersFactoriesAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Autofac
{
    using System;
    using global::Autofac;
    using ProcessingTools.Contracts.Services.Documents;
    using ProcessingTools.Contracts.Services.Layout;
    using ProcessingTools.Contracts.Services.Xml;
    using ProcessingTools.Services.Xml;

    /// <summary>
    /// Transformers factories module.
    /// </summary>
    public class TransformersFactoriesAutofacModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<XslTransformerFactory>()
                .As<IXslTransformerFactory>()
                .As<IXmlTransformerFactory>()
                .PropertiesAutowired()
                .InstancePerDependency();

            builder
                .RegisterType<TransformersFactory>()
                .As<IDocumentsFormatTransformersFactory>()
                .As<IFormatTransformerFactory>();
        }

        // TODO: No interceptors due to Castle.DynamicProxy version collision. Needs implementation.
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
