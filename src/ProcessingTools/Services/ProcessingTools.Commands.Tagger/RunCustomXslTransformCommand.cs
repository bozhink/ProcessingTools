﻿// <copyright file="RunCustomXslTransformCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
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
    /// Run custom XSL transform command.
    /// </summary>
    [System.ComponentModel.Description("Custom XSL transform.")]
    public class RunCustomXslTransformCommand : IRunCustomXslTransformCommand
    {
        private readonly IDocumentXslProcessor processor;

        /// <summary>
        /// Initializes a new instance of the <see cref="RunCustomXslTransformCommand"/> class.
        /// </summary>
        /// <param name="processor">Instance of <see cref="IDocumentXslProcessor"/>.</param>
        public RunCustomXslTransformCommand(IDocumentXslProcessor processor)
        {
            this.processor = processor ?? throw new ArgumentNullException(nameof(processor));
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
            if (numberOfFileNames < 3)
            {
                throw new InvalidOperationException("The name of the XSLT file should be set.");
            }

            this.processor.XslFileName = settings.FileNames[2];

            await this.processor.ProcessAsync(document);

            return true;
        }
    }
}