// <copyright file="XmlContextParserCommand{TParser}.cs" company="ProcessingTools">
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
    /// XML context parser command.
    /// </summary>
    /// <typeparam name="TParser">Type of parser.</typeparam>
    public class XmlContextParserCommand<TParser> : ITaggerCommand
        where TParser : class, IXmlContextParser
    {
        private readonly TParser parser;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlContextParserCommand{TParser}"/> class.
        /// </summary>
        /// <param name="parser">Parser to be invoked.</param>
        public XmlContextParserCommand(TParser parser)
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

            return this.parser.ParseAsync(document.XmlDocument.DocumentElement);
        }
    }
}
