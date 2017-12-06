// <copyright file="IAddressable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Models.Data.Journals
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Addressable.
    /// </summary>
    public interface IAddressable : IServiceModel
    {
        /// <summary>
        /// Gets addresses.
        /// </summary>
        ICollection<IAddress> Addresses { get; }
    }
}
