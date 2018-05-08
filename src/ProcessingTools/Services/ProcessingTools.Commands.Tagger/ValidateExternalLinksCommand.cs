// <copyright file="ValidateExternalLinksCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Validation;

    /// <summary>
    /// Validate external links command.
    /// </summary>
    [System.ComponentModel.Description("Validate external links.")]
    public class ValidateExternalLinksCommand : DocumentValidatorCommand<IExternalLinksValidator>, IValidateExternalLinksCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateExternalLinksCommand"/> class.
        /// </summary>
        /// <param name="validator">Instance of <see cref="IExternalLinksValidator"/>.</param>
        /// <param name="reporter">Instance of <see cref="IReporter"/>.</param>
        public ValidateExternalLinksCommand(IExternalLinksValidator validator, IReporter reporter)
            : base(validator, reporter)
        {
        }
    }
}
