// <copyright file="IEnumerableXmlHarvester{TModel}.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// Enumerable harvester with <see cref="XmlNode"/> context.
    /// </summary>
    /// <typeparam name="TModel">Type of the harvester model.</typeparam>
    public interface IEnumerableXmlHarvester<TModel> : IXmlHarvester<IList<TModel>>
    {
    }
}
