// <copyright file="IXmlContextParser{TRuleSet,TResult}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.Xml;

    /// <summary>
    /// Context parser for <see cref="XmlNode"/> context.
    /// </summary>
    /// <typeparam name="TRuleSet">Type of rule sets to apply.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    public interface IXmlContextParser<in TRuleSet, TResult> : IContextParser<XmlNode, TRuleSet, TResult>
    {
    }
}
