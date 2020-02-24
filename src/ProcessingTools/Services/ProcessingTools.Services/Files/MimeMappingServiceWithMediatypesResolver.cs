// <copyright file="MimeMappingServiceWithMediatypesResolver.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Files
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Services.Files;
    using ProcessingTools.Extensions;

    /// <summary>
    /// MIME mapping service.
    /// </summary>
    public class MimeMappingServiceWithMediatypesResolver : IMimeMappingService
    {
        private readonly IMediatypesResolver resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="MimeMappingServiceWithMediatypesResolver"/> class.
        /// </summary>
        /// <param name="resolver">Instance of <see cref="IMediatypesResolver"/>.</param>
        public MimeMappingServiceWithMediatypesResolver(IMediatypesResolver resolver)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        /// <inheritdoc/>
        public async Task<string> MapAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new FileNameIsNullOrWhitespaceException();
            }

            var mediatypes = await this.resolver.ResolveMediatypeAsync(Path.GetExtension(fileName)).ConfigureAwait(false);

            if (mediatypes is null || !mediatypes.Any())
            {
                return ContentTypes.OctetStream;
            }

            var mediatype = mediatypes.First();

            return $"{mediatype.MimeType}/{mediatype.MimeSubtype}";
        }
    }
}
