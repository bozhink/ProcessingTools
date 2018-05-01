// <copyright file="IXmlContextRulesProcessor.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Rules
{
    using System.Xml;
    using ProcessingTools.Models.Contracts.Rules;

    /// <summary>
    /// Rules processor with <see cref="XmlNode"/> context.
    /// </summary>
    public interface IXmlContextRulesProcessor : IRulesProcessor<XmlNode, IXmlReplaceRuleSetModel>
    {
    }
}
