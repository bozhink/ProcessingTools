// <copyright file="ReferencesParser.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.References;
using ProcessingTools.Contracts.Services.Rules;

namespace ProcessingTools.Services.References
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Models.Rules;

    /// <summary>
    /// References parser.
    /// </summary>
    public class ReferencesParser : IReferencesParser
    {
        private readonly IXmlReplaceRuleSetParser ruleSetParser;
        private readonly IXmlContextRulesProcessor rulesProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencesParser"/> class.
        /// </summary>
        /// <param name="ruleSetParser">Rule set parser.</param>
        /// <param name="rulesProcessor">Rules processor.</param>
        public ReferencesParser(IXmlReplaceRuleSetParser ruleSetParser, IXmlContextRulesProcessor rulesProcessor)
        {
            this.ruleSetParser = ruleSetParser ?? throw new ArgumentNullException(nameof(ruleSetParser));
            this.rulesProcessor = rulesProcessor ?? throw new ArgumentNullException(nameof(rulesProcessor));
        }

        /// <inheritdoc/>
        public async Task<object> ParseAsync(XmlNode context, IEnumerable<IReferenceParseStyleModel> styles)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (styles == null || !styles.Any())
            {
                return false;
            }

            var scripts = styles.Select(s => s.Script);

            var ruleSets = new List<IXmlReplaceRuleSetModel>();
            foreach (var script in scripts)
            {
                var scriptRuleSets = await this.ruleSetParser.ParseStringToRuleSetsAsync(script).ConfigureAwait(false);
                if (scriptRuleSets != null && scriptRuleSets.Any())
                {
                    ruleSets.AddRange(scriptRuleSets);
                }
            }

            return await this.rulesProcessor.ProcessAsync(context, ruleSets).ConfigureAwait(false);
        }
    }
}
