// <copyright file="ReferencesParser.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.References
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Processors.Contracts.References;
    using ProcessingTools.Processors.Contracts.Rules;
    using ProcessingTools.Services.Contracts.Layout.Styles;

    /// <summary>
    /// References parser.
    /// </summary>
    public class ReferencesParser : IReferencesParser
    {
        private readonly IXmlContextRulesProcessor rulesProcessor;
        private readonly IReferenceParseStyleRuleSetsProvider ruleSetsProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencesParser"/> class.
        /// </summary>
        /// <param name="rulesProcessor">Rules processor.</param>
        /// <param name="ruleSetsProvider">Reference parse style rule sets provider.</param>
        public ReferencesParser(IXmlContextRulesProcessor rulesProcessor, IReferenceParseStyleRuleSetsProvider ruleSetsProvider)
        {
            this.rulesProcessor = rulesProcessor ?? throw new ArgumentNullException(nameof(rulesProcessor));
            this.ruleSetsProvider = ruleSetsProvider ?? throw new ArgumentNullException(nameof(ruleSetsProvider));
        }

        /// <inheritdoc/>
        public async Task<object> ParseAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return await this.rulesProcessor.ProcessAsync(context, this.ruleSetsProvider.RuleSets).ConfigureAwait(false);
        }
    }
}
