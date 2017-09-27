// <copyright file="IGenericXmlHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Harvesters
{
    using System.Xml;

    public interface IGenericXmlHarvester<T> : IHarvester<XmlNode, T>
    {
    }
}
