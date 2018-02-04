// <copyright file="IAddressable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Journals
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Model with addresses.
    /// </summary>
    public interface IAddressable : IDataModel
    {
        /// <summary>
        /// Gets addresses.
        /// </summary>
        IEnumerable<IAddress> Addresses { get; }
    }
}
