// <copyright file="MimeMappingServiceWithStaticDictionary.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Files
{
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Services.Files;
    using ProcessingTools.Extensions;

    /// <summary>
    /// MIME mapping service with static dictionary.
    /// </summary>
    public partial class MimeMappingServiceWithStaticDictionary : IMimeMappingService
    {
        /// <inheritdoc/>
        public Task<string> MapAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new FileNameIsNullOrWhitespaceException();
            }

            string extension = Path.GetExtension(fileName).TrimStart('.');
            if (this.mimetypes.TryGetValue(extension, out string mediatype))
            {
                return Task.FromResult(mediatype);
            }
            else
            {
                return Task.FromResult(ContentTypes.OctetStream);
            }
        }
    }
}
