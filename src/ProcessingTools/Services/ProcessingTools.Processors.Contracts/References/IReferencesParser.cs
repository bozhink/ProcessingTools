// <copyright file="IReferencesParser.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.References
{
    using ProcessingTools.Models.Contracts.Rules;

    /// <summary>
    /// References parser.
    /// </summary>
    public interface IReferencesParser : IXmlContextParser<IXmlReplaceRuleSetModel, object>
    {
    }
}
