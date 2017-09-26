// <copyright file="IInstitution.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Institution.
    /// </summary>
    public interface IInstitution : IAddressable, IAbbreviatedNameableGuidIdentifiable, IModelWithUserInformation
    {
    }
}
