﻿// <copyright file="IXslTransformCache.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Xml
{
    using System.Xml.Xsl;

    /// <summary>
    /// XSL transform cache.
    /// </summary>
    public interface IXslTransformCache : ITransformCache<XslCompiledTransform>
    {
    }
}
