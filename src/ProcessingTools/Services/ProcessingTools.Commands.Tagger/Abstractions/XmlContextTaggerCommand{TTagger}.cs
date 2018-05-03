// <copyright file="XmlContextTaggerCommand{TTagger}.cs" company="ProcessingTools">
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
    /// XML context tagger command.
    /// </summary>
    /// <typeparam name="TTagger">Type of tagger.</typeparam>
    public class XmlContextTaggerCommand<TTagger> : ITaggerCommand
        where TTagger : class, IXmlContextTagger
    {
        private readonly TTagger tagger;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlContextTaggerCommand{TTagger}"/> class.
        /// </summary>
        /// <param name="tagger">Tagger to be invoked.</param>
        public XmlContextTaggerCommand(TTagger tagger)
        {
            this.tagger = tagger ?? throw new ArgumentNullException(nameof(tagger));
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

            return this.tagger.TagAsync(document.XmlDocument.DocumentElement);
        }
    }
}
