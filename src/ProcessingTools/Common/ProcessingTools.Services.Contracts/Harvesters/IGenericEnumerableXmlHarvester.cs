// <copyright file="IGenericEnumerableXmlHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Harvesters
{
    using System.Collections.Generic;
    using System.Xml;

    public interface IGenericEnumerableXmlHarvester<T> : IEnumerableHarvester<XmlNode, T>, IGenericXmlHarvester<IEnumerable<T>>
    {
    }
}
