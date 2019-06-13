// <copyright file="IAddressable.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Services.Data.Journals
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Addressable.
    /// </summary>
    public interface IAddressable
    {
        /// <summary>
        /// Gets addresses.
        /// </summary>
        ICollection<IAddress> Addresses { get; }
    }
}
