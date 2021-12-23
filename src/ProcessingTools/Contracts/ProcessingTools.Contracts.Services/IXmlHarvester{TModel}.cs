// <copyright file="IXmlHarvester{TModel}.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System.Xml;

    /// <summary>
    /// Harvester with <see cref="XmlNode"/> context.
    /// </summary>
    /// <typeparam name="TModel">Type of the harvester model.</typeparam>
    public interface IXmlHarvester<TModel> : IHarvester<XmlNode, TModel>
    {
    }
}
