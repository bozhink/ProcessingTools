﻿// <copyright file="IXmlContextTagger{TResult}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts
{
    using System.Xml;

    /// <summary>
    /// Context tagger for <see cref="XmlNode"/> context.
    /// </summary>
    /// <typeparam name="TResult">Type of output result.</typeparam>
    public interface IXmlContextTagger<TResult> : IContextTagger<XmlNode, TResult>
    {
    }
}
