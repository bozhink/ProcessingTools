// <copyright file="IEnumerableXmlHarvesterCore{TModel}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Xml;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Core enumerable harvester with <see cref="XmlNode"/> context.
    /// </summary>
    /// <typeparam name="TModel">Type of the harvester model.</typeparam>
    public interface IEnumerableXmlHarvesterCore<TModel> : IXmlHarvesterCore<TModel[]>
    {
    }
}
