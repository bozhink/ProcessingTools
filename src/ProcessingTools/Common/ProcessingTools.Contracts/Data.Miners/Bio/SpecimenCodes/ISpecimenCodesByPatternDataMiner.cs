// <copyright file="ISpecimenCodesByPatternDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Contracts.Miners.Bio.SpecimenCodes
{
    using ProcessingTools.Contracts.Data.Miners;
    using ProcessingTools.Data.Miners.Contracts.Models.Bio.SpecimenCodes;

    /// <summary>
    /// Specimen codes by pattern data miner.
    /// </summary>
    public interface ISpecimenCodesByPatternDataMiner : IDataMiner<string, ISpecimenCode>
    {
    }
}
