﻿// <copyright file="MethodType.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Enumerations
{
    /// <summary>
    /// Method type
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
        AsyncFunction = 2
    }
}
