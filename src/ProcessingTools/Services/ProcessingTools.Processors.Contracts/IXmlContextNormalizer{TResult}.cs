// <copyright file="IXmlContextNormalizer{TResult}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.Xml;

    /// <summary>
    /// Generic normalizer over <see cref="XmlNode"/> context.
    /// </summary>
    /// <typeparam name="TResult">Type of resultant object</typeparam>
    public interface IXmlContextNormalizer<TResult> : INormalizer<XmlNode, TResult>
    {
    }
}
