// <copyright file="IReferencesTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models.Layout.Styles.References;

namespace ProcessingTools.Contracts.Services.References
{
    /// <summary>
    /// References tagger.
    /// </summary>
    public interface IReferencesTagger : IXmlContextTagger<IReferenceTagStyleModel, object>
    {
    }
}
