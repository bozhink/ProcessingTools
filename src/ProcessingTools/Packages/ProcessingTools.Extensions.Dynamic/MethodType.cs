// <copyright file="MethodType.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    /// <summary>
    /// Method type.
    /// </summary>
    public enum MethodType
    {
        /// <summary>
        /// Synchronous
        /// </summary>
        Synchronous = 0,

        /// <summary>
        /// Asynchronous action
        /// </summary>
        AsyncAction = 1,

        /// <summary>
        /// Asynchronous function
        /// </summary>
        AsyncFunction = 2,
    }
}
