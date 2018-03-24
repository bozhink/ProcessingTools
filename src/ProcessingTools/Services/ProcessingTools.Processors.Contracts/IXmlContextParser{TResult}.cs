// <copyright file="IXmlContextParser{TResult}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.Xml;

    /// <summary>
    /// Context parser for <see cref="XmlNode"/> context.
    /// </summary>
    /// <typeparam name="TResult">Type of output result</typeparam>
    public interface IXmlContextParser<TResult> : IContextParser<XmlNode, TResult>
    {
    }
}
