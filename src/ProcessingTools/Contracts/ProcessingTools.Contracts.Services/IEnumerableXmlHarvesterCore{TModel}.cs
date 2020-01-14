// <copyright file="IEnumerableXmlHarvesterCore{TModel}.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// Core enumerable harvester with <see cref="XmlNode"/> context.
    /// </summary>
    /// <typeparam name="TModel">Type of the harvester model.</typeparam>
    public interface IEnumerableXmlHarvesterCore<TModel> : IXmlHarvesterCore<IList<TModel>>
    {
    }
}
