// <copyright file="IInstitution.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Institution.
    /// </summary>
    public interface IInstitution : IAddressable, IAbbreviatedNameableGuidIdentifiable, ICreated, IModified
    {
    }
}
