// <copyright file="ISpecimenCode.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Bio.SpecimenCodes
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Specimen code.
    /// </summary>
    public interface ISpecimenCode : IDescribed, IUrlLinkable, IPatternHoldable
    {
    }
}
