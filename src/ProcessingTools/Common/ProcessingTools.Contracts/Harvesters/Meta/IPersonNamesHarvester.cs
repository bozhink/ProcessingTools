// <copyright file="IPersonNamesHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Harvesters.Meta
{
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Models.Contracts.Harvesters.Meta;

    /// <summary>
    /// Person names harvester.
    /// </summary>
    public interface IPersonNamesHarvester : IEnumerableXmlHarvester<IPersonNameModel>
    {
    }
}
