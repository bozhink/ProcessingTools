// <copyright file="ParseHigherTaxaCommand{TService}.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;
    using ProcessingTools.Common.Code.Extensions;
    using ProcessingTools.Contracts.Commands.Models;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Parse higher taxa command.
    /// </summary>
    /// <typeparam name="TService">Type of service.</typeparam>
    public class ParseHigherTaxaCommand<TService> : ITaggerCommand
        where TService : ITaxonRankResolver
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
        public Task<object> RunAsync(IDocument document, ICommandSettings settings)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return this.RunInternalAsync(document);
        }

        private async Task<object> RunInternalAsync(IDocument document)
        {
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
                    this.reporter.AppendContent($"\t{taxonName}");
                }

                await this.reporter.MakeReportAsync().ConfigureAwait(false);
            }
        }
    }
}
