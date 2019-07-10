// <copyright file="IReferencesParser.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models.Layout.Styles.References;

namespace ProcessingTools.Contracts.Services.References
{
    /// <summary>
    /// References parser.
    /// </summary>
    public interface IReferencesParser : IXmlContextParser<IReferenceParseStyleModel, object>
    {
    }
}
