// <copyright file="IEnvironmentUser.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Environment user.
    /// </summary>
    public interface IEnvironmentUser : IStringIdentifiable
    {
        /// <summary>
        /// Gets the user name.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets the user's e-mail.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Gets the user's role.
        /// </summary>
        UserRole Role { get; }
    }
}
