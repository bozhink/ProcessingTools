// <copyright file="IReferencesParser.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.References
{
    using ProcessingTools.Models.Contracts.Layout.Styles.References;

    /// <summary>
    /// References parser.
    /// </summary>
    public interface IReferencesParser : IXmlContextParser<IReferenceParseStyleModel, object>
    {
    }
}
