namespace ProcessingTools.FileSystem.Generators
{
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Constants;
    using Contracts.Generators;
    using ProcessingTools.Common.Exceptions;

    public class SequentialFileNameGenerator : ISequentialFileNameGenerator
    {
        public Task<string> Generate(string baseFileFullName, int maximalFileNameLength, bool returnFullName = true)
        {
            string directoryPath = Path.GetDirectoryName(baseFileFullName);
            string fileName = Path.GetFileName(baseFileFullName);

            return this.Generate(directoryPath, fileName, maximalFileNameLength, returnFullName);
        }

        public Task<string> Generate(string directoryPath, string baseFileName, int maximalFileNameLength, bool returnFullName = false)
        {
            return Task.Run(() =>
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
                string fileName, fullName;
                int i = 0;
                do
                {
                    fileName = string.Format(outputFileNameFormat, fileNameWithoutExtension, ++i, extension);
                    if (fileName.Length > maximalFileNameLength)
                    {
                        throw new MaximalLengthOfFileNameExceededException();
                    }

                    fullName = Path.Combine(directoryPath, fileName);

                    if (i > GeneratorsConstants.MaximalNumberOfIterationsToFindNewFileName)
                    {
                        throw new MaximalNumberOfIterationsExceededException(GeneratorsConstants.MaximalNumberOfIterationsExceedeExceptionMessage);
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
            });
        }
    }
}
