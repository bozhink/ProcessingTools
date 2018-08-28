// <copyright file="EncodeService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Tools
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using ProcessingTools.Services.Contracts.Tools;

    /// <summary>
    /// Encode service.
    /// </summary>
    public class EncodeService : IEncodeService
    {
        private readonly Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodeService"/> class.
        /// </summary>
        /// <param name="encoding">Character encoding</param>
        public EncodeService(Encoding encoding)
        {
            this.encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        /// <inheritdoc/>
        public Task<string> EncodeBase64Async(string source)
        {
            string result = null;

            if (!string.IsNullOrEmpty(source))
            {
                byte[] bytes = this.encoding.GetBytes(source);
                result = Convert.ToBase64String(bytes);
            }

            return Task.FromResult(result);
        }

        /// <inheritdoc/>
        public Task<string> EncodeBase64UrlAsync(string source)
        {
            string result = null;

            if (!string.IsNullOrEmpty(source))
            {
                byte[] bytes = this.encoding.GetBytes(source);
                result = ProcessingTools.Security.Utils.ToBase64Url(bytes);
            }

            return Task.FromResult(result);
        }
    }
}
