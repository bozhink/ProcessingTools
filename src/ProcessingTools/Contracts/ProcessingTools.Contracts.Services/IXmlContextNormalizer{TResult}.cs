﻿// <copyright file="IXmlContextNormalizer{TResult}.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System.Xml;

    /// <summary>
    /// Generic normalizer over <see cref="XmlNode"/> context.
    /// </summary>
    /// <typeparam name="TResult">Type of resultant object.</typeparam>
    public interface IXmlContextNormalizer<TResult> : INormalizer<XmlNode, TResult>
    {
    }
}
