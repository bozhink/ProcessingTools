// <copyright file="IParseXmlContextStrategy.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Xml;

namespace ProcessingTools.Contracts.Services.Strategies
{
    /// <summary>
    /// Strategy to parse specified <see cref="XmlNode"/> context.
    /// </summary>
    public interface IParseXmlContextStrategy : IParseContextStrategy<XmlNode, object>
    {
    }
}
