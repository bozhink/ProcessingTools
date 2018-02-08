// <copyright file="ValidationServiceModel{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Validation
{
    using System;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Validation;

    /// <summary>
    /// Generic validation service model.
    /// </summary>
    /// <typeparam name="T">Type of the validated object.</typeparam>
    public class ValidationServiceModel<T> : IValidationModel<T>
    {
        /// <inheritdoc/>
        public T ValidatedObject { get; set; }

        /// <inheritdoc/>
        public Exception ValidationException { get; set; }

        /// <inheritdoc/>
        public ValidationStatus ValidationStatus { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format("{0} / {1}", this.ValidatedObject, this.ValidationStatus);
        }
    }
}
