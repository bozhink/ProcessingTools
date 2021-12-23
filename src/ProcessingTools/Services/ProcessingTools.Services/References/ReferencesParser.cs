// <copyright file="ReferencesParser.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.References
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Models.Rules;
    using ProcessingTools.Contracts.Services.References;
    using ProcessingTools.Contracts.Services.Rules;

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
        public Task<object> ParseAsync(XmlNode context, IEnumerable<IReferenceParseStyleModel> styles)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (styles is null || !styles.Any())
            {
                return Task.FromResult<object>(false);
            }

            return this.ParseInternalAsync(context, styles);
        }

        private async Task<object> ParseInternalAsync(XmlNode context, IEnumerable<IReferenceParseStyleModel> styles)
        {
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
