// <copyright file="DocumentRulesProcessor.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Rules;
    using ProcessingTools.Contracts.Services.Rules;

    /// <summary>
    /// Rules processor with <see cref="IDocument"/> context.
    /// </summary>
    public class DocumentRulesProcessor : IDocumentRulesProcessor
    {
        /// <inheritdoc/>
        public Task<object> ProcessAsync(IDocument context, IEnumerable<IXmlReplaceRuleSetModel> ruleSets)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (ruleSets is null || !ruleSets.Any())
            {
                return Task.FromResult<object>(false);
            }

            return Task.Run<object>(() =>
            {
                foreach (var ruleSet in ruleSets)
                {
                    var nodes = context.SelectNodes(ruleSet.XPath);
                    foreach (var node in nodes)
                    {
                        foreach (var rule in ruleSet.Rules)
                        {
                            node.InnerXml = Regex.Replace(node.InnerXml, rule.Pattern, rule.Replacement);
                        }
                    }
                }

                return true;
            });
        }
    }
}
