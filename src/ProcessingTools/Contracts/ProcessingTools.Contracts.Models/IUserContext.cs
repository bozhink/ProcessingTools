﻿// <copyright file="IUserContext.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// User context.
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// Gets the user ID.
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// Gets the user name.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets the user e-mail.
        /// </summary>
        string UserEmail { get; }
    }
}