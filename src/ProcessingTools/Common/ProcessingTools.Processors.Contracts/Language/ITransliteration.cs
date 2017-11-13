// <copyright file="ITransliteration.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Language
{
    /// <summary>
    /// Transliteration.
    /// </summary>
    public interface ITransliteration
    {
        /// <summary>
        /// Converts text content according to transliteration map.
        /// </summary>
        /// <param name="text">Text content to be processed.</param>
        /// <returns>Processed text.</returns>
        string ProcessText(string text);
    }
}
