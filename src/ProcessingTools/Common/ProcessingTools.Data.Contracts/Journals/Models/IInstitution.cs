// <copyright file="IInstitution.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Journals.Models
{
    using ProcessingTools.Models.Contracts;

    public interface IInstitution : IAddressable, IAbbreviatedNameableStringIdentifiable, IModelWithUserInformation
    {
    }
}
