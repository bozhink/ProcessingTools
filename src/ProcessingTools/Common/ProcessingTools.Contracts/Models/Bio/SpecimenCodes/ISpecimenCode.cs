// <copyright file="ISpecimenCode.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Bio.SpecimenCodes
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Specimen code.
    /// </summary>
    public interface ISpecimenCode : IDescribable, IUrlLinkable, IPatternHoldable
    {
    }
}
