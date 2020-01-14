// <copyright file="ZooBankCloneXmlCommand.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Commands.Models;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Bio.ZooBank;
    using ProcessingTools.Contracts.Services.IO;

    /// <summary>
    /// ZooBank clone XML command.
    /// </summary>
    [System.ComponentModel.Description("Clone ZooBank XML.")]
    public class ZooBankCloneXmlCommand : IZooBankCloneXmlCommand
    {
        private readonly IZooBankXmlCloner cloner;
        private readonly IDocumentFactory documentFactory;
        private readonly IXmlReadService reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZooBankCloneXmlCommand"/> class.
        /// </summary>
        /// <param name="documentFactory">Instance of <see cref="IDocumentFactory"/>.</param>
        /// <param name="cloner">Instance of <see cref="IZooBankXmlCloner"/>.</param>
        /// <param name="reader">Instance of <see cref="IXmlReadService"/>.</param>
        public ZooBankCloneXmlCommand(IDocumentFactory documentFactory, IZooBankXmlCloner cloner, IXmlReadService reader)
        {
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.cloner = cloner ?? throw new ArgumentNullException(nameof(cloner));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
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

            return this.RunInternalAsync(document, settings);
        }

        private async Task<object> RunInternalAsync(IDocument document, ICommandSettings settings)
        {
            int numberOfFileNames = settings.FileNames.Count;
            if (numberOfFileNames < 2)
            {
                throw new InvalidOperationException("Output file name should be set.");
            }

            if (numberOfFileNames < 3)
            {
                throw new InvalidOperationException("The file path to xml-file-to-clone should be set.");
            }

            string sourceFileName = settings.FileNames[2];
            var sourceDocument = await this.ReadSourceDocument(sourceFileName).ConfigureAwait(false);

            return await this.cloner.CloneAsync(document, sourceDocument).ConfigureAwait(false);
        }

        private async Task<IDocument> ReadSourceDocument(string sourceFileName)
        {
            var xml = await this.reader.ReadFileToXmlDocumentAsync(sourceFileName).ConfigureAwait(false);
            var document = this.documentFactory.Create(xml.OuterXml);
            return document;
        }
    }
}
