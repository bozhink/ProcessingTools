// <copyright file="IDocumentRulesProcessor.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models;
using ProcessingTools.Contracts.Models.Rules;

namespace ProcessingTools.Contracts.Services.Rules
{
    /// <summary>
    /// Rules processor with <see cref="IDocument"/> context.
    /// </summary>
    public interface IDocumentRulesProcessor : IRulesProcessor<IDocument, IXmlReplaceRuleSetModel>
    {
    }
}
