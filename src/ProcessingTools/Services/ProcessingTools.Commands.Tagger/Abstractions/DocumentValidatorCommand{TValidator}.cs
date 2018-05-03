// <copyright file="DocumentValidatorCommand{TValidator}.cs" company="ProcessingTools">
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
    /// Document validator command.
    /// </summary>
    /// <typeparam name="TValidator">Type of validator.</typeparam>
    public class DocumentValidatorCommand<TValidator> : ITaggerCommand
        where TValidator : class, IDocumentValidator
    {
        private readonly IReporter reporter;
        private readonly TValidator validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentValidatorCommand{TValidator}"/> class.
        /// </summary>
        /// <param name="validator">Validator to be invoked.</param>
        /// <param name="reporter">Reporter.</param>
        public DocumentValidatorCommand(TValidator validator, IReporter reporter)
        {
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
        }

        /// <inheritdoc/>
        public async Task<object> RunAsync(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var result = await this.validator.ValidateAsync(document, this.reporter).ConfigureAwait(false);
            await this.reporter.MakeReportAsync().ConfigureAwait(false);

            return result;
        }
    }
}
