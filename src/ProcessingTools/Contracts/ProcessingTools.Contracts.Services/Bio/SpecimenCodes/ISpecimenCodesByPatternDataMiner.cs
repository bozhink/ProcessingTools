// <copyright file="ISpecimenCodesByPatternDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.SpecimenCodes
{
    using ProcessingTools.Contracts.Models.Bio.SpecimenCodes;

    /// <summary>
    /// Specimen codes by pattern data miner.
    /// </summary>
    public interface ISpecimenCodesByPatternDataMiner : IDataMiner<string, ISpecimenCode>
    {
    }
}
