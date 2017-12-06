// <copyright file="IAffiliation.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents
{
    using System;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Affiliation.
    /// </summary>
    public interface IAffiliation : INameableGuidIdentifiable, ICreated, IModified
    {
        /// <summary>
        /// Gets institution ID.
        /// </summary>
        Guid InstitutionId { get; }

        /// <summary>
        /// Gets address ID.
        /// </summary>
        Guid AddressId { get; }
    }
}
