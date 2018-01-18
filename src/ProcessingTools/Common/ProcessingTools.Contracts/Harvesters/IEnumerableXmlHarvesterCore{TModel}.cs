// <copyright file="IEnumerableXmlHarvesterCore{TModel}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Harvesters
{
    using System.Xml;

    /// <summary>
    /// Core enumerable harvester with <see cref="XmlNode"/> context.
    /// </summary>
    /// <typeparam name="TModel">Type of the harvester model.</typeparam>
    public interface IEnumerableXmlHarvesterCore<TModel> : IXmlHarvesterCore<TModel[]>
    {
    }
}
