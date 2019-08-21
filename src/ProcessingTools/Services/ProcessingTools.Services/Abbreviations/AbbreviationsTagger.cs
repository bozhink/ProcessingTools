// <copyright file="AbbreviationsTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Abbreviations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Models.Abbreviations;
    using ProcessingTools.Contracts.Services.Abbreviations;
    using ProcessingTools.Contracts.Services.Xml;
    using ProcessingTools.Extensions;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Services.Models.Abbreviations;

    /// <summary>
    /// Abbreviations tagger.
    /// </summary>
    public class AbbreviationsTagger : IAbbreviationsTagger
    {
        private const string SelectNodesToTagAbbreviationsXPathTemplate = ".//node()[contains(string(.),string('{0}'))]";

        private readonly IAbbreviationsHarvester harvester;
        private readonly IXmlContextWrapper contextWrapper;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbbreviationsTagger"/> class.
        /// </summary>
        /// <param name="harvester">Abbreviations harvester.</param>
        /// <param name="contextWrapper">Context wrapper.</param>
        /// <param name="logger">Logger.</param>
        public AbbreviationsTagger(IAbbreviationsHarvester harvester, IXmlContextWrapper contextWrapper, ILogger<AbbreviationsTagger> logger)
        {
            this.harvester = harvester ?? throw new ArgumentNullException(nameof(harvester));
            this.contextWrapper = contextWrapper ?? throw new ArgumentNullException(nameof(contextWrapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<object> TagAsync(XmlNode context)
        {
            // Do not change this sequence
            await this.TagAbbreviationsInSubContextSelectedByXPathWithHarvestSubContextAsync(
                context,
                "//graphic | //media | //disp-formula-group")
                .ConfigureAwait(false);

            await this.TagAbbreviationsInSubContextSelectedByXPathWithHarvestSubContextAsync(
                context,
                "//chem-struct-wrap | //fig | //supplementary-material | //table-wrap")
                .ConfigureAwait(false);

            await this.TagAbbreviationsInSubContextSelectedByXPathWithHarvestSubContextAsync(
                context,
                "//fig-group | //table-wrap-group")
                .ConfigureAwait(false);

            await this.TagAbbreviationsInSubContextSelectedByXPathWithHarvestSubContextAsync(
                context,
                "//boxed-text")
                .ConfigureAwait(false);

            await this.TagAbbreviationsInSubContextSelectedByXPathWithHarvestContextAsync(
                context,
                "//alt-title | //article-title | //attrib | //award-id | //comment | //conf-theme | //def-head | //funding-source | //license-p | //meta-value | //p | //preformat | //product | //subtitle | //supplement | //td | //term | //term-head | //th | //title | //trans-subtitle | //trans-title | //verse-line")
                .ConfigureAwait(false);

            return true;
        }

        private Task TagAbbreviationsInSubContextSelectedByXPathWithHarvestSubContextAsync(XmlNode context, string selectContextToTagXPath)
        {
            if (context != null && !string.IsNullOrWhiteSpace(selectContextToTagXPath))
            {
                var tasks = context.SelectNodes(selectContextToTagXPath)
                    .Cast<XmlNode>()
                    .Select(this.TagAbbreviationsWithHarvestWholeContextAsync)
                    .ToArray();

                return Task.WhenAll(tasks);
            }

            return Task.CompletedTask;
        }

        private async Task TagAbbreviationsWithHarvestWholeContextAsync(XmlNode context)
        {
            if (context != null)
            {
                var abbreviationDefinitions = await this.GetAbbreviationsAsync(context).ConfigureAwait(false);
                this.TagAbbreviations(context, abbreviationDefinitions);
            }
        }

        private async Task TagAbbreviationsInSubContextSelectedByXPathWithHarvestContextAsync(XmlNode context, string selectContextToTagXPath)
        {
            if (context != null && !string.IsNullOrWhiteSpace(selectContextToTagXPath))
            {
                var abbreviationDefinitions = await this.GetAbbreviationsAsync(context).ConfigureAwait(false);

                context.SelectNodes(selectContextToTagXPath).Cast<XmlNode>().ForEach(n => this.TagAbbreviations(n, abbreviationDefinitions));
            }
        }

        /// <summary>
        /// Get the list of abbreviation definitions in a specified <see cref="XmlNode"/> context.
        /// </summary>
        /// <param name="context"><see cref="XmlNode"/> context to be harvested.</param>
        /// <returns>List of abbreviation definitions.</returns>
        private async Task<IList<IAbbreviation>> GetAbbreviationsAsync(XmlNode context)
        {
            if (context != null)
            {
                var abbreviations = await this.harvester.HarvestAsync(context).ConfigureAwait(false);
                if (abbreviations != null && abbreviations.Any())
                {
                    var result = abbreviations
                        .Where(a => !string.IsNullOrWhiteSpace(a.Value) && !string.IsNullOrWhiteSpace(a.Definition) && a.Value.Length > 1)
                        .Select(a => new Abbreviation(a.Value, a.ContentType, a.Definition))
                        .ToList<IAbbreviation>();

                    return result;
                }
            }

            return Array.Empty<IAbbreviation>();
        }

        /// <summary>
        /// Tag abbreviation definitions in a specified <see cref="XmlNode"/> context.
        /// </summary>
        /// <param name="context"><see cref="XmlNode"/> context to be updated.</param>
        /// <param name="abbreviations">List of abbreviation definitions to be tagged in the context.</param>
        private void TagAbbreviations(XmlNode context, IList<IAbbreviation> abbreviations)
        {
            if (context != null && abbreviations != null && abbreviations.Any())
            {
                var document = this.contextWrapper.Create(context);

                abbreviations.OrderByDescending(a => a.Content.Length).ForEach(abbreviation => this.TagAbbreviation(document, abbreviation));

                context.InnerXml = document.DocumentElement.InnerXml;
            }
        }

        /// <summary>
        /// Tag single abbreviation definition in a specified <see cref="XmlNode"/> context.
        /// </summary>
        /// <param name="context"><see cref="XmlNode"/> context to be updated.</param>
        /// <param name="abbreviation">Abbreviation definition to be tagged in the context.</param>
        private void TagAbbreviation(XmlNode context, IAbbreviation abbreviation)
        {
            if (context != null && abbreviation != null)
            {
                string xpath = string.Format(SelectNodesToTagAbbreviationsXPathTemplate, abbreviation.Content);
                foreach (XmlNode node in context.SelectNodes(xpath))
                {
                    bool canPerformReplace = node.CheckIfIsPossibleToPerformReplaceInXmlNode();
                    if (canPerformReplace)
                    {
                        try
                        {
                            node.ReplaceWholeXmlNodeByRegexPattern(abbreviation.SearchPattern, abbreviation.ReplacePattern);
                        }
                        catch (XmlException ex)
                        {
                            this.logger.LogError(ex, $"Exception in abbreviation {abbreviation.Content}");
                        }
                    }
                }
            }
        }
    }
}
