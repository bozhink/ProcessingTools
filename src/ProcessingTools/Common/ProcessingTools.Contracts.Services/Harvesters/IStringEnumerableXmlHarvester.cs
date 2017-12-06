// <copyright file="IStringEnumerableXmlHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Harvesters
{
    /// <summary>
    /// Enumerable harvester with <see cref="System.Xml.XmlNode"/> context and string model.
    /// </summary>
    public interface IStringEnumerableXmlHarvester : IEnumerableXmlHarvester<string>
    {
    }
}
