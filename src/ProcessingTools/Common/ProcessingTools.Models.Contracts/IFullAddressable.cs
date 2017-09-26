// <copyright file="IFullAddressable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    /// <summary>
    /// Model with full address.
    /// </summary>
    public interface IFullAddressable
    {
        /// <summary>
        /// Gets the full address.
        /// </summary>
        string FullAddress { get; }
    }
}
