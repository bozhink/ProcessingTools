// <copyright file="IXmlContextParser{TResult}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Xml;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Context parser for <see cref="XmlNode"/> context.
    /// </summary>
    /// <typeparam name="TResult">Type of output result.</typeparam>
    public interface IXmlContextParser<TResult> : IContextParser<XmlNode, TResult>
    {
    }
}
