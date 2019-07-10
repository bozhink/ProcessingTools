// <copyright file="DocumentFormatterCommand{TFormatter}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Models;
using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services;

namespace ProcessingTools.Commands.Tagger.Abstractions
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document formatter command.
    /// </summary>
    /// <typeparam name="TFormatter">Type of formatter.</typeparam>
    public class DocumentFormatterCommand<TFormatter> : ITaggerCommand
        where TFormatter : class, IDocumentFormatter
    {
        private readonly TFormatter formatter;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentFormatterCommand{TFormatter}"/> class.
        /// </summary>
        /// <param name="formatter">Formatter to be invoked.</param>
        public DocumentFormatterCommand(TFormatter formatter)
        {
            this.formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        /// <inheritdoc/>
        public Task<object> RunAsync(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return this.formatter.FormatAsync(document);
        }
    }
}
