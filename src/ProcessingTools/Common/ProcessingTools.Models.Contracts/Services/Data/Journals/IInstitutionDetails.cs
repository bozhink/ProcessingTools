﻿// <copyright file="IInstitutionDetails.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Services.Data.Journals
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Institution details.
    /// </summary>
    public interface IInstitutionDetails : IInstitution, IAddressable, ICreated, IModified, IDetailedModel
    {
    }
}
