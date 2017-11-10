// <copyright file="IInstitutionDetails.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Data.Journals
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Institution details.
    /// </summary>
    public interface IInstitutionDetails : IInstitution, IAddressable, ICreated, IModified, IDetailedModel
    {
    }
}
