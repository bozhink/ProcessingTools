// <copyright file="QueryReplaceCommand.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Commands.Models;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;

    /// <summary>
    /// Query replace command.
    /// </summary>
    [System.ComponentModel.Description("Query replace.")]
    public class QueryReplaceCommand : IQueryReplaceCommand
    {
        private readonly IQueryReplacer queryReplacer;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryReplaceCommand"/> class.
        /// </summary>
        /// <param name="queryReplacer">Instance of <see cref="IQueryReplacer"/>.</param>
        public QueryReplaceCommand(IQueryReplacer queryReplacer)
        {
            this.queryReplacer = queryReplacer ?? throw new ArgumentNullException(nameof(queryReplacer));
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

            return this.RunInternalAsync(document, settings);
        }

        private async Task<object> RunInternalAsync(IDocument document, ICommandSettings settings)
        {
            int numberOfFileNames = settings.FileNames.Count;
            if (numberOfFileNames < 3)
            {
                throw new InvalidOperationException("The query file name should be set.");
            }

            string queryFileName = settings.FileNames[2];

            var processedContent = await this.queryReplacer.ReplaceAsync(document.Xml, queryFileName).ConfigureAwait(false);

            document.Xml = processedContent;

            return true;
        }
    }
}
