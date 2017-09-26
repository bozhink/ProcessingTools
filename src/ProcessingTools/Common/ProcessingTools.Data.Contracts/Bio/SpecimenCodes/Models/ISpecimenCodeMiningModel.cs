// <copyright file="ISpecimenCodeMiningModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Bio.SpecimenCodes.Models
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Specimen code.
    /// </summary>
    public interface ISpecimenCode : IDescribable, IUrlLinkable, IPatternHoldable
    {
    }
}
