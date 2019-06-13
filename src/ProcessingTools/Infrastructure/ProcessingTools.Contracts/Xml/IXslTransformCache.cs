﻿// <copyright file="IXslTransformCache.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Xml
{
    using System.Xml.Xsl;

    /// <summary>
    /// XSL transform cache.
    /// </summary>
    public interface IXslTransformCache : ITransformCache<XslCompiledTransform>
    {
    }
}