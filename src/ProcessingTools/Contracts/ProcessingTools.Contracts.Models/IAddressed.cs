// <copyright file="IAddressed.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with address string.
    /// </summary>
    public interface IAddressed
    {
        /// <summary>
        /// Gets the address string.
        /// </summary>
        string AddressString { get; }
    }
}
