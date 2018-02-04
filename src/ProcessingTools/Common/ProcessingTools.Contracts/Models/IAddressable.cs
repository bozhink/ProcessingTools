// <copyright file="IAddressable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    /// <summary>
    /// Model with address string.
    /// </summary>
    public interface IAddressable
    {
        /// <summary>
        /// Gets the address string.
        /// </summary>
        string AddressString { get; }
    }
}
