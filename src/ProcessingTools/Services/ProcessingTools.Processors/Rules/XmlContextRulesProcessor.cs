// <copyright file="XmlContextRulesProcessor.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Models.Contracts.Rules;
    using ProcessingTools.Processors.Contracts.Rules;

    /// <summary>
    /// Rules processor with <see cref="XmlNode"/> context.
    /// </summary>
    public class XmlContextRulesProcessor : IXmlContextRulesProcessor
    {
        /// <inheritdoc/>
        public async Task<object> ProcessAsync(XmlNode context, IEnumerable<IXmlReplaceRuleSetModel> ruleSets)
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
                    foreach (XmlNode node in context.SelectNodes(ruleSet.XPath))
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
