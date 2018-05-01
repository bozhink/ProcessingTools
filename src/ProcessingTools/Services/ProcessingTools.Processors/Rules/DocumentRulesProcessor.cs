// <copyright file="DocumentRulesProcessor.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.Rules;
    using ProcessingTools.Processors.Contracts.Rules;

    /// <summary>
    /// Rules processor with <see cref="IDocument"/> context.
    /// </summary>
    public class DocumentRulesProcessor : IDocumentRulesProcessor
    {
        /// <inheritdoc/>
        public async Task<object> ProcessAsync(IDocument context, IEnumerable<IXmlReplaceRuleSetModel> ruleSets)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (ruleSets == null || !ruleSets.Any())
            {
                return false;
            }

            return await Task.Run(() =>
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
            }).ConfigureAwait(false);
        }
    }
}
