﻿// <copyright file="TestCommand.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Commands.Models;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Special;

    /// <summary>
    /// Test command.
    /// </summary>
    [System.ComponentModel.Description("Test.")]
    public class TestCommand : ITestCommand
    {
        private readonly ITestFeaturesProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCommand"/> class.
        /// </summary>
        /// <param name="provider">Instance of <see cref="ITestFeaturesProvider"/>.</param>
        public TestCommand(ITestFeaturesProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
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

            this.provider.RenumerateFootNotes(document);
            return Task.FromResult<object>(true);
        }
    }
}
