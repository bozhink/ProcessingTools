// <copyright file="ValidationStatus.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Enumerations
{
    /// <summary>
    /// Represents validation status code returned by a ValidationService.
    /// </summary>
    public enum ValidationStatus
    {
        /// <summary>
        /// Returned result is valid.
        /// </summary>
        Valid = 1,

        /// <summary>
        /// Returned result is not valid.
        /// </summary>
        Invalid = 2,

        /// <summary>
        /// Can not determine if the returned result is valid or invalid.
        /// </summary>
        Undefined = 0
    }
}
