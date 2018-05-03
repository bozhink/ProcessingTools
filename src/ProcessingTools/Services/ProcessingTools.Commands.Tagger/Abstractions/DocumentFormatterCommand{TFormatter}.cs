// <copyright file="DocumentFormatterCommand{TFormatter}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Abstractions
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts;

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
