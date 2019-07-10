// <copyright file="IReferencesParser.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.References
{
    using ProcessingTools.Contracts.Models.Layout.Styles.References;

    /// <summary>
    /// References parser.
    /// </summary>
    public interface IReferencesParser : IXmlContextParser<IReferenceParseStyleModel, object>
    {
    }
}
