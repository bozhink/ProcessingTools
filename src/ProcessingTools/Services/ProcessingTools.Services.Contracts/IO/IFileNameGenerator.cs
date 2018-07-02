// <copyright file="IFileNameGenerator.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.IO
{
    using System.Threading.Tasks;

    /// <summary>
    /// File name generator.
    /// </summary>
    public interface IFileNameGenerator
    {
        /// <summary>
        /// Generates file name based on specified file name with specified maximal length.
        /// </summary>
        /// <param name="baseFileFullName">Full name of the file on which name the file-name-generation will be based</param>
        /// <param name="maximalFileNameLength">Maximal length of the output file name</param>
        /// <param name="returnFullName">Specified if to return the full name of the generated file name or to return only the file name</param>
        /// <returns>Generated file name</returns>
        string Generate(string baseFileFullName, int maximalFileNameLength, bool returnFullName = true);

        /// <summary>
        /// Generates file name based on specified file name with specified maximal length and related to a directory.
        /// </summary>
        /// <param name="directoryName">The name directory to which the generated file name will be related</param>
        /// <param name="baseFileName">The name of the file on which name the file-name-generation will be based</param>
        /// <param name="maximalFileNameLength">Maximal length of the output file name</param>
        /// <param name="returnFullName">Specified if to return the full name of the generated file name or to return only the file name</param>
        /// <returns>Generated file name</returns>
        string Generate(string directoryName, string baseFileName, int maximalFileNameLength, bool returnFullName = false);
    }
}
