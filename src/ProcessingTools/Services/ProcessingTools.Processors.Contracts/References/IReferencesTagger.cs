// <copyright file="IReferencesTagger.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.References
{
    /// <summary>
    /// References tagger.
    /// </summary>
    public interface IReferencesTagger : IXmlContextTagger
    {
        /// <summary>
        /// Gets or sets the path for output references Xml file.
        /// </summary>
        string ReferencesGetReferencesXmlPath { get; set; }
    }
}
