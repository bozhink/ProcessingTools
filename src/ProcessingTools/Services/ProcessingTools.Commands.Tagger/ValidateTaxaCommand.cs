﻿// <copyright file="ValidateTaxaCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Contracts.Validation;

    /// <summary>
    /// Validate taxa command.
    /// </summary>
    [System.ComponentModel.Description("Taxa validation using Global Names Resolver.")]
    public class ValidateTaxaCommand : DocumentValidatorCommand<ITaxaValidator>, IValidateTaxaCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateTaxaCommand"/> class.
        /// </summary>
        /// <param name="validator">Instance of <see cref="ITaxaValidator"/>.</param>
        /// <param name="reporter">Instance of <see cref="IReporter"/>.</param>
        public ValidateTaxaCommand(ITaxaValidator validator, IReporter reporter)
            : base(validator, reporter)
        {
        }
    }
}
