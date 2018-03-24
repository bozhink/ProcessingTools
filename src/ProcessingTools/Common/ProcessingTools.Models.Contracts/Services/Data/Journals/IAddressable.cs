// <copyright file="IAddressable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Services.Data.Journals
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

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
