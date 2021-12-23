﻿// <copyright file="IValidationModel{T}.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Validation
{
    using System;
    using ProcessingTools.Common.Enumerations;

    /// <summary>
    /// Generic validation model.
    /// </summary>
    /// <typeparam name="T">Type of the validated object.</typeparam>
    public interface IValidationModel<T>
    {
        /// <summary>
        /// Gets or sets the validated object.
        /// </summary>
        T ValidatedObject { get; set; }

        /// <summary>
        /// Gets or sets the validation status.
        /// </summary>
        ValidationStatus ValidationStatus { get; set; }

        /// <summary>
        /// Gets or sets the validation exception (if any).
        /// </summary>
        Exception ValidationException { get; set; }
    }
}
