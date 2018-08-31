// <copyright file="IAddressable.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents
{
    using System.Collections.Generic;

    /// <summary>
    /// Model with addresses.
    /// </summary>
    public interface IAddressable
    {
        /// <summary>
        /// Gets addresses.
        /// </summary>
        IEnumerable<IAddress> Addresses { get; }
    }
}
