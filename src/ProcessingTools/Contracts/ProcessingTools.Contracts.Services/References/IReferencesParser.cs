// <copyright file="IReferencesParser.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.References
{
    using ProcessingTools.Contracts.Models.Layout.Styles.References;

    /// <summary>
    /// References parser.
    /// </summary>
    public interface IReferencesParser : IXmlContextParser<IReferenceParseStyleModel, object>
    {
    }
}
