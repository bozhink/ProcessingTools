// <copyright file="TestCommand.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Special;
    using System;
    using System.Threading.Tasks;

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
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            this.provider.RenumerateFootNotes(document);
            return Task.FromResult<object>(true);
        }
    }
}
