// <copyright file="IParseXmlContextStrategy.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.Xml;

    /// <summary>
    /// Strategy to parse specified <see cref="XmlNode"/> context.
    /// </summary>
    public interface IParseXmlContextStrategy : IParseContextStrategy<XmlNode, object>
    {
    }
}
