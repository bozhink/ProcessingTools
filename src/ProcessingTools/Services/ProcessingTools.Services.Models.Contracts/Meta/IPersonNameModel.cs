﻿// <copyright file="IPersonNameModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Meta
{
    /// <summary>
    /// Person Name.
    /// </summary>
    public interface IPersonNameModel
    {
        /// <summary>
        /// Gets given names.
        /// </summary>
        string GivenNames { get; }

        /// <summary>
        /// Gets prefix.
        /// </summary>
        string Prefix { get; }

        /// <summary>
        /// Gets suffix.
        /// </summary>
        string Suffix { get; }

        /// <summary>
        /// Gets surname.
        /// </summary>
        string Surname { get; }
    }
}
