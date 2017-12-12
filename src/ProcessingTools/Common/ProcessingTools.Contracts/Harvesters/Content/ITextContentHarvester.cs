// <copyright file="ITextContentHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Harvesters.Content
{
    using ProcessingTools.Contracts.Harvesters;

    /// <summary>
    /// Text content harvester.
    /// </summary>
    public interface ITextContentHarvester : IXmlHarvester<string>
    {
    }
}
