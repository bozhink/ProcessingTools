﻿// <copyright file="IInstitutionDetails.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Services.Data.Journals
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Institution details.
    /// </summary>
    public interface IInstitutionDetails : IInstitution, IAddressable, ICreated, IModified, IDetailedModel
    {
    }
}
