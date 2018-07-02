// <copyright file="IReferencesTagger.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.References
{
    using ProcessingTools.Models.Contracts.Layout.Styles.References;

    /// <summary>
    /// References tagger.
    /// </summary>
    public interface IReferencesTagger : IXmlContextTagger<IReferenceTagStyleModel, object>
    {
    }
}
