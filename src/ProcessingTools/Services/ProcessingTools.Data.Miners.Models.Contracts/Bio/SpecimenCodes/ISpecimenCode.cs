// <copyright file="ISpecimenCode.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Models.Contracts.Bio.SpecimenCodes
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Specimen code.
    /// </summary>
    public interface ISpecimenCode : IContent, IContentTyped, IUrlLinkable
    {
    }
}
