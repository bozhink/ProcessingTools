// <copyright file="IAffiliation.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Documents.Models
{
    using ProcessingTools.Models.Contracts;
    using System;

    /// <summary>
    /// Affiliation.
    /// </summary>
    public interface IAffiliation : INameableGuidIdentifiable, IModelWithUserInformation
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
