// <copyright file="IPersonNamesHarvester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Meta;

namespace ProcessingTools.Contracts.Services.Meta
{
    /// <summary>
    /// Person names harvester.
    /// </summary>
    public interface IPersonNamesHarvester : IEnumerableXmlHarvester<IPersonNameModel>
    {
    }
}
