﻿// <copyright file="IValidationService{T}.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Validation
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Validation;

    /// <summary>
    /// Generic validation service.
    /// </summary>
    /// <typeparam name="T">Type of the validated object.</typeparam>
    public interface IValidationService<T>
    {
        /// <summary>
        /// Validates objects.
        /// </summary>
        /// <param name="items">Objects to be validated.</param>
        /// <returns>Task of validation models.</returns>
        Task<IValidationModel<T>[]> ValidateAsync(params T[] items);
    }
}
