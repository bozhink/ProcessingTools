// <copyright file="IPerson.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    /// <summary>
    /// Person.
    /// </summary>
    public interface IPerson
    {
        /// <summary>
        /// Gets given names.
        /// </summary>
        string GivenNames { get; }

        /// <summary>
        /// Gets name prefix.
        /// </summary>
        string Prefix { get; }

        /// <summary>
        /// Gets name suffix.
        /// </summary>
        string Suffix { get; }

        /// <summary>
        /// Gets surname.
        /// </summary>
        string Surname { get; }
    }
}
