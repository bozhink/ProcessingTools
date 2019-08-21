// <copyright file="IAbbreviationsHarvester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Abbreviations
{
    using ProcessingTools.Contracts.Models.Abbreviations;

    /// <summary>
    /// Abbreviations harvester.
    /// </summary>
    public interface IAbbreviationsHarvester : IEnumerableXmlHarvester<IAbbreviationModel>
    {
    }
}
