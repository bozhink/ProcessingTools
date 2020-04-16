﻿// <copyright file="IXmlContextParser{TStyle,TResult}.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System.Xml;
    using ProcessingTools.Contracts.Models.Layout.Styles;

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