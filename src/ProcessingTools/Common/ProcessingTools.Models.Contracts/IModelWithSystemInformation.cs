// <copyright file="IModelWithSystemInformation.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    using System;

    /// <summary>
    /// Model with system information.
    /// </summary>
    public interface IModelWithSystemInformation
    {
        /// <summary>
        /// Gets the name of user by which the model is created.
        /// </summary>
        string CreatedBy { get; }

        /// <summary>
        /// Gets date of creation.
        /// </summary>
        DateTime CreatedOn { get; }

        /// <summary>
        /// Gets the name of user by which the model is last modified.
        /// </summary>
        string ModifiedBy { get; }

        /// <summary>
        /// Gets date of last modification.
        /// </summary>
        DateTime ModifiedOn { get; }

        /// <summary>
        /// Gets timestamp.
        /// </summary>
        byte[] Timestamp { get; }
    }
}
