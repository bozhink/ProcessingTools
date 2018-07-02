// <copyright file="IXmlContextParser{TStyle,TResult}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.Xml;
    using ProcessingTools.Models.Contracts.Layout.Styles;

    /// <summary>
    /// Context parser for <see cref="XmlNode"/> context.
    /// </summary>
    /// <typeparam name="TStyle">Type of style rules to apply.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    public interface IXmlContextParser<in TStyle, TResult> : IContextParser<XmlNode, TStyle, TResult>
        where TStyle : IStyleModel
    {
    }
}
