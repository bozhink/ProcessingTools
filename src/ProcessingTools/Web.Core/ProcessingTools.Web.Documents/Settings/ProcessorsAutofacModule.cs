// <copyright file="ProcessorsAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Autofac;
    using ProcessingTools.Common;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Layout;
    using ProcessingTools.Processors.Contracts.References;
    using ProcessingTools.Processors.Contracts.Rules;
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
            builder.RegisterType<NormalizationTransformerFactory>().As<INormalizationTransformerFactory>().InstancePerLifetimeScope();

            builder.RegisterType<DocumentRulesProcessor>().As<IDocumentRulesProcessor>().InstancePerDependency();
            builder.RegisterType<XmlContextRulesProcessor>().As<IXmlContextRulesProcessor>().InstancePerDependency();
            builder.RegisterType<ReferencesParser>().As<IReferencesParser>().InstancePerDependency();
        }
    }
}
