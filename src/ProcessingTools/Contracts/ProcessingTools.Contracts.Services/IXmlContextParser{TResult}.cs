// <copyright file="IXmlContextParser{TResult}.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System.Xml;

    /// <summary>
    /// Context parser for <see cref="XmlNode"/> context.
    /// </summary>
    /// <typeparam name="TResult">Type of output result.</typeparam>
    public interface IXmlContextParser<TResult> : IContextParser<XmlNode, TResult>
    {
    }
}
