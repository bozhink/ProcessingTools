namespace ProcessingTools.Services.Data.Services.Files
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Services.Contracts.Files;

    public class StreamingSystemFileContentDataService : IStreamingSystemFileContentDataService
    {
        public StreamReader GetReader(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            string fileName = id.ToString();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new FileNameIsNullOrWhitespaceException();
            }

            var reader = new StreamReader(fileName);
            return reader;
        }

        public Stream ReadToStream(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            string fileName = id.ToString();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new FileNameIsNullOrWhitespaceException();
            }

            var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            return stream;
        }

        public Task<object> WriteAsync(object id, StreamReader streamReader)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (streamReader == null)
            {
                throw new ArgumentNullException(nameof(streamReader));
            }

            return this.WriteAsync(id, streamReader.BaseStream);
        }

        public async Task<object> WriteAsync(object id, Stream stream)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new StreamCannotBeReadException();
            }

            string fileName = id.ToString();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new FileNameIsNullOrWhitespaceException();
            }

            long writtenStreamLength = 0L;
            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream).ConfigureAwait(false);
                await fileStream.FlushAsync().ConfigureAwait(false);
                writtenStreamLength = fileStream.Length;
                fileStream.Close();
            }

            return writtenStreamLength;
        }
    }
}
