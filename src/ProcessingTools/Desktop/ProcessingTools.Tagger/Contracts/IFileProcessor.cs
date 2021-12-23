// <copyright file="IFileProcessor.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tagger.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// File processor.
    /// </summary>
    public interface IFileProcessor
    {
        /// <summary>
        /// Run the processing of file.
        /// </summary>
        /// <param name="settings">Program settings for the execution of the processing.</param>
        /// <returns>Task.</returns>
        Task RunAsync(IProgramSettings settings);
    }
}
