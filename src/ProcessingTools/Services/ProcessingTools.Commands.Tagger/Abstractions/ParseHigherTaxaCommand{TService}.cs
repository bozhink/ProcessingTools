// <copyright file="ParseHigherTaxaCommand{TService}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Parse higher taxa command.
    /// </summary>
    /// <typeparam name="TService">Type of service.</typeparam>
    public class ParseHigherTaxaCommand<TService> : ITaggerCommand
        where TService : ITaxaRankResolver
    {
        private readonly IHigherTaxaParserWithDataService<TService, ITaxonRank> parser;
        private readonly IReporter reporter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseHigherTaxaCommand{TService}"/> class.
        /// </summary>
        /// <param name="parser">Parser to be invoked.</param>
        /// <param name="reporter">Reporter.</param>
        public ParseHigherTaxaCommand(IHigherTaxaParserWithDataService<TService, ITaxonRank> parser, IReporter reporter)
        {
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
            this.reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
        }

        /// <inheritdoc/>
        public async Task<object> RunAsync(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var result = await this.parser.ParseAsync(document.XmlDocument.DocumentElement).ConfigureAwait(false);

            await this.PrintNonParsedTaxa(document.XmlDocument).ConfigureAwait(false);

            return result;
        }

        private async Task PrintNonParsedTaxa(XmlNode node)
        {
            var uniqueHigherTaxaList = node.ExtractUniqueNonParsedHigherTaxa()
                .Distinct()
                .OrderBy(s => s)
                .ToArray();

            if (uniqueHigherTaxaList.Length > 0)
            {
                this.reporter.AppendContent("Non-parsed taxa:");
                foreach (var taxonName in uniqueHigherTaxaList)
                {
                    this.reporter.AppendContent(string.Format("\t{0}", taxonName));
                }

                await this.reporter.MakeReportAsync().ConfigureAwait(false);
            }
        }
    }
}
