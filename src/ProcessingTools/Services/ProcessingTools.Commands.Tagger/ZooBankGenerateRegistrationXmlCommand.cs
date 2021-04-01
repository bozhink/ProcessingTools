﻿// <copyright file="ZooBankGenerateRegistrationXmlCommand.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Commands.Models;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Bio.ZooBank;

    /// <summary>
    /// ZooBank generate registration XML command.
    /// </summary>
    [System.ComponentModel.Description("Generate XML document for registration in ZooBank.")]
    public class ZooBankGenerateRegistrationXmlCommand : IZooBankGenerateRegistrationXmlCommand
    {
        private readonly IZooBankRegistrationXmlGenerator generator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZooBankGenerateRegistrationXmlCommand"/> class.
        /// </summary>
        /// <param name="generator">Instance of <see cref="IZooBankRegistrationXmlGenerator"/>.</param>
        public ZooBankGenerateRegistrationXmlCommand(IZooBankRegistrationXmlGenerator generator)
        {
            this.generator = generator ?? throw new ArgumentNullException(nameof(generator));
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

            return this.generator.GenerateAsync(document);
        }
    }
}
