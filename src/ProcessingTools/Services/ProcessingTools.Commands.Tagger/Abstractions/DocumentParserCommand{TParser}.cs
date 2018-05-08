// <copyright file="DocumentParserCommand{TParser}.cs" company="ProcessingTools">
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
    /// Document parser command.
    /// </summary>
    /// <typeparam name="TParser">Type of parser.</typeparam>
    public class DocumentParserCommand<TParser> : ITaggerCommand
        where TParser : class, IDocumentParser
    {
        private readonly TParser parser;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentParserCommand{TParser}"/> class.
        /// </summary>
        /// <param name="parser">Parser to be invoked.</param>
        public DocumentParserCommand(TParser parser)
        {
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
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

            return this.parser.ParseAsync(document);
        }
    }
}
