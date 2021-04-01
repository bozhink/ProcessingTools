// <copyright file="ITransliteration.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Language
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
