// <copyright file="ISpecimenCodesByPatternDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio.SpecimenCodes
{
    using ProcessingTools.Services.Models.Contracts.Bio.SpecimenCodes;

    /// <summary>
    /// Specimen codes by pattern data miner.
    /// </summary>
    public interface ISpecimenCodesByPatternDataMiner : IDataMiner<string, ISpecimenCode>
    {
    }
}
