// <copyright file="IReporter.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Represents reporter.
    /// </summary>
    public interface IReporter
    {
        /// <summary>
        /// Appends content to current report.
        /// </summary>
        /// <param name="content">String content to be appended.</param>
        void AppendContent(string content);

        /// <summary>
        /// Generates the report.
        /// </summary>
        /// <returns>Task</returns>
        Task MakeReportAsync();
    }
}
