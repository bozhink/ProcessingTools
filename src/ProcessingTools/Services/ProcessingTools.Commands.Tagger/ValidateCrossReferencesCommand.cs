// <copyright file="ValidateCrossReferencesCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Validation;

    /// <summary>
    /// Validate cross references command.
    /// </summary>
    [System.ComponentModel.Description("Validate cross-references.")]
    public class ValidateCrossReferencesCommand : DocumentValidatorCommand<ICrossReferencesValidator>, IValidateCrossReferencesCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateCrossReferencesCommand"/> class.
        /// </summary>
        /// <param name="validator">Instance of <see cref="ICrossReferencesValidator"/>.</param>
        /// <param name="reporter">Instance of <see cref="IReporter"/>.</param>
        public ValidateCrossReferencesCommand(ICrossReferencesValidator validator, IReporter reporter)
            : base(validator, reporter)
        {
        }
    }
}
