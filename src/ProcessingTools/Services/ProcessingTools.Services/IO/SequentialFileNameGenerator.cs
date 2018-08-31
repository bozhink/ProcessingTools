// <copyright file="SequentialFileNameGenerator.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.IO
{
    using System.IO;
    using System.Text.RegularExpressions;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Services.Contracts.IO;

    /// <summary>
    /// File name generator with sequential file name generation.
    /// </summary>
    public class SequentialFileNameGenerator : IFileNameGenerator
    {
        private const int MaximalNumberOfIterationsToFindNewFileName = 200;

        /// <inheritdoc/>
        public string Generate(string baseFileFullName, int maximalFileNameLength, bool returnFullName = true)
        {
            string directoryPath = Path.GetDirectoryName(baseFileFullName);
            string fileName = Path.GetFileName(baseFileFullName);

            return this.Generate(directoryPath, fileName, maximalFileNameLength, returnFullName);
        }

        /// <inheritdoc/>
        public string Generate(string directoryName, string baseFileName, int maximalFileNameLength, bool returnFullName = false)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(baseFileName);
            string outputFileNameFormat = null;
            if (Regex.IsMatch(fileNameWithoutExtension, @"\-out(?:\-\d+)?\Z"))
            {
                fileNameWithoutExtension = Regex.Replace(fileNameWithoutExtension, @"\-\d+(?=\Z)", string.Empty);

                outputFileNameFormat = "{0}-{1}{2}";
            }
            else
            {
                outputFileNameFormat = "{0}-out-{1}{2}";
            }

            string extension = Path.GetExtension(baseFileName);
            string fileName;
            string fullName;
            int i = 0;
            do
            {
                fileName = string.Format(outputFileNameFormat, fileNameWithoutExtension, ++i, extension);
                if (fileName.Length > maximalFileNameLength)
                {
                    throw new MaximalLengthOfFileNameExceededException();
                }

                fullName = Path.Combine(directoryName, fileName);

                if (i > MaximalNumberOfIterationsToFindNewFileName)
                {
                    throw new MaximalNumberOfIterationsExceededException("Maximal number of iterations to find a new unique file name is exceeded");
                }
            }
            while (File.Exists(fullName));

            if (returnFullName)
            {
                return fullName;
            }
            else
            {
                return fileName;
            }
        }
    }
}
