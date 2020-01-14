// <copyright file="IReferencesTagger.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.References
{
    using ProcessingTools.Contracts.Models.Layout.Styles.References;

    /// <summary>
    /// References tagger.
    /// </summary>
    public interface IReferencesTagger : IXmlContextTagger<IReferenceTagStyleModel, object>
    {
    }
}
