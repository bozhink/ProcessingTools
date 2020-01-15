﻿// <copyright file="IInstitutionEntity.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Resources
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Institution.
    /// </summary>
    public interface IInstitutionEntity : INamedIntegerIdentified, IEntityWithSources
    {
    }
}