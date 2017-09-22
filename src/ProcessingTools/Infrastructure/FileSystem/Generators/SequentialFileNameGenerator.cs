namespace ProcessingTools.FileSystem.Generators
{
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Exceptions;

    public class SequentialFileNameGenerator : ISequentialFileNameGenerator
    {
        private const int MaximalNumberOfIterationsToFindNewFileName = 200;

        public Task<string> GenerateAsync(string baseFileFullName, int maximalFileNameLength, bool returnFullName = true)
        {
            string directoryPath = Path.GetDirectoryName(baseFileFullName);
            string fileName = Path.GetFileName(baseFileFullName);

            return this.GenerateAsync(directoryPath, fileName, maximalFileNameLength, returnFullName);
        }

        public Task<string> GenerateAsync(string directoryPath, string baseFileName, int maximalFileNameLength, bool returnFullName = false)
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

                    fullName = Path.Combine(directoryPath, fileName);

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
            });
        }
    }
}
