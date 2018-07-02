// <copyright file="IAbbreviationsHarvester.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Contracts.Abbreviations
{
    using ProcessingTools.Harvesters.Models.Contracts.Abbreviations;

    /// <summary>
    /// Abbreviations harvester.
    /// </summary>
    public interface IAbbreviationsHarvester : IEnumerableXmlHarvester<IAbbreviationModel>
    {
    }
}
