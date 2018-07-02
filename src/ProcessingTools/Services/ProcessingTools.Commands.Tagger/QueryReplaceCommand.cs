// <copyright file="QueryReplaceCommand.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts;

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

            int numberOfFileNames = settings.FileNames.Count;
            if (numberOfFileNames < 3)
            {
                throw new InvalidOperationException("The query file name should be set.");
            }

            string queryFileName = settings.FileNames[2];

            var processedContent = await this.queryReplacer.ReplaceAsync(document.Xml, queryFileName);

            document.Xml = processedContent;

            return true;
        }
    }
}
