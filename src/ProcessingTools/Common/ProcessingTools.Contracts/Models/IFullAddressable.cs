// <copyright file="IFullAddressable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
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
