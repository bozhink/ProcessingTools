// <copyright file="MimeMappingService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Files
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.StaticFiles;
    using ProcessingTools.Contracts.Services.Files;
    using ProcessingTools.Extensions;

    /// <summary>
    /// MIME mapping service.
    /// </summary>
    /// <remarks>
    /// See https://dotnetcoretutorials.com/2018/08/14/getting-a-mime-type-from-a-file-name-in-net-core/.
    /// </remarks>
    public class MimeMappingService : IMimeMappingService
    {
        private readonly IContentTypeProvider contentTypeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MimeMappingService"/> class.
        /// </summary>
        /// <param name="contentTypeProvider">Content type provider.</param>
        public MimeMappingService(IContentTypeProvider contentTypeProvider)
        {
            this.contentTypeProvider = contentTypeProvider ?? throw new ArgumentNullException(nameof(contentTypeProvider));
        }

        /// <inheritdoc/>
        public Task<string> MapAsync(string fileName)
        {
            if (!this.contentTypeProvider.TryGetContentType(fileName, out string contentType))
            {
                contentType = ContentTypes.OctetStream;
            }

            return Task.FromResult(contentType);
        }
    }
}
