// <copyright file="IAbbreviationsHarvester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Abbreviations;

namespace ProcessingTools.Contracts.Services.Abbreviations
{
    /// <summary>
    /// Abbreviations harvester.
    /// </summary>
    public interface IAbbreviationsHarvester : IEnumerableXmlHarvester<IAbbreviationModel>
    {
    }
}
