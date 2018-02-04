// <copyright file="IAbbreviationsHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Harvesters.Abbreviations
{
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Models.Contracts.Harvesters.Abbreviations;

    /// <summary>
    /// Abbreviations harvester.
    /// </summary>
    public interface IAbbreviationsHarvester : IEnumerableXmlHarvester<IAbbreviationModel>
    {
    }
}
