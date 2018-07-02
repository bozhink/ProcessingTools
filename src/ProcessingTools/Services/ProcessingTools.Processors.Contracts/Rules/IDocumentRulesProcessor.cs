// <copyright file="IDocumentRulesProcessor.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Rules
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.Rules;

    /// <summary>
    /// Rules processor with <see cref="IDocument"/> context.
    /// </summary>
    public interface IDocumentRulesProcessor : IRulesProcessor<IDocument, IXmlReplaceRuleSetModel>
    {
    }
}
