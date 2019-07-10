// <copyright file="IXmlContextParser{TStyle,TResult}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Xml;
using ProcessingTools.Contracts.Models.Layout.Styles;

namespace ProcessingTools.Contracts.Services
{
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
