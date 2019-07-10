// <copyright file="IXslTransformCache.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Xml.Xsl;

namespace ProcessingTools.Contracts.Services.Xml
{
    /// <summary>
    /// XSL transform cache.
    /// </summary>
    public interface IXslTransformCache : ITransformCache<XslCompiledTransform>
    {
    }
}
