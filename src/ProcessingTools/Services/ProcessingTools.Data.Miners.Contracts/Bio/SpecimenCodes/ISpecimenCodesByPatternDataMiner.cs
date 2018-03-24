// <copyright file="ISpecimenCodesByPatternDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Contracts.Bio.SpecimenCodes
{
    using ProcessingTools.Data.Miners.Models.Contracts.Bio.SpecimenCodes;

    /// <summary>
    /// Specimen codes by pattern data miner.
    /// </summary>
    public interface ISpecimenCodesByPatternDataMiner : IDataMiner<string, ISpecimenCode>
    {
    }
}
