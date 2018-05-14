// <copyright file="IXmlContextTagger{TStyle,TResult}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.Xml;
    using ProcessingTools.Models.Contracts.Layout.Styles;

    /// <summary>
    /// Context tagger for <see cref="XmlNode"/> context.
    /// </summary>
    /// <typeparam name="TStyle">Type of style rules to apply.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    public interface IXmlContextTagger<TStyle, TResult> : IContextTagger<XmlNode, TStyle, TResult>
        where TStyle : IStyleModel
    {
    }
}
