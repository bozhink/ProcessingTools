// <copyright file="IXmlContextRulesProcessor.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Rules
{
    using System.Xml;
    using ProcessingTools.Contracts.Models.Rules;

    /// <summary>
    /// Rules processor with <see cref="XmlNode"/> context.
    /// </summary>
    public interface IXmlContextRulesProcessor : IRulesProcessor<XmlNode, IXmlReplaceRuleSetModel>
    {
    }
}
