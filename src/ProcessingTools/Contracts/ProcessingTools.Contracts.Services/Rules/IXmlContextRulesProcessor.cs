// <copyright file="IXmlContextRulesProcessor.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Xml;
using ProcessingTools.Contracts.Models.Rules;

namespace ProcessingTools.Contracts.Services.Rules
{
    /// <summary>
    /// Rules processor with <see cref="XmlNode"/> context.
    /// </summary>
    public interface IXmlContextRulesProcessor : IRulesProcessor<XmlNode, IXmlReplaceRuleSetModel>
    {
    }
}
