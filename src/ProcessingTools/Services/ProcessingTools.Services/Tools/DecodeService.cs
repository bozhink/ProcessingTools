﻿// <copyright file="DecodeService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Tools
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services.Tools;
    using ProcessingTools.Services.Security;

    /// <summary>
    /// Decode service.
    /// </summary>
    public class DecodeService : IDecodeService
    {
        private readonly Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="DecodeService"/> class.
        /// </summary>
        /// <param name="encoding">Character encoding.</param>
        public DecodeService(Encoding encoding)
        {
            this.encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        /// <inheritdoc/>
        public Task<string> DecodeBase64Async(string source)
        {
            string result = null;

            if (!string.IsNullOrEmpty(source))
            {
                byte[] bytes = Convert.FromBase64String(source.Trim(new[] { ' ', '\t', '\r', '\n' }));
                result = this.encoding.GetString(bytes);
            }

            return Task.FromResult(result);
        }

        /// <inheritdoc/>
        public Task<string> DecodeBase64UrlAsync(string source)
        {
            string result = null;

            if (!string.IsNullOrEmpty(source))
            {
                byte[] bytes = SecurityUtilities.FromBase64Url(source.Trim(new[] { ' ', '\t', '\r', '\n' }));
                result = this.encoding.GetString(bytes);
            }

            return Task.FromResult(result);
        }
    }
}
