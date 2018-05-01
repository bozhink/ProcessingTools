// <copyright file="ProcessorsAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Autofac;
    using ProcessingTools.Processors.Contracts.Rules;
    using ProcessingTools.Processors.Rules;

    /// <summary>
    /// Autofac bindings for ProcessingTools.Processors.*
    /// </summary>
    public class ProcessorsAutofacModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DocumentRulesProcessor>().As<IDocumentRulesProcessor>().InstancePerDependency();
            builder.RegisterType<XmlContextRulesProcessor>().As<IXmlContextRulesProcessor>().InstancePerDependency();
        }
    }
}
