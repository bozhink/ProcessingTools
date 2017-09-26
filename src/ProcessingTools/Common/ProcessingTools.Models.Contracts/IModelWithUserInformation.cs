// <copyright file="IModelWithUserInformation.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    // TODO: DELETE THIS INTERFACE
    using System;

    /// <summary>
    /// IModelWithUserInformation
    /// </summary>
    public interface IModelWithUserInformation
    {
        /// <summary>
        /// Gets DateCreated
        /// </summary>
        DateTime DateCreated { get; }

        /// <summary>
        /// Gets DateModified
        /// </summary>
        DateTime DateModified { get; }

        /// <summary>
        /// Gets CreatedByUser
        /// </summary>
        string CreatedByUser { get; }

        /// <summary>
        /// Gets ModifiedByUser
        /// </summary>
        string ModifiedByUser { get; }
    }
}
