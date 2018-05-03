// <copyright file="ReferencesParser.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.References
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Models.Contracts.Rules;
    using ProcessingTools.Processors.Contracts.References;
    using ProcessingTools.Processors.Contracts.Rules;

    /// <summary>
    /// References parser.
    /// </summary>
    public class ReferencesParser : IReferencesParser
    {
        private readonly IXmlContextRulesProcessor rulesProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencesParser"/> class.
        /// </summary>
        /// <param name="rulesProcessor">Rules processor.</param>
        public ReferencesParser(IXmlContextRulesProcessor rulesProcessor)
        {
            this.rulesProcessor = rulesProcessor ?? throw new ArgumentNullException(nameof(rulesProcessor));
        }

        /// <inheritdoc/>
        public async Task<object> ParseAsync(XmlNode context, IEnumerable<IXmlReplaceRuleSetModel> ruleSets)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return await this.rulesProcessor.ProcessAsync(context, ruleSets).ConfigureAwait(false);
        }
    }
}
