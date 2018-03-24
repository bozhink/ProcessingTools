// <copyright file="ISpecimenCode.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Bio.SpecimenCodes
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Specimen code.
    /// </summary>
    public interface ISpecimenCode : IDescribable, IUrlLinkable, IPatternHoldable
    {
    }
}
