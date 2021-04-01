// <copyright file="ProcessorsAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Autofac
{
    using global::Autofac;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Documents;
    using ProcessingTools.Contracts.Services.Imaging;
    using ProcessingTools.Contracts.Services.Layout;
    using ProcessingTools.Contracts.Services.References;
    using ProcessingTools.Contracts.Services.Rules;
    using ProcessingTools.Services;
    using ProcessingTools.Services.Documents;
    using ProcessingTools.Services.Imaging;
    using ProcessingTools.Services.Layout;
    using ProcessingTools.Services.References;
    using ProcessingTools.Services.Rules;

    /// <summary>
    /// Autofac bindings for ProcessingTools.Services.*.
    /// </summary>
    public class ProcessorsAutofacModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            if (builder is null)
            {
                throw new System.ArgumentNullException(nameof(builder));
            }

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
        }
    }
}
