// <copyright file="IPersonNamesHarvester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Meta
{
    using ProcessingTools.Services.Models.Contracts.Meta;

    /// <summary>
    /// Person names harvester.
    /// </summary>
    public interface IPersonNamesHarvester : IEnumerableXmlHarvester<IPersonNameModel>
    {
    }
}
