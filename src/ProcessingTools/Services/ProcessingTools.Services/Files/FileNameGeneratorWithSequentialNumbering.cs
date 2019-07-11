// <copyright file="FileNameGeneratorWithSequentialNumbering.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Files
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Services.Files;

    /// <summary>
    /// File name generator with sequential numbering.
    /// </summary>
    public class FileNameGeneratorWithSequentialNumbering : IFileNameGenerator
    {
        private readonly IFileNameResolver fileNameResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileNameGeneratorWithSequentialNumbering"/> class.
        /// </summary>
        /// <param name="fileNameResolver">File name resolver.</param>
        public FileNameGeneratorWithSequentialNumbering(IFileNameResolver fileNameResolver)
        {
            this.fileNameResolver = fileNameResolver ?? throw new ArgumentNullException(nameof(fileNameResolver));
        }

        /// <inheritdoc/>
        public string GetNewFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new FileNameIsNullOrWhitespaceException();
            }

            string fullFileName = this.fileNameResolver.GetFullFileName(fileName);

            return this.GetNewFileName(directoryName: Path.GetDirectoryName(fullFileName), fileName: Path.GetFileName(fileName));
        }

        private string GetNewFileName(string directoryName, string fileName)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            string outputFileNameFormat;

            if (Regex.IsMatch(fileNameWithoutExtension, @"\-out(?:\-\d+)?\Z"))
            {
                fileNameWithoutExtension = Regex.Replace(fileNameWithoutExtension, @"\-\d+(?=\Z)", string.Empty);

                outputFileNameFormat = "{0}-{1}{2}";
            }
            else
            {
                outputFileNameFormat = "{0}-out-{1}{2}";
            }

            string extension = Path.GetExtension(fileName);

            string generatedFileName;

            for (int i = 1; i <= FileConstants.MaximalNumberOfIterationsToGenerateNewFileName; i++)
            {
                generatedFileName = string.Format(outputFileNameFormat, fileNameWithoutExtension, i, extension);

                if (generatedFileName.Length > FileConstants.MaximalLengthOfGeneratedNewFileName)
                {
                    throw new MaximalLengthOfFileNameExceededException();
                }

                string fullFileName = Path.Combine(directoryName, generatedFileName);

                if (!File.Exists(fullFileName))
                {
                    return generatedFileName;
                }
            }

            throw new MaximalNumberOfIterationsExceededException("Maximal number of iterations to find a new unique file name is exceeded");
        }
    }
}
