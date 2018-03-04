// <copyright file="IPersonNamesHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Contracts.Meta
{
    using ProcessingTools.Harvesters.Models.Contracts.Meta;

    /// <summary>
    /// Person names harvester.
    /// </summary>
    public interface IPersonNamesHarvester : IEnumerableXmlHarvester<IPersonNameModel>
    {
    }
}
