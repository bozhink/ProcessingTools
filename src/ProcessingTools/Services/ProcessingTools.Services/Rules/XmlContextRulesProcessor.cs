// <copyright file="XmlContextRulesProcessor.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Models.Rules;
    using ProcessingTools.Contracts.Services.Rules;

    /// <summary>
    /// Rules processor with <see cref="XmlNode"/> context.
    /// </summary>
    public class XmlContextRulesProcessor : IXmlContextRulesProcessor
    {
        /// <inheritdoc/>
        public Task<object> ProcessAsync(XmlNode context, IEnumerable<IXmlReplaceRuleSetModel> ruleSets)
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
                    foreach (XmlNode node in context.SelectNodes(ruleSet.XPath))
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
