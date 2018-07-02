// <copyright file="IPersonNameModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Models.Contracts.Meta
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
