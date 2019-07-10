// <copyright file="IXmlHarvester{TModel}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Xml;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Harvester with <see cref="XmlNode"/> context.
    /// </summary>
    /// <typeparam name="TModel">Type of the harvester model.</typeparam>
    public interface IXmlHarvester<TModel> : IHarvester<XmlNode, TModel>
    {
    }
}
