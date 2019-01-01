﻿// <copyright file="IAffiliation.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents
{
    using System;
    using ProcessingTools.Models.Contracts;

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
