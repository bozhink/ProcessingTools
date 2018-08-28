namespace ProcessingTools.Services.Tools
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using ProcessingTools.Services.Contracts.Tools;
    using Utils = ProcessingTools.Security.Utils;

    /// <summary>
    /// Hash service.
    /// </summary>
    public class HashService : IHashService
    {
        private readonly Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashService"/> class.
        /// </summary>
        /// <param name="encoding">Character encoding.</param>
        public HashService(Encoding encoding)
        {
            this.encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        /// <inheritdoc/>
        public Task<string> GetMD5HashAsBase64StringAsync(string source)
        {
            string hash = Utils.GetMD5HashAsBase64String(source, this.encoding);
            return Task.FromResult(hash);
        }

        /// <inheritdoc/>
        public Task<string> GetMD5HashAsStringAsync(string source)
        {
            string hash = Utils.GetMD5HashAsString(source, this.encoding);
            return Task.FromResult(hash);
        }

        /// <inheritdoc/>
        public Task<string> GetSHA1HashAsBase64StringAsync(string source)
        {
            string hash = Utils.GetSHA1HashAsBase64String(source, this.encoding);
            return Task.FromResult(hash);
        }

        /// <inheritdoc/>
        public Task<string> GetSHA1HashAsStringAsync(string source)
        {
            string hash = Utils.GetSHA1HashAsString(source, this.encoding);
            return Task.FromResult(hash);
        }

        /// <inheritdoc/>
        public Task<string> GetSHA256HashAsBase64StringAsync(string source)
        {
            string hash = Utils.GetSHA256HashAsBase64String(source, this.encoding);
            return Task.FromResult(hash);
        }

        /// <inheritdoc/>
        public Task<string> GetSHA256HashAsStringAsync(string source)
        {
            string hash = Utils.GetSHA256HashAsString(source, this.encoding);
            return Task.FromResult(hash);
        }

        /// <inheritdoc/>
        public Task<string> GetSHA384HashAsBase64StringAsync(string source)
        {
            string hash = Utils.GetSHA384HashAsBase64String(source, this.encoding);
            return Task.FromResult(hash);
        }

        /// <inheritdoc/>
        public Task<string> GetSHA384HashAsStringAsync(string source)
        {
            string hash = Utils.GetSHA384HashAsString(source, this.encoding);
            return Task.FromResult(hash);
        }

        /// <inheritdoc/>
        public Task<string> GetSHA512HashAsBase64StringAsync(string source)
        {
            string hash = Utils.GetSHA512HashAsBase64String(source, this.encoding);
            return Task.FromResult(hash);
        }

        /// <inheritdoc/>
        public Task<string> GetSHA512HashAsStringAsync(string source)
        {
            string hash = Utils.GetSHA512HashAsString(source, this.encoding);
            return Task.FromResult(hash);
        }
    }
}
