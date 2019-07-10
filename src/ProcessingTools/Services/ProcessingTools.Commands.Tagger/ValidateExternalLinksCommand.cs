// <copyright file="ValidateExternalLinksCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services;
using ProcessingTools.Contracts.Services.Validation;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts;

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
